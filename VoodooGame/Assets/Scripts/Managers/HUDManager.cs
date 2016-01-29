// Date   : 29.01.2016 23:25
// Project: VoodooGame

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

    public static HUDManager main;

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    void Awake ()
    {

        if (GameObject.FindGameObjectsWithTag("HUDManager").Length > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            this.tag = "HUDManager";
            main = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
