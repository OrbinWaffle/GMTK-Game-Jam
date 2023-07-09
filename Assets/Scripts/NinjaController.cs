using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed = 5f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float minReactionTime = .5f;
    [SerializeField] float maxReactionTime = 2f;
    [SerializeField] private float slashRange = 3f;
    [SerializeField] private float slashCooldown = 1f;
    [SerializeField] float gravity = 9.81f;
    float verticalVelocity = 0f;
    [SerializeField] GameObject slashPrefab;
    List<GameObject> fruitList;
    CharacterController CC;
    Animator anim;
    private float movement = 0;
    private float nextSlashTime = 0f;
    private float timer = 0f;
    float nextGroundCheckTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        fruitList = new List<GameObject>();
        CC = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
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
        UpdateAnimations();
    }
    void UpdateAnimations()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(movement));
        anim.SetBool("isGrounded", CC.isGrounded);
    }
    void FixedUpdate()
    {        
        GameObject closest = FindClosest(fruitList);
        if(closest)
        {
            Vector3 targetVector = (closest.transform.position - transform.position).normalized;
            movement = Mathf.Clamp(targetVector.x * moveSpeed, -maxMoveSpeed, maxMoveSpeed);
        }

        if(CC.isGrounded && Time.time > nextGroundCheckTime)
        {
            verticalVelocity = 0f;
            if(Time.time < nextSlashTime)
            {
                movement = 0;
            }

            else if(!closest)
            {
                movement = 0;
                RotateNinja(Vector3.forward);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        }

        if(CC.isGrounded && Time.time >= nextSlashTime && closest && Vector3.Distance(transform.position, closest.transform.position) < slashRange)
        {
            Attack(closest);
        }
        CC.Move(Vector3.right * movement * Time.deltaTime + Vector3.up * verticalVelocity * Time.fixedDeltaTime);
        if(closest)
        {
            RotateNinja(-(Vector3.right * movement).normalized);
        }
    }

    GameObject FindClosest(List<GameObject> list)
    {
        if(list.Count == 0)
        {
            return null;
        }
        GameObject currClosest = null;
        float closestDist = Mathf.Infinity;
        for(int i = 0; i < fruitList.Count; ++i)
        {
            GameObject fruit = fruitList[i];
            if(fruit == null)
            {
                fruitList.RemoveAt(i);
                --i;
                continue;
            }
            float dist = Vector3.Distance(fruit.transform.position, transform.position);
            if(dist < closestDist)
            {
                currClosest = fruit;
                closestDist = dist;
            }
        }
        return currClosest;
    }
    public void OnExploded()
    {
        anim.SetTrigger("knockedBack");
        
        verticalVelocity = 10;
        movement = Random.Range(-.5f, .5f) * moveSpeed;
        nextGroundCheckTime = Time.time + 0.1f;
    }
    void Attack(GameObject objToAttack)
    {
        GameObject fruitToKill = objToAttack;
        Instantiate(slashPrefab, fruitToKill.transform.position, Quaternion.Euler(0,0,Random.Range(0,256)));
        RemoveFruit(objToAttack);
        objToAttack.GetComponent<ItemParent>().KillMe();
        anim.SetTrigger("attack");
        nextSlashTime = Time.time + slashCooldown;
    }
    public void RotateNinja(Vector3 vectorToRotateTowards){
        Quaternion targetRotation = Quaternion.LookRotation(vectorToRotateTowards, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    public void AddFruit(GameObject fruit)
    {
        StartCoroutine(AddFruitDelay(fruit));
    }
    IEnumerator AddFruitDelay(GameObject fruit)
    {
        yield return new WaitForSeconds(Random.Range(minReactionTime, maxReactionTime));
        fruitList.Add(fruit);
    }
    public void RemoveFruit(GameObject fruit)
    {
        fruitList.Remove(fruit);
    }
}
