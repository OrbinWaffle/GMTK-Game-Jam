using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRange : MonoBehaviour
{
    public NinjaSlash ninja;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Fruit"){
            print("TESTINGG");
        }

        ninja.addFruit(other.gameObject);
        //ninja.transform.position = other.transform.position;
    }
}
