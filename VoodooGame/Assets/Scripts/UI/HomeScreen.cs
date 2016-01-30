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
    private List<QuestUI> quests;

    public void CheckOrStartQuest(bool isComplete)
    {
        if (isComplete)
        {
            HUDManager.main.SetNextQuest(quests[0].Requirements);
            txtComponent.text = "Quest complete!";
        }
        else
        {
            txtComponent.text = "Your quest is not yet complete!";
        }
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
