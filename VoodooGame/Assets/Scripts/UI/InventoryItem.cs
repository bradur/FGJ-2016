// Date   : 30.01.2016 10:15
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Ingredient ingredient;
    public Ingredient Ingredient { get { return ingredient; } }

    private GameObject pickup;

    void Start () {
    
    }

    void Update () {
    
    }

    public void Kill()
    {
        if (pickup != null)
        {
            Entity entity = pickup.GetComponent<Entity>();
            if (entity != null)
            {
                entity.DeleteStealItems();
            }
            else
            {
                Destroy(pickup.gameObject);
            }
        }
        Destroy(gameObject);
    }

    public void Init(Ingredient ingredient)
    {
        //imgComponent.sprite = ingredient.Sprite;
        if (ingredient.AnimalType != AnimalType.None) { 
            txtComponent.text = ingredient.AnimalType + " " + ingredient.AnimalState;
        }
        else
        {
            txtComponent.text = ingredient.GatheredState + " " + ingredient.GatheredType;
        }
        this.ingredient = ingredient;
    }

    public void Init(Ingredient ingredient, GameObject pickup)
    {
        Init(ingredient);
        this.pickup = pickup;
    }
}
