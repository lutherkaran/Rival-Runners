using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    [SerializeField] float speed = 1f;
    [SerializeField] float force;

    [SerializeField] bool playerAlive = true;
    [SerializeField] bool gameStart = false;
    [SerializeField] bool jumped = false;
    [SerializeField] float moveDistance = .1f;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;
    [SerializeField] private float playeHeight;

    private int moveDir = 1;

    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStart = true;
        }
        if (gameStart && playerAlive)
        {
            Movement();
            if (Input.GetKeyDown(KeyCode.Space) && !jumped)
            {
                jumped = true;
            }
        }
        IsDied();
    }

    private void IsDied()
    {
        Debug.DrawRay(transform.GetChild(0).position, transform.GetChild(0).transform.forward, Color.red);
        if (Physics.CapsuleCast(transform.GetChild(0).position, transform.GetChild(0).position + Vector3.up * playeHeight, 0.1f, transform.GetChild(0).transform.forward, 0.3f, deathMask))
        {
            playerAlive = false;
        }
    }

    private void Movement()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * -moveDir * moveDistance / 100, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * moveDir * moveDistance / 100, 1);
        }
        if (jumped)
        {
            rb.AddForce(this.transform.GetChild(0).position.y * Vector3.up * force * Time.fixedDeltaTime, ForceMode.Impulse);

            if (Physics.Raycast(transform.GetChild(0).position, Vector3.down, 3f, floorMask))
            {
                jumped = false;
            }
        }
    }
    public bool GameStart()
    {
        return gameStart;
    }

    public bool Jumped()
    {
        return jumped;
    }
    public bool PlayerAlive()
    {
        return playerAlive;
    }
}
