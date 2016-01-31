// Date   : 30.01.2016 15:54
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestItem : MonoBehaviour
{

    [SerializeField]
    private QuestRequirement questRequirement;
    public QuestRequirement QuestRequirement { get { return questRequirement; } }

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Image checkMark;

    [SerializeField]
    private Image itemSprite;

    [SerializeField]
    private Image checkBox;

    private bool isChecked = false;
    public bool IsChecked { get { return isChecked; } }

    public void Init(QuestRequirement questRequirement)
    {
        this.questRequirement = questRequirement;
        txtComponent.text = questRequirement.Description;
    }

    public bool Check(Ingredient ingredient, bool check = true)
    {
        if (questRequirement.Check(ingredient))
        {
            if (check)
            {
                checkMark.enabled = true;
                isChecked = true;
            }
            return true;
        }
        return false;
    }

    public void Kill()
    {
        Debug.Log("Kill questlog!");
        Destroy(gameObject);
    }

    public void Hide()
    {
        txtComponent.enabled = false;
        imgComponent.enabled = false;
        checkMark.enabled = false;
        itemSprite.enabled = false;
        checkBox.enabled = false;
    }

    public void Show()
    {
        txtComponent.enabled = true;
        imgComponent.enabled = true;
        checkMark.enabled = isChecked;
        itemSprite.enabled = true;
        checkBox.enabled = true;
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
