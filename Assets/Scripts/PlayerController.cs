using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Vector2 moveVector;
    CharacterController CC;
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
}
