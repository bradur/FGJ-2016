// Date   : 30.01.2016 12:48
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    [SerializeField]
    private KeyCode pickupKey = KeyCode.Space;
    public KeyCode PickupKey { get { return PickupKey; } }

    [SerializeField]
    private Material killOutline;
    public Material KillOutline { get { return killOutline; } }

    [SerializeField]
    private Material stealOutline;
    public Material StealOutline { get { return stealOutline; } }

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


    void Start () {
    
    }

    void Update () {
    
    }
}
