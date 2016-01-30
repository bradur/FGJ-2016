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
    List<Ingredient> ingredients;
    private bool waitForAction = false;

    [SerializeField]
    private WorldDialogIngredient worldDialogIngredientPrefab;

    void Start () {
    
    }

    void Update () {
        if (waitForAction)
        {
            if (Input.GetKey(GameManager.main.PickupKey))
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    HUDManager.main.AddToInventory(ingredient);
                }
            }
        }
    }

    /*public void Init(string message, WorldDialogType dialogType, Shop shop)
    {

    }*/

    public void Init(string message, WorldDialogType dialogType)
    {
        txtComponent.text = message;
        this.dialogType = dialogType;
        if (this.dialogType == WorldDialogType.ItemPickup)
        {
            waitForAction = true;
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        WorldDialogIngredient worldDialogIngredient = Instantiate<WorldDialogIngredient>(worldDialogIngredientPrefab) as WorldDialogIngredient;
        //worldDialogIngredient.transform.position = 
        worldDialogIngredient.transform.SetParent(transform, false);
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        waitForAction = false;
    }

    public void AfterHide()
    {
        Destroy(gameObject);
    }
}
