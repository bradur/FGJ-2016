// Date   : 30.01.2016 20:17
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HomeScreen : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private List<Quest> quests;

    [SerializeField]
    private Text questTitletxt;

    [SerializeField]
    private Text questStorytxt;

    int questnum = 0;

    public void CheckOrStartQuest(bool isComplete)
    {
        if (isComplete)
        {
            if (questnum > 0)
            {
                txtComponent.text = "Quest complete!";
            }
            quests.RemoveAt(0);
            if (quests.Count > 0) {
                questTitletxt.text = quests[0].Title;
                questStorytxt.text = quests[0].Description;
                HUDManager.main.SetNextQuest(quests[0]);
            }
            else
            {
                txtComponent.text = "The end.";
            }
            questnum++;
        }
        else
        {
            txtComponent.text = "Your quest is not yet complete!";
        }
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
