// Date   : 30.01.2016 15:22
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private QuestItem questItemPrefab;

    [SerializeField]
    private Transform questItemContainer;
    /*
    public bool CheckRequirements(List<InventoryItem> items)
    {
        bool success = true;
        InventoryItem[] itemsCopy;
        items.CopyTo(itemsCopy);
        foreach (QuestRequirement qr in requirements)
        {
            for (int i = 0; i < itemsCopy.Length; i++)
            {
                if (itemsCopy[i] != null) { 
                    if (qr.Check(itemsCopy[i]))
                    {
                        itemsCopy[i] = null;
                    }
                }
            }
            for (int i = 0; i < itemsCopy.Length; i++)
            {
                if (itemsCopy[i] != null)
                {
                    success = false;
                    break;
                }
            }
        }
        return success;
    }*/
    public void Clear()
    {
        for (int i = 0; i < questItemContainer.childCount; i++)
        {
            questItemContainer.GetChild(i).GetComponent<QuestItem>().Kill();
        }
    }

    public void Init(Quest quest)
    {
        int level = 0;
        float step = -40f;
        foreach (QuestRequirement qr in quest.Requirements)
        {
            QuestItem qi = Instantiate<QuestItem>(questItemPrefab) as QuestItem;
            qi.Init(qr);
            qi.transform.position = new Vector3(0f, level * step, 0f);
            qi.transform.SetParent(questItemContainer, false);
            level++;
        }
    }

    public void FitsRequirement(Ingredient ingredient)
    {
        for (int i = 0; i < questItemContainer.childCount; i++)
        {
            QuestItem qi = questItemContainer.GetChild(i).GetComponent<QuestItem>();
            if (qi.IsChecked)
            {
                continue;
            }
            else if (qi.Check(ingredient))
            {
                break;
            }
        }
    }

    public bool QuestComplete()
    {
        for (int i = 0; i < questItemContainer.childCount; i++)
        {
            if (!questItemContainer.GetChild(i).GetComponent<QuestItem>().IsChecked)
            {
                return false;
            }
        }
        return true;
    }

    public List<QuestItem> GetQuestItems()
    {
        List<QuestItem> questItems = new List<QuestItem>();

        for (int i = 0; i < questItemContainer.childCount; i++)
        {
            questItems.Add(questItemContainer.GetChild(i).GetComponent<QuestItem>());
        }

        return questItems;
    }

    public void Show()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().enabled = true;
            }
            else if (child.GetComponent<Text>() != null)
            {
                child.GetComponent<Text>().enabled = true;
            }
            for (int i = 0; i < questItemContainer.childCount; i++)
            {
                questItemContainer.GetChild(i).GetComponent<QuestItem>().Show();
            }
        }
        txtComponent.enabled = true;
    }

    public void Hide()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().enabled = false;
            }
            else if (child.GetComponent<Text>() != null)
            {
                child.GetComponent<Text>().enabled = false;
            }
            for (int i = 0; i < questItemContainer.childCount; i++)
            {
                questItemContainer.GetChild(i).GetComponent<QuestItem>().Hide();
            }
        }
        txtComponent.enabled = false;
    }

    void Update()
    {

    }


}
