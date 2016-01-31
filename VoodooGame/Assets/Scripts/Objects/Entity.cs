// Date   : 30.01.2016 00:25
// Project: VoodooGame

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Entity : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of items that can be stolen. Use only for humans.")]
    private List<Ingredient> stealItems;

    [SerializeField]
    [Tooltip("List of items that can be dug from the ground. Use only for ground.")]
    private List<Ingredient> digItems;

    [SerializeField]
    [Tooltip("List of items that can be bought. Use only for humans.")]
    private List<Ingredient> buyItems;

    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } }

    [SerializeField]
    private bool killable;
    public bool Killable { get { return killable; } }

    [SerializeField]
    private bool buyable;
    public bool Buyable { get { return buyable; } }

    [SerializeField]
    private bool stealable;
    public bool Stealable { get { return stealable; } }

    [SerializeField]
    private bool diggable;
    public bool Diggable { get { return diggable; } }
    
    [SerializeField]
    [Range(0, 3f)]
    private float smoothTime = 0.3f;

    [SerializeField]
    private bool idleMovement;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rigidBody2D;
    private List<Vector3> waypoints;
    private Vector3 currentWaypoint;
    private float timeStill = 0f;
    private bool standStill = false;
    private float maxSpeed = 1.5f;

    private GameObject outline;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        waypoints = new List<Vector3>();

        //loop children, create waypoints and hide outline
        Vector3 player = transform.position;
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Outline")
            {
                outline = child.gameObject;
                outline.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                waypoints.Add(child.position);
            }
        }

        if (waypoints.Count > 0) { 
            currentWaypoint = waypoints[0];
        }

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!standStill && idleMovement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, currentWaypoint, ref velocity, smoothTime, maxSpeed);

            if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
            {
                StartCoroutine(StandStill());
            }
        }
    }

    public void StartMovingAgain()
    {
        standStill = false;
    }

    IEnumerator StandStill()
    {
        standStill = true;
        yield return new WaitForSeconds(timeStill);
        GetRandomWaypoint();
        timeStill = Random.Range(2f, 4f);
        standStill = false;
        yield return null;
    }

    private void GetRandomWaypoint()
    {
        Vector3 oldWaypoint = currentWaypoint;
        waypoints.Remove(currentWaypoint);
        currentWaypoint = waypoints[Random.Range(0, 2)];
        waypoints.Add(oldWaypoint);
    }

    public void Kill()
    {
        string meat = animalType.ToString() + "Meat";
        string blood = animalType.ToString() + "Blood";
        string bone = animalType.ToString() + "Bone";

        Ingredient meatIngredient = Resources.Load<Ingredient>("Ingredients/"+meat) as Ingredient;
        Ingredient bloodIngredient = Resources.Load<Ingredient>("Ingredients/" + blood) as Ingredient;
        Ingredient boneIngredient = Resources.Load<Ingredient>("Ingredients/" + bone) as Ingredient;

        PickupIngredient meatPI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        meatPI = Instantiate(meatPI, transform.localPosition, Quaternion.identity) as PickupIngredient;
        meatPI.transform.SetParent(transform.parent, false);
        meatPI.Init(meatIngredient);

        PickupIngredient bloodPI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        bloodPI = Instantiate(bloodPI, transform.localPosition, Quaternion.identity) as PickupIngredient;
        bloodPI.transform.SetParent(transform.parent, false);
        bloodPI.Init(bloodIngredient);

        PickupIngredient bonePI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        bonePI = Instantiate(bonePI, transform.localPosition, Quaternion.identity) as PickupIngredient;
        bonePI.transform.SetParent(transform.parent, false);
        bonePI.Init(boneIngredient);

        idleMovement = false;

        outline.GetComponent<Renderer>().enabled = true;
        Vector3 corpsePos = gameObject.transform.position;
        Destroy(gameObject);

        GameObject corpse = Resources.Load<GameObject>("Corpse") as GameObject;
        Instantiate(corpse, corpsePos, Quaternion.identity);

        HUDManager.main.CheckGameOver();
    }

    public void Steal()
    {
        foreach (Ingredient item in stealItems)
        {
            HUDManager.main.AddWorldDialogIngredient(item, gameObject);
        }
    }

    public void Dig()
    {
        foreach (Ingredient loot in digItems)
        {
            PickupIngredient pickup = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
            pickup = Instantiate(pickup, transform.localPosition, Quaternion.identity) as PickupIngredient;
            pickup.transform.SetParent(transform.parent, false);
            pickup.Init(loot);
        }

        outline.GetComponent<Renderer>().enabled = true;
        Vector3 corpsePos = gameObject.transform.localPosition;
        

        GameObject corpse = Resources.Load<GameObject>("DugGround") as GameObject;
        corpse = (GameObject)Instantiate(corpse, corpsePos, Quaternion.identity) as GameObject;
        corpse.transform.SetParent(transform.parent, false);

        Destroy(gameObject);
    }

    public void Buy()
    {
        foreach (Ingredient item in buyItems)
        {
            HUDManager.main.AddBuyWorldDialogIngredient(item, gameObject);
        }
    }

    public void ShowOutline(Material outlineMaterial)
    {
        Renderer renderer = outline.GetComponent<Renderer>();
        renderer.material = outlineMaterial;
        renderer.enabled = true;
    }

    public void HideOutline()
    {
        outline.GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "seinä")
        {
            GetRandomWaypoint();
        }
        //Steal();
        Debug.Log("Piu!");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Pam!");
    }

    public void DeleteStealItems()
    {
        stealItems.Clear();
    }

    void OnMouseUp()
    {
        if (GameManager.main.StealMode && stealable)
        {
            Debug.Log("STEAL");
            Steal();
            HUDManager.main.ToggleStealMode();
        }
        else if (GameManager.main.KillMode && killable)
        {
            Debug.Log("KILL");
            Kill();
            HUDManager.main.ToggleKillMode();
        }
        else if (GameManager.main.BuyMode && buyable)
        {
            Debug.Log("Buy");
            Buy();
            HUDManager.main.ToggleBuyMode();
        }
        else if (GameManager.main.DigMode && diggable)
        {
            Debug.Log("Dig");
            Dig();
            HUDManager.main.ToggleDigMode();
        }
    }

    public void DeleteBuyItem(Ingredient ingredient)
    {
        buyItems.Remove(ingredient);
    }

    public List<Ingredient> GetIngredients()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        PickupIngredient pickupIngredient = GetComponent<PickupIngredient>();
        if (pickupIngredient != null)
        {
            ingredients.Add(pickupIngredient.Ingredient);
        }
        if (animalType != AnimalType.None)
        {
            //killing
            string meat = animalType.ToString() + "Meat";
            string blood = animalType.ToString() + "Blood";
            string bone = animalType.ToString() + "Bone";

            Ingredient meatIngredient = Resources.Load<Ingredient>("Ingredients/" + meat) as Ingredient;
            Ingredient bloodIngredient = Resources.Load<Ingredient>("Ingredients/" + blood) as Ingredient;
            Ingredient boneIngredient = Resources.Load<Ingredient>("Ingredients/" + bone) as Ingredient;

            ingredients.Add(meatIngredient);
            ingredients.Add(bloodIngredient);
            ingredients.Add(boneIngredient);
        }

        ingredients.AddRange(stealItems);
        ingredients.AddRange(digItems);
        ingredients.AddRange(buyItems);

        return ingredients;
    }
}
