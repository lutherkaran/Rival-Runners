using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameInput))]

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public static event Action OnDied;

    [SerializeField] float speed = 1f;
    [SerializeField] float force;

    [SerializeField] float moveMultiplier = 1f;
    [SerializeField] float moveAmount = 0;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;

    [SerializeField] bool playerAlive = true;
    [SerializeField] bool jumped = false;

    public bool jumping { get; private set; }
    public bool playerStarted { get; private set; }

    private float playeHeight;
    private int moveDir = 1;

    private GameInput gameInput;

    private Rigidbody rb;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();
        gameInput = GetComponent<GameInput>();
        playerStarted = false;

        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void FixedUpdate()
    {
        if (playerStarted)
        {
            if (GameMenuManager.Instance.timer.isCountDownOver()==0)
            {
                Movement(this.transform);
            }
        }
    }

    private void Update()
    {
        if (gameInput.Started()) // touchInput // change it to touch anywhere on the screen
        {
            playerStarted = true;
            GameMenuManager.Instance.gameStart = playerStarted;
        }

        if (playerStarted)
        {
            if (playerAlive)
            {
                if (gameInput.jump && !jumping)
                {
                    if (Physics.Raycast(this.transform.position, Vector3.down, .01f, floorMask))
                    {
                        Jump(jumped);
                    }
                }

                else
                {
                    jumping = false;
                }

                IsDied(this.transform);
            }
        }
    }

    private void IsDied(Transform transform)
    {
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playeHeight, 0.1f, transform.forward, 0.3f, deathMask))
        {
            playerAlive = false;
            OnDied?.Invoke();
        }
    }

    private void Movement(Transform transform)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        moveAmount = moveMultiplier * speed * Time.deltaTime;

        if (gameInput.move.x == -1)
        {
            transform.position += transform.right * -moveDir * moveAmount;
        }
        else if (gameInput.move.x == 1)
        {
            transform.position += transform.right * moveDir * moveAmount;
        }
    }

    private void Jump(bool _jumped)
    {
        jumped = _jumped;
        if (!jumped)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            jumping = true;
        }
    }

}
