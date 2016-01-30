// Date   : 30.01.2016 15:22
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestUI : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private List<QuestRequirement> requirements;
    public List<QuestRequirement> Requirements { get { return requirements; } }

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

    public void Init(List<QuestRequirement> reqs)
    {
        int level = 0;
        float step = -80f;
        foreach(QuestRequirement qr in reqs){
            QuestItem qi = Instantiate<QuestItem>(questItemPrefab) as QuestItem;
            qi.Init(qr);
            qi.transform.position = new Vector3(0f, level*step, 0f);
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
                Debug.Log(questItemContainer.GetChild(i).GetComponent<QuestItem>().name + " IS NOT COMPLETE!");
                return false;
            }
        }
        return true;
    }

    void Start () {
        Init(requirements);
    }

    void Update () {
    
    }


}
