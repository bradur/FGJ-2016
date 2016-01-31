// Date   : 30.01.2016 02:52
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class PickupIngredient : MonoBehaviour
{
    [SerializeField]
    Ingredient ingredient;

    public Ingredient Ingredient { get { return ingredient; } }

    void Start()
    {
    }

    public void Init(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        //SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = ingredient.Sprite;
    }

    void Update()
    {

    }

    void Pickup()
    {
        //HUDManager.main.AddItemToInventory(ingredient);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HUDManager.main.AddWorldDialogIngredient(ingredient, gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("PIM!");
            HUDManager.main.RemoveWorldDialogIngredient(ingredient);
        }
    }
}
