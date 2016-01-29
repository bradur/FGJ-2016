// Date   : 29.01.2016 21:23
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class PCMovement : MonoBehaviour {

    [SerializeField]
    [Range(0.5f, 5f)]
    private float speed = 1f;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float speedFactor = 0.5f;

    private Vector2 targetSpeed;
    private Rigidbody2D rigidBody2D;

    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float move_h = Input.GetAxis("Horizontal");
        float move_v = Input.GetAxis("Vertical");

        targetSpeed = new Vector2(speed * move_h, speed * move_v);

        rigidBody2D.AddForce(speedFactor* (targetSpeed - rigidBody2D.velocity), ForceMode2D.Impulse);
    }

    void Update () {
    
    }
}
