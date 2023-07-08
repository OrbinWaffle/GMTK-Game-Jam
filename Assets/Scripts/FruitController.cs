using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField] float ejectionForce = 1f;
    [SerializeField] float fruitDelay = 1f;
    [SerializeField] int queueSize = 5;
    [SerializeField] int queueDistance = 3;
    [SerializeField] GameObject[] FruitPool;
    public Queue<GameObject> FruitQueue;
    public Queue<GameObject> VisualFruitQueue;
    [SerializeField] Transform queueLocation;
    [SerializeField] Transform[] pipes;

    float nextFruitTime = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        FruitQueue = new Queue<GameObject>();
        VisualFruitQueue = new Queue<GameObject>();
        for(int i = 0; i < queueSize; ++i)
        {
            AddToQueue(queueLocation.position + Vector3.up * i * 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextFruitTime)
        {
            AddToQueue();
            SpawnFruit();
            nextFruitTime = Time.time + fruitDelay;
        }
    }
    public void AddToQueue()
    {
        GameObject selectedFruit = FruitPool[Random.Range(0, FruitPool.Length)];
        GameObject pipeFruit = Instantiate(selectedFruit, queueLocation.position, Quaternion.identity);
        FruitQueue.Enqueue(selectedFruit);
        VisualFruitQueue.Enqueue(pipeFruit);
    }
    public void AddToQueue(Vector3 location)
    {
        GameObject selectedFruit = FruitPool[Random.Range(0, FruitPool.Length)];
        GameObject pipeFruit = Instantiate(selectedFruit, location, Quaternion.identity);
        FruitQueue.Enqueue(selectedFruit);
        VisualFruitQueue.Enqueue(pipeFruit);
    }
    public void SpawnFruit()
    {
        GameObject selectedFruit = FruitQueue.Dequeue();
        Transform pipeToUse = pipes[Random.Range(0, pipes.Length)];
        GameObject fruitInstance = Instantiate(selectedFruit, pipeToUse.position, Quaternion.identity);
        fruitInstance.GetComponent<Rigidbody>().AddForce(pipeToUse.up * ejectionForce, ForceMode.Impulse);
        Destroy(VisualFruitQueue.Peek());
        VisualFruitQueue.Dequeue();
    }
    public void DisplayQueue()
    {
    }
}
