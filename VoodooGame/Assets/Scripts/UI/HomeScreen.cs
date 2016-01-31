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

    Quest currentQuest;

    int questnum = 0;
    private bool waitForConfirm = false;
    private bool waitBeforeActivate = false;
    private float timer = 0f;
    private float waitTime = 0.4f;

    public void CheckOrStartQuest(bool isComplete)
    {
        if (isComplete)
        {
            currentQuest = quests[0];

            quests.RemoveAt(0);
            if (quests.Count > 0) {
                if (questnum > 0)
                {
                    txtComponent.text = "Quest complete! Press Enter to continue.";
                    questStorytxt.text = currentQuest.EndDescription;
                    waitForConfirm = true;
                    GameManager.main.Player.GetComponent<PCMovement>().DisallowConfirm();
                }
                else
                {
                    NextQuest();
                }
            }
            else
            {
                questTitletxt.text = "The End";
                questStorytxt.text = "You have mastered the art of voodoo. Now go, my apprentice, and make the world yours!";
                txtComponent.text = "The end. You're now the voodoo master!";
            }
            questnum++;
        }
        else
        {
            txtComponent.text = "Your quest is not yet complete!";
        }
    }

    public void NextQuest()
    {
        questTitletxt.text = quests[0].Title;
        questStorytxt.text = quests[0].Description;
        HUDManager.main.SetNextQuest(quests[0]);
        waitForConfirm = false;
        waitBeforeActivate = true;
        txtComponent.text = "";
    }

    public void Show(bool isGameOver = false)
    {
        if (isGameOver)
        {
            GameManager.main.Player.GetComponent<PCMovement>().DisallowMovement();
        }
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    void Start () {
    }

    void Update () {
        if (waitForConfirm)
        {
            if (Input.GetKeyUp(GameManager.main.ConfirmKey))
            {
                NextQuest();
            }
        }
        if (waitBeforeActivate)
        {
            if (timer > waitTime)
            {
                GameManager.main.Player.GetComponent<PCMovement>().AllowConfirm();
                timer = 0f;
                waitBeforeActivate = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
            
        }
    }
}
