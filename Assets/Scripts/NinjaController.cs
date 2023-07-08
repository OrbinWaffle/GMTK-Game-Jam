using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject slashPrefab;
    List<GameObject> fruitList;
    CharacterController CC;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        fruitList = new List<GameObject>();
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        
        // timer += Time.deltaTime;

        // if (timer >= 1f){
        //     Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x - 9, transform.position.x + 9),
        //      Random.Range(transform.position.y - 1, transform.position.y + 9), transform.position.z - 2);

        //      Instantiate(slashPrefab, randomSpawnPosition, Quaternion.identity);
            
        //     timer = 0f;
        // }

        /*
        if(Input.GetKeyDown(KeyCode.F)){
            //Vector3 randomSpawnPosition = new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1));
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x - 9, transform.position.x + 9),
             Random.Range(transform.position.y - 1, transform.position.y + 9), transform.position.z - 2);

            Instantiate(slashPrefab, randomSpawnPosition, Quaternion.identity);
        }
        */
    }
    void FixedUpdate()
    {        
        GameObject closest = FindClosest(fruitList);
        if(!closest)
        {
            return;
        }
        Vector3 targetVector = (closest.transform.position - transform.position).normalized;
        if(targetVector.x > 0)
        {
            CC.Move(Vector3.right * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            CC.Move(-Vector3.right * moveSpeed * Time.fixedDeltaTime);
        }
    }

    GameObject FindClosest(List<GameObject> list)
    {
        if(list.Count == 0)
        {
            return null;
        }
        GameObject currClosest = list[0];
        float closestDist = Vector3.Distance(currClosest.transform.position, transform.position);
        foreach (GameObject fruit in fruitList)
        {
            float dist = Vector3.Distance(fruit.transform.position, transform.position);
            if(dist < closestDist)
            {
                currClosest = fruit;
                closestDist = dist;
            }
        }
        return currClosest;
    }
    public void AddFruit(GameObject fruit)
    {
        fruitList.Add(fruit);
    }
    public void RemoveFruit(GameObject fruit)
    {
        fruitList.Remove(fruit);
    }
}
