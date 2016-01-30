// Date   : 30.01.2016 15:54
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestItem : MonoBehaviour
{

    [SerializeField]
    private QuestRequirement questRequirement;

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Image checkMark;

    private bool isChecked = false;
    public bool IsChecked { get { return isChecked; } }

    public void Init(QuestRequirement questRequirement)
    {
        this.questRequirement = questRequirement;
        txtComponent.text = questRequirement.Description;
    }

    public bool Check(Ingredient ingredient)
    {
        if (questRequirement.Check(ingredient))
        {
            checkMark.enabled = true;
            return true;
        }
        return false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
