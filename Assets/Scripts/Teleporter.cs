using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            other.transform.position = new Vector3(Mathf.Clamp(other.transform.position.x, -16.5f, 16.5f), transform.position.y + 1, 6);
        }
    }
}
