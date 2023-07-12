using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    public static event Action OnDied;

    [SerializeField] float speed = 1f;
    [SerializeField] float force;

    [SerializeField] bool playerAlive = true;
    [SerializeField] bool gameStart = false;
    [SerializeField] bool jumped = false;
    [SerializeField] bool jumping = false;
    [SerializeField] float moveMultiplier = 5f;
    [SerializeField] float moveAmount = 0;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;
    [SerializeField] private float playeHeight;

    private int moveDir = 1;
    //private Transform childTransform;

    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Update()
    {
        //childTransform = transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameStart = true;
        }
        if (gameStart && playerAlive)
        {
            Movement(this.transform);

            if (Physics.Raycast(this.transform.position, Vector3.down, .01f, floorMask))
            {
                if (Input.GetKeyDown(KeyCode.Space) && !jumping)
                {
                    Jump(jumped);
                }
            }
            else
            {
                jumping = false;
            }

        }
        IsDied(this.transform);
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
        transform.position += transform.forward * speed * Time.deltaTime;
        moveAmount = moveMultiplier * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * -moveDir * moveAmount, .1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * moveDir * moveAmount, .1f);
        }
    }

    private void Jump(bool _jumped)
    {
        jumped = _jumped;
        if (!jumped)
        {
            rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
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
