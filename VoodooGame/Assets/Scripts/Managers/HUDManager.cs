// Date   : 29.01.2016 23:25
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HUDManager : MonoBehaviour
{

    public static HUDManager main;

    [SerializeField]
    private Transform screenParent;
    [SerializeField]
    private Transform worldParent;

    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    [Range(10, 50)]
    private int poolSize = 10;

    [SerializeField]
    private Dialog dialogPrefab;

    [SerializeField]
    private WorldDialog worldDialogPrefab;

    private WorldDialog currentWorldDialog;

    private List<Dialog> dialogList = null;

    [SerializeField]
    private Text stealButtonTxt;

    [SerializeField]
    private Text killButtonTxt;

    [SerializeField]
    private Text digButtonTxt;

    [SerializeField]
    private Text buyButtonTxt;

    [SerializeField]
    private QuestUI questUI;

    [SerializeField]
    private GameObject buttonContainer;

    [SerializeField]
    private HomeScreen homeScreen;

    [SerializeField]
    private HomeScreen gameoverScreen;

    [SerializeField]
    private AreaTransitionEffect areaTransitionEffect;

    [SerializeField]
    private Text goldDisplaytxt;
    [SerializeField]
    private Text goldDisplayTitletxt;
    [SerializeField]
    private Image goldDisplayimg;
    [SerializeField]
    private Image goldDisplaybg;
    private bool waitForEndKey = false;

    int num = 0;

    void Awake()
    {

        if (GameObject.FindGameObjectsWithTag("HUDManager").Length > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            this.tag = "HUDManager";
            main = this;
        }

    }



    void Start()
    {

    }

    void Update()
    {

        if (waitForEndKey)
        {
            if (Input.GetKeyUp(GameManager.main.ConfirmKey))
            {
                Application.Quit();
            }
            else if (Input.GetKeyUp(GameManager.main.PickupKey))
            {
                waitForEndKey = false;
                ClearPool();
            }
        }
        if (Input.GetKeyUp(GameManager.main.ExitKey))
        {
            ShowDialog("Really quit? Press enter. Press space to return", Vector3.zero, DialogType.SimpleDialog);
            waitForEndKey = true;
        }
    }

    public void UpdateGold(int newCount)
    {
        goldDisplaytxt.text = newCount + "";
    }


    public void OpenHomeScreen()
    {
        homeScreen.Show();
        homeScreen.CheckOrStartQuest(questUI.QuestComplete());
        questUI.Hide();
        goldDisplaytxt.enabled = false;
        goldDisplayTitletxt.enabled = false;
        goldDisplayimg.enabled = false;
        goldDisplaybg.enabled = false;

        buttonContainer.gameObject.SetActive(false);
        worldParent.gameObject.SetActive(false);
        inventoryManager.Hide();
    }

    public void CloseHomeScreen()
    {
        homeScreen.Hide();
        questUI.Show();

        goldDisplaytxt.enabled = true;
        goldDisplayTitletxt.enabled = true;
        goldDisplayimg.enabled = true;
        goldDisplaybg.enabled = true;

        buttonContainer.gameObject.SetActive(true);
        worldParent.gameObject.SetActive(true);
        inventoryManager.Show();
    }

    public void SetNextQuest(Quest quest)
    {
        questUI.Clear();
        inventoryManager.ClearInventory();
        questUI.Init(quest);
    }

    public void AddWorldDialogIngredient(Ingredient ingredientToAdd, GameObject pickup = null)
    {
        if (currentWorldDialog == null)
        {
            currentWorldDialog = Instantiate<WorldDialog>(worldDialogPrefab) as WorldDialog;
            currentWorldDialog.transform.SetParent(worldParent, false);
            currentWorldDialog.Init(
                "Press " + GameManager.main.PickupKey + " to pick up the items!",
                WorldDialogType.ItemPickup
            );
        }
        if (pickup != null)
        {
            Vector3 newpos = pickup.transform.position;
            newpos.y += 2.4f;
            currentWorldDialog.transform.position = newpos;
        }
        currentWorldDialog.AddIngredient(ingredientToAdd, pickup);
    }

    public void AddBuyWorldDialogIngredient(Ingredient ingredientToAdd, GameObject pickup = null)
    {
        if (currentWorldDialog == null)
        {
            currentWorldDialog = Instantiate<WorldDialog>(worldDialogPrefab) as WorldDialog;
            currentWorldDialog.transform.SetParent(worldParent, false);
            currentWorldDialog.Init(
                "Click to buy an item!",
                WorldDialogType.Shop
            );
        }

        Vector3 newpos = GameManager.main.Player.transform.position;
        newpos.y += 2.4f;
        currentWorldDialog.transform.position = newpos;

        currentWorldDialog.AddIngredient(ingredientToAdd, pickup);
    }

    public void RemoveWorldDialogIngredient(Ingredient ingredientToRemove)
    {
        if (currentWorldDialog != null)
        {
            currentWorldDialog.RemoveIngredient(ingredientToRemove);
        }
        else
        {
            Debug.Log("No world dialog!!");
        }
    }

    public void EmptyShop()
    {
        if (currentWorldDialog != null)
        {
            currentWorldDialog.Clear();
            currentWorldDialog.Hide();
        }

    }

    public void AreaTransition()
    {
        areaTransitionEffect.Show();
    }

    public void HideWorldDialog()
    {
        currentWorldDialog.Hide();
    }
    public void ClearWorldDialog()
    {
        currentWorldDialog.Clear();
    }

    public void ShowWorldDialog(string message, Vector3 position, WorldDialogType dialogType)
    {
        WorldDialog dialog = Instantiate<WorldDialog>(worldDialogPrefab) as WorldDialog;
        dialog.transform.position = position;
        dialog.Init(message, dialogType);
        dialog.gameObject.transform.SetParent(worldParent, false);
    }

    public void ShowDialog(string message, Vector3 position, DialogType dialogType)
    {
        Dialog dialog = GetDialog();
        dialog.gameObject.SetActive(true);
        dialog.Show(message, position, transform, dialogType);
    }

    private void InitDialogPool(int size, Dialog dialogPrefab)
    {
        dialogList = new List<Dialog>();
        for (int i = 0; i < size; i++)
        {
            Dialog dialog = Instantiate(dialogPrefab);
            dialog.gameObject.transform.SetParent(screenParent, false);
            dialogList.Add(dialog);
        }
    }

    public Dialog GetDialog()
    {
        if (dialogList == null)
        {
            InitDialogPool(1, dialogPrefab);
        }
        if (dialogList.Count > 0)
        {
            Dialog dialog = dialogList[0];
            dialogList.RemoveAt(0);
            return dialog;
        }
        return null;
    }

    public void DestroyDialog(Dialog dialog)
    {
        dialogList.Add(dialog);
    }

    public void ClearPool()
    {
        /*for (int i = dialogList.Count - 1; i < 0; i--)
        {
            dialogList[i].Clear();
        }*/
        foreach (Transform child in screenParent)
        {
            if (child.GetComponent<Dialog>() != null)
            {

                child.GetComponent<Dialog>().Clear();
            }
        }
        foreach (Transform child in worldParent)
        {
            if (child.GetComponent<Dialog>() != null)
            {
                child.GetComponent<Dialog>().Clear();
            }
        }
    }

    public void AddToInventory(Ingredient ingredient)
    {
        questUI.FitsRequirement(ingredient);
        inventoryManager.AddItem(ingredient);
    }

    public List<InventoryItem> GetInventoryContents()
    {
        return inventoryManager.GetInventoryContents();
    }

    public void ToggleKillMode()
    {
        DeactivateModes("Kill");

        List<Entity> entities = FindObjectsOfType<Entity>().ToList();
        if (!GameManager.main.KillMode)
        {
            entities.Where(x => x.Killable).ToList().ForEach(x => x.ShowOutline(GameManager.main.KillOutline));

            //activate clicking
            killButtonTxt.text = "Stop the massacre";
        }
        else
        {
            entities.ForEach(x => x.HideOutline());
            killButtonTxt.text = "Kill";
        }
        GameManager.main.KillMode = !GameManager.main.KillMode;
    }

    public void ToggleStealMode()
    {
        DeactivateModes("Steal");

        List<Entity> entities = FindObjectsOfType<Entity>().ToList();
        if (!GameManager.main.StealMode)
        {
            entities.Where(x => x.Stealable).ToList().ForEach(x => x.ShowOutline(GameManager.main.StealOutline));

            //activate clicking
            stealButtonTxt.text = "Stop stealing";
        }
        else
        {
            entities.ForEach(x => x.HideOutline());
            stealButtonTxt.text = "Steal";
        }
        GameManager.main.StealMode = !GameManager.main.StealMode;
    }

    public void ToggleBuyMode()
    {
        DeactivateModes("Buy");

        List<Entity> entities = FindObjectsOfType<Entity>().ToList();
        if (!GameManager.main.BuyMode)
        {
            entities.Where(x => x.Buyable).ToList().ForEach(x => x.ShowOutline(GameManager.main.BuyOutline));

            //activate clicking
            buyButtonTxt.text = "Stop buying";
        }
        else
        {
            entities.ForEach(x => x.HideOutline());
            buyButtonTxt.text = "Buy";
        }
        GameManager.main.BuyMode = !GameManager.main.BuyMode;
    }

    public void ToggleDigMode()
    {
        DeactivateModes("Dig");

        List<Entity> entities = FindObjectsOfType<Entity>().ToList();
        if (!GameManager.main.DigMode)
        {
            entities.Where(x => x.Diggable).ToList().ForEach(x => x.ShowOutline(GameManager.main.DigOutline));

            //activate clicking
            digButtonTxt.text = "Stop digging";
        }
        else
        {
            entities.ForEach(x => x.HideOutline());
            digButtonTxt.text = "Dig";
        }
        GameManager.main.DigMode = !GameManager.main.DigMode;
    }

    //toggles off all the modes that are on except nextMode
    private void DeactivateModes(string nextMode)
    {
        if (nextMode != "Buy" && GameManager.main.BuyMode)
        {
            ToggleBuyMode();
        }
        if (nextMode != "Steal" && GameManager.main.StealMode)
        {
            ToggleStealMode();
        }
        if (nextMode != "Kill" && GameManager.main.KillMode)
        {
            ToggleKillMode();
        }
        if (nextMode != "Dig" && GameManager.main.DigMode)
        {
            ToggleDigMode();
        }
    }

    public void CheckGameOver()
    {
        List<Ingredient> worldIngredients = GameManager.main.ListIngredients();
        List<QuestItem> requirements = questUI.GetQuestItems();

        bool allOK = true;

        foreach (QuestItem questItem in requirements)
        {
            bool itemOK = false;

            foreach (Ingredient ingredient in worldIngredients)
            {
                itemOK = itemOK || questItem.Check(ingredient, false);
            }

            allOK = allOK && itemOK;
        }

        if (!allOK)
        {
            GameOver();
        }
    }

    public void HideGameOver()
    {
        gameoverScreen.Hide();
    }

    private void GameOver()
    {
        gameoverScreen.Show(true);
        
        questUI.Hide();
        goldDisplaytxt.enabled = false;
        goldDisplayTitletxt.enabled = false;
        goldDisplayimg.enabled = false;
        goldDisplaybg.enabled = false;

        buttonContainer.gameObject.SetActive(false);
        worldParent.gameObject.SetActive(false);
        inventoryManager.Hide();
    }
}
