using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.25f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float sprintMultiplier = 1f;
    [SerializeField] Transform holdSpot;
    float groundCheckDelay = 0.25f;
    Vector2 moveVector;
    CharacterController CC;
    Animator anim;
    GameObject heldObj;
    float verticalVelocity = 0f;
    float nextGroundCheckTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
    }
    void UpdateAnimations()
    {
        anim.SetFloat("moveSpeed", moveVector.magnitude);
        anim.SetBool("isHolding", heldObj != null);
    }
    void FixedUpdate()
    {
        if(CC.isGrounded && Time.time > nextGroundCheckTime)
        {
            verticalVelocity = 0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        }
        CC.Move
        (
            new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed * sprintMultiplier * Time.fixedDeltaTime
            + Vector3.up * verticalVelocity * Time.fixedDeltaTime
        );
    }
    public void UpdateMoveVector(Vector2 newVec)
    {
        moveVector = newVec;
        if(newVec != Vector2.zero)
        {
            RotatePlayer(new Vector3(moveVector.x, 0, moveVector.y));
        }
    }
    public void RotatePlayer(Vector3 vectorToRotateTowards)
    {
        Quaternion targetRotation = Quaternion.LookRotation(vectorToRotateTowards, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    public void Jump()
    {
        nextGroundCheckTime = Time.time + groundCheckDelay;
        verticalVelocity = jumpSpeed;
    }
    public void Pickup()
    {
        if(heldObj != null)
        {
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.transform.parent = null;
            heldObj = null;
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(holdSpot.position, 1.5f);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Fruit"))
            {
                heldObj = collider.gameObject;
                heldObj.transform.parent = holdSpot;
                heldObj.transform.position = holdSpot.transform.position;
                heldObj.GetComponent<Rigidbody>().isKinematic = true;
                break;
            }
        }
    }

    public void StartSprint(){
        sprintMultiplier = 4f;
    }

    public void EndSprint(){
        sprintMultiplier = 1f;
    }

    public void StartThrow(){
        anim.SetTrigger("kick");
    }
}
