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
    [SerializeField] float moveDistance = .1f;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask deathMask;
    [SerializeField] private float playeHeight;

    private int moveDir = 1;
    private Transform childTransform;

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
        childTransform = transform.GetChild(0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStart = true;
        }
        if (gameStart && playerAlive)
        {
            Movement(childTransform);

            if (Physics.Raycast(childTransform.position, Vector3.down, .01f, floorMask))
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
        IsDied(childTransform);
    }

    private void IsDied(Transform childTransform)
    {
        if (Physics.CapsuleCast(childTransform.position, childTransform.position + Vector3.up * playeHeight, 0.1f, childTransform.forward, 0.3f, deathMask))
        {
            playerAlive = false;
            OnDied?.Invoke();
        }
        //if ((Physics.Raycast(childTransform.position, Vector3.down, 5f, floorMask)))
        //{
        //    //playerAlive = false;
        //}

    }

    private void Movement(Transform childTransform)
    {
        childTransform.position += childTransform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            childTransform.position = Vector3.Lerp(childTransform.position, childTransform.position + childTransform.right * -moveDir * moveDistance / 100, 1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            childTransform.position = Vector3.Lerp(childTransform.position, childTransform.position + childTransform.right * moveDir * moveDistance / 100, 1f);
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
