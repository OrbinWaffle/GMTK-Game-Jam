using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform holdSpot;
    Vector2 moveVector;
    CharacterController CC;
    GameObject heldObj;
    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CC.Move(new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed * Time.deltaTime);
    }
    public void UpdateMoveVector(Vector2 newVec)
    {
        moveVector = newVec;
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
