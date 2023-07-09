using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider col in collidersInRange)
        {
            col.gameObject.SendMessage("OnExploded", SendMessageOptions.DontRequireReceiver);
        }
    }
}
