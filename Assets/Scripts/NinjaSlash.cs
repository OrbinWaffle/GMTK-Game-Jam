using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSlash : MonoBehaviour
{

    public GameObject slashPrefab;
    private float timer = 0f;
    private List<GameObject> fruitList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f){
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x - 9, transform.position.x + 9),
             Random.Range(transform.position.y - 1, transform.position.y + 9), transform.position.z - 2);

             Instantiate(slashPrefab, randomSpawnPosition, Quaternion.identity);
            
            timer = 0f;
        }

        for (int i = 0; i < fruitList.Count; i++){
            Destroy(fruitList[i]);
        }

        /*
        if(Input.GetKeyDown(KeyCode.F)){
            //Vector3 randomSpawnPosition = new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1));
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x - 9, transform.position.x + 9),
             Random.Range(transform.position.y - 1, transform.position.y + 9), transform.position.z - 2);

            Instantiate(slashPrefab, randomSpawnPosition, Quaternion.identity);
        }
        */
    }

    public void addFruit(GameObject fruit){
        fruitList.Add(fruit);
    }
}
