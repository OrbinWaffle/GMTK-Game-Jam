using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] NinjaController NC;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            NC.AddFruit(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            NC.RemoveFruit(other.gameObject);
        }
    }
}
