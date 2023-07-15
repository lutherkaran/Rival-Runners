using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public static event Action OnDied;

    [SerializeField] float speed = 1f;
    [SerializeField] float force;

    [SerializeField] bool playerAlive = true;
    [SerializeField] bool gameStart = false;
    [SerializeField] bool jumped = false;
    [SerializeField] bool jumping = false;
    [SerializeField] float moveMultiplier = 1f;
    [SerializeField] float moveAmount = 0;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;
    [SerializeField] private float playeHeight;

    private int moveDir = 1;
    //private Transform childTransform;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Update()
    {
        //childTransform = transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.K)) // touchInput
        {
            gameStart = true;
        }
        if (gameStart && playerAlive)
        {
            Movement(this.transform);

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
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

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -moveDir * moveAmount;
        }
        else if (Input.GetKey(KeyCode.D))
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

    public bool GameStart()
    {
        return gameStart;
    }

    public bool Jumping()
    {
        return jumping;
    }
}
