// Date   : 30.01.2016 11:44
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum WorldDialogType { Message, ItemPickup, Shop }

public class WorldDialog : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Animator animator;

    WorldDialogType dialogType = WorldDialogType.Message;
    private bool waitForAction = false;
    private bool hidden = false;

    [SerializeField]
    private InventoryManager dialogInventory;

    void Start () {
    
    }

    void Update () {
        if (waitForAction)
        {
            if (Input.GetKey(GameManager.main.PickupKey))
            {
                List<InventoryItem> items = dialogInventory.GetInventoryContents();
                for (int i = 0; i < items.Count; i++)
                {
                    InventoryItem item = items[i];
                    HUDManager.main.AddToInventory(item.Ingredient);
                }
                dialogInventory.ClearInventory();
                Hide();
            }            
        }
    }

    /*public void Init(string message, WorldDialogType dialogType, Shop shop)
    {

    }*/

    public void Init(string message, WorldDialogType dialogType)
    {
        Show();
        txtComponent.text = message;
        this.dialogType = dialogType;
        if (this.dialogType == WorldDialogType.ItemPickup)
        {
            waitForAction = true;
        }
        else if (this.dialogType == WorldDialogType.Shop)
        {
            waitForAction = false;
        }
    }

    public void AddIngredient(Ingredient ingredient, GameObject pickup = null)
    {
        if (hidden)
        {
            Show();
            if (this.dialogType == WorldDialogType.ItemPickup)
            {
                waitForAction = true;
            }
            else if (this.dialogType == WorldDialogType.Shop)
            {
                waitForAction = false;
            }
        }
        dialogInventory.AddItem(ingredient, pickup, this.dialogType == WorldDialogType.Shop);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        dialogInventory.Remove(ingredient);
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        hidden = false;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        hidden = true;
        waitForAction = false;
    }

    public void AfterHide()
    {
        Destroy(gameObject);
    }
}
