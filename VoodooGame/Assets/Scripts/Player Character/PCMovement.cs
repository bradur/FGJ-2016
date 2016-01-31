// Date   : 29.01.2016 21:23
// Project: VoodooGame

using UnityEngine;
using System.Collections;

public class PCMovement : MonoBehaviour
{

    [SerializeField]
    [Range(0.5f, 5f)]
    private float speed = 1f;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float speedFactor = 0.5f;

    private Vector2 targetSpeed;
    private Rigidbody2D rigidBody2D;

    private bool waitForConfirm = false;
    private bool noMoving = false;
    private bool processConfirm = false;

    private float waitTime = 0.4f;
    private float timer = 0f;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!waitForConfirm || !noMoving)
        {
            float move_h = Input.GetAxis("Horizontal");
            float move_v = Input.GetAxis("Vertical");

            targetSpeed = new Vector2(speed * move_h, speed * move_v);

            rigidBody2D.AddForce(speedFactor * (targetSpeed - rigidBody2D.velocity), ForceMode2D.Impulse);
        }
    }

    public void AllowConfirm()
    {
        processConfirm = true;
    }

    public void DisallowConfirm()
    {
        processConfirm = false;
    }

    void Update()
    {
        if (processConfirm)
        {
            if (waitForConfirm)
            {
                if (Input.GetKeyUp(GameManager.main.ConfirmKey))
                {
                    HUDManager.main.CloseHomeScreen();
                    waitForConfirm = false;
                    StartCoroutine(CheckGameOver());
                }
            }
            if (noMoving)
            {
                if (timer > waitTime)
                {
                    if (Input.GetKeyUp(GameManager.main.ConfirmKey))
                    {
                        Application.LoadLevel(0);
                    }
                    if (Input.GetKeyUp(GameManager.main.ExitKey))
                    {
                        Application.Quit();
                    }
                    timer = 0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(5);

        HUDManager.main.CheckGameOver();
        yield return null;

    }

    public void DisallowMovement()
    {
        Stop();
        noMoving = true;
    }

    public void Stop()
    {
        rigidBody2D.velocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Home")
        {
            HUDManager.main.OpenHomeScreen();
            waitForConfirm = true;
            rigidBody2D.velocity = Vector3.zero;
        }

    }
}
