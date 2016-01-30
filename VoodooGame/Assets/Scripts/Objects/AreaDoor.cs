// Date   : 31.01.2016 01:06
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class AreaDoor : MonoBehaviour {

    [SerializeField]
    [Range(0, 3)]
    private int area;

    [SerializeField]
    private Transform playerPosition;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameManager.main.SetNextArea(area, playerPosition.position);
            HUDManager.main.AreaTransition();
        }
    }

}
