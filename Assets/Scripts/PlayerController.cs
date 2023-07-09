using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField] float moveSpeed = 0.25f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float sprintMultiplier = 1f;
    [SerializeField] float pickupRange = 1f;
    [SerializeField] Transform holdSpot;
    float groundCheckDelay = 0.25f;
    Vector2 moveVector;
    CharacterController CC;
    Animator anim;
    GameObject heldObj;
    float verticalVelocity = 0f;
    float nextGroundCheckTime = 0f;
    float startTime;
    [SerializeField] float minKickForce;
    [SerializeField] float maxKickForce;
    [SerializeField] float maxHoldTime;
    FruitBasket fruitBasket;

    // Start is called before the first frame update

    void Start(){
        CC = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        fruitBasket = GetComponent<FruitBasket>();
    }

    // Update is called once per frame
    void Update(){
        UpdateAnimations();
    }

    void UpdateAnimations(){
        anim.SetFloat("moveSpeed", moveVector.magnitude);
        anim.SetBool("isHolding", heldObj != null);
        // anim.SetBool("isGrounded", CC.isGrounded);
    }
    void FixedUpdate(){

        if(CC.isGrounded && Time.time > nextGroundCheckTime){
            verticalVelocity = 0f;
        }
        else{
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        }

        CC.Move(
            new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed * sprintMultiplier * Time.fixedDeltaTime
            + Vector3.up * verticalVelocity * Time.fixedDeltaTime
        );
    }
    public void UpdateMoveVector(Vector2 newVec){
        moveVector = newVec;

        if(newVec != Vector2.zero){
            RotatePlayer(new Vector3(moveVector.x, 0, moveVector.y));
        }
    }
    public void RotatePlayer(Vector3 vectorToRotateTowards){
        Quaternion targetRotation = Quaternion.LookRotation(vectorToRotateTowards, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void Jump(){
        nextGroundCheckTime = Time.time + groundCheckDelay;
        verticalVelocity = jumpSpeed;
    }

    public void Pickup(){
        if(heldObj){
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.transform.parent = null;
            heldObj.GetComponent<ItemParent>().StartLifespan();

            heldObj = null;
        }
        else{
            Collider[] colliders = Physics.OverlapSphere(holdSpot.position, pickupRange);

            foreach (Collider collider in colliders){
                if (collider.CompareTag("Fruit")) {
                    heldObj = collider.gameObject;

                    heldObj.transform.parent = holdSpot;
                    heldObj.transform.position = holdSpot.transform.position;
                    heldObj.GetComponent<Rigidbody>().isKinematic = true;
                    heldObj.GetComponent<ItemParent>().CancelLifespan();

                    break;
                }
            }
        }
    }

    public void StartSprint(){
        sprintMultiplier = 2f;
        anim.speed = 2f;
    }

    public void EndSprint(){
        sprintMultiplier = 1f;
        anim.speed = 1f;
    }

    public void StartThrow(){
        startTime = Time.time;
    }

    public void EndThrow(){
        if(heldObj == null){
            return;
        }

        float endTime;
        float kickForce;

        endTime = Time.time - startTime;

        anim.SetTrigger("kick");

        if (endTime > maxHoldTime){
            endTime = maxHoldTime;
        }

        kickForce = Mathf.Lerp(minKickForce, maxKickForce, (endTime / maxHoldTime));

        if (heldObj){
            Physics.IgnoreCollision(CC, heldObj.GetComponent<Collider>());
            heldObj.transform.position = transform.position + Vector3.up * 2;
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.transform.parent = null;
            heldObj.GetComponent<Rigidbody>().AddForce(holdSpot.up * kickForce, ForceMode.Impulse);
            heldObj.GetComponent<ItemParent>().collisionEnabled = true;
            heldObj = null;
        }
    }

    public void InteractWithFruitBasket(){
        if (heldObj){
            bool successfullyAdded;

            successfullyAdded = fruitBasket.AddToFruitBasket(heldObj.GetComponent<ItemParent>());

            if (successfullyAdded){
                heldObj.transform.parent = null;

                heldObj.SetActive(false);

                heldObj = null;
            }
        }
        else{
            ItemParent item;

            item = fruitBasket.RemoveFromFruitBasket();

            if (item){
                item.gameObject.SetActive(true);

                heldObj = item.gameObject;
                heldObj.transform.parent = holdSpot;
                heldObj.transform.position = holdSpot.transform.position;
                heldObj.GetComponent<Rigidbody>().isKinematic = true;

                item.StartLifespan();
            }
        }
    }
}
