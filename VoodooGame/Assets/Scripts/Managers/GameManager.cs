// Date   : 30.01.2016 12:48
// Project: VoodooGame

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    [SerializeField]
    private KeyCode pickupKey = KeyCode.Space;
    public KeyCode PickupKey { get { return pickupKey; } }


    [SerializeField]
    private KeyCode confirmKey = KeyCode.Return;
    public KeyCode ConfirmKey { get { return confirmKey; } }

    [SerializeField]
    private Material killOutline;
    public Material KillOutline { get { return killOutline; } }

    [SerializeField]
    private Material stealOutline;
    public Material StealOutline { get { return stealOutline; } }

    [SerializeField]
    private Material buyOutline;
    public Material BuyOutline { get { return buyOutline; } }

    [SerializeField]
    private Material digOutline;
    public Material DigOutline { get { return digOutline; } }


    [SerializeField]
    private List<GameObject> areas = new List<GameObject>();
    [SerializeField]
    private GameObject currentArea;

    [SerializeField]
    private GameObject player;
    public GameObject Player { get { return player; } }

    [SerializeField]
    private int gold;
    public int Gold { get { return gold; } set { gold = value; } }
    public bool StealMode { get; set; }
    public bool KillMode { get; set; }
    public bool BuyMode { get; set; }
    public bool DigMode { get; set; }

    private int nextAreaNumber;
    private Vector3 playerPos;

    void Awake()
    {

        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            this.tag = "GameManager";
            main = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetNextArea(int areaNumber, Vector3 playerpos)
    {
        nextAreaNumber = areaNumber;
        this.playerPos = playerpos;
    }

    public void OpenNextArea()
    {
        player.transform.position = this.playerPos;
        currentArea.SetActive(false);
        currentArea = areas[nextAreaNumber];
        foreach (Transform child in currentArea.transform)
        {
            if (child.GetComponent<Entity>() != null)
            {
                child.GetComponent<Entity>().StartMovingAgain();
            }
        }
        currentArea.SetActive(true);
    }

    void Start () {
        HUDManager.main.UpdateGold(gold);
    }

    void Update () {
    
    }
}
