using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour{
    public GameObject itemInstance;
    [SerializeField] int lifespan;
    int points;



    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLifespan(){
        StartCoroutine(Lifespan());
    }

    public IEnumerator Lifespan() {
        Debug.Log("Lifetime Started");

        yield return new WaitForSeconds(lifespan);

        Destroy(itemInstance);
    }
}
