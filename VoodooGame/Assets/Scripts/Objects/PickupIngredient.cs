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
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (ingredient.GatheredState != GatheredState.None && ingredient.GatheredState != GatheredState.Any)
        {
            spriteRenderer.color = GetIngredientColor(ingredient);
        }
    }

    public void Init(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        GetComponent<SpriteRenderer>().sprite = ingredient.Sprite;
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
            Entity entity = GetComponent<Entity>();
            if (entity != null)
            {
                entity.StopMovement();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HUDManager.main.RemoveWorldDialogIngredient(ingredient);
            Entity entity = GetComponent<Entity>();
            if (entity != null)
            {
                entity.StartMovement();
            }
        }
    }

    public Color GetIngredientColor(Ingredient ingredient)
    {
        if (ingredient.GatheredState != GatheredState.None && ingredient.GatheredState != GatheredState.Any)
        {
            return GameManager.main.ingredientColors[(int)ingredient.GatheredState];
        }

        return Color.yellow;
    }
}
