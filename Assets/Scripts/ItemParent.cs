using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour{
    public bool collisionEnabled = false;
    protected IEnumerator currentCoroutine;
    [SerializeField] GameObject deathObject;
    [SerializeField] protected int lifespan;
    [SerializeField] protected int points;
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start(){
        scoreManager = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update(){    
    }

    public void StartLifespan(){
        currentCoroutine = Lifespan();

        StartCoroutine(currentCoroutine);
    }

    public void CancelLifespan(){
        StopCoroutine(currentCoroutine);
    }

    public virtual IEnumerator Lifespan(){
        yield return new WaitForSeconds(lifespan);

        scoreManager.AddPoints(points * 2);

        KillMe();
    }

    public int GetPoints(){
        return points;
    }

    void OnCollisionEnter(Collision collison){
        if (collisionEnabled){
            if(!collison.gameObject.CompareTag("Fruit"))
            {
                KillMe();
            }
        }
    }
    public void OnExploded(){
        KillMe();
    }
    public void KillMe(){
        if(deathObject != null){
            Instantiate(deathObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
