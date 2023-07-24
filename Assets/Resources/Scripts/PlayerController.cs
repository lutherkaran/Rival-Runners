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
    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;

    public bool jumping { get; private set; }
    public bool playerStarted { get; private set; }

    private float playeHeight;
    private float moveAmount = 0;
    private bool playerAlive = true;
    private bool jumped = false;

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

    private void Update()
    {
        if (gameInput.Started()) // touchInput // change it to touch anywhere on the screen
        {
            GameMenuManager.Instance.playerStarted = playerStarted = true;
        }

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

    private void FixedUpdate()
    {
        if (playerAlive && GameMenuManager.Instance.timer.GetRemainingTime() == 0)
        {
            Movement(this.transform);
        }
    }

    private void Movement(Transform transform)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        moveAmount = moveMultiplier * Time.deltaTime;

        if (gameInput.move.x == -1)
        {
            transform.Translate(-transform.right * moveAmount * Time.deltaTime);
        }
        else if (gameInput.move.x == 1)
        {
            transform.Translate(transform.right * moveAmount * Time.deltaTime);
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

    private void IsDied(Transform transform)
    {
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playeHeight, 0.1f, transform.forward, 0.3f, deathMask))
        {
            playerAlive = false;
            OnDied?.Invoke();
        }
    }

}
