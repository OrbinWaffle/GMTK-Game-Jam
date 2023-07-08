using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] Transform holdSpot;
    float groundCheckDelay = 0.25f;
    Vector2 moveVector;
    CharacterController CC;
    GameObject heldObj;
    float verticalVelocity = 0f;
    float nextGroundCheckTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
            new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed * Time.fixedDeltaTime
            + Vector3.up * verticalVelocity * Time.fixedDeltaTime
        );
    }
    public void UpdateMoveVector(Vector2 newVec)
    {
        moveVector = newVec;
        RotatePlayer(moveVector);
    }
    public void RotatePlayer(Vector2 vectorToRotateTowards)
    {
        
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
            Debug.Log("hi");
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.transform.parent = null;
            heldObj = null;
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(holdSpot.position, 1);
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
}
