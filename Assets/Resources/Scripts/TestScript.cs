using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 5f * Time.deltaTime;
    }
}
