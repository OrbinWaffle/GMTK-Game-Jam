using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField] GameObject[] FruitPool;
    List<GameObject> FruitQueue;
    
    // Start is called before the first frame update
    void Start()
    {
        FruitQueue = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToQueue()
    {
        GameObject selectedFruit = FruitPool[Random.Range(0, FruitPool.Length)];
        GameObject fruitInstance = Instantiate(selectedFruit);
        FruitQueue.Add(fruitInstance);
    }
}
