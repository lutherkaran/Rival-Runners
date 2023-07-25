using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 0.7f, -2f);
    [SerializeField]Transform playerTarget;

    private void LateUpdate()
    {
        if (playerTarget)
        {
            //this.transform.position = playerTarget.position + offset;
        }
    }
}
