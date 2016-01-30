// Date   : 30.01.2016 00:25
// Project: VoodooGame

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stealItems;

    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } }
    
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
            waypoints.Add(child.position);

            if (child.name == "Outline")
            {
                outline = child.gameObject;
                outline.GetComponent<Renderer>().enabled = false;
            }
        }

        currentWaypoint = waypoints[0];
        

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

        Ingredient meatIngredient = Resources.Load<Ingredient>(meat) as Ingredient;
        Ingredient bloodIngredient = Resources.Load<Ingredient>(blood) as Ingredient;
        Ingredient boneIngredient = Resources.Load<Ingredient>(bone) as Ingredient;

        PickupIngredient meatPI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        meatPI = Instantiate(meatPI, transform.position, Quaternion.identity) as PickupIngredient;
        meatPI.transform.SetParent(transform.parent, false);
        meatPI.Init(meatIngredient);

        PickupIngredient bloodPI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        bloodPI = Instantiate(bloodPI, transform.position, Quaternion.identity) as PickupIngredient;
        bloodPI.transform.SetParent(transform.parent, false);
        bloodPI.Init(bloodIngredient);

        PickupIngredient bonePI = Resources.Load<PickupIngredient>("pickupIngredient") as PickupIngredient;
        bonePI = Instantiate(bonePI, transform.position, Quaternion.identity) as PickupIngredient;
        bonePI.transform.SetParent(transform.parent, false);
        bonePI.Init(boneIngredient);

        idleMovement = false;

        outline.GetComponent<Renderer>().enabled = true;
    }

    public void Steal()
    {
        foreach (GameObject item in stealItems)
        {
            //HUDManager.main.addItemToInventory(item);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "sein�")
        {
            GetRandomWaypoint();
        }
        Kill();
        Debug.Log("Piu!");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Pam!");
    }
}
