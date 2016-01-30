// Date   : 30.01.2016 02:52
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class PickupIngredient : MonoBehaviour {
    [SerializeField]
    Ingredient ingredient;
    
    void Start () {
    }

    public void Init(Ingredient ingredient) {
        this.ingredient = ingredient;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ingredient.Sprite;
    }

    void Update () {
    
    }

    void Pickup()
    {
        //HUDManager.main.AddItemToInventory(ingredient);
    }
}
