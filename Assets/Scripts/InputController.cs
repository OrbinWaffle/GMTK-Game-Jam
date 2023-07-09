using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour{
    PlayerController PC;
    bool canMove = true;

    // Start is called before the first frame update
    void Start(){
        PC = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update(){
        if(canMove == false)
        {
            PC.UpdateMoveVector(Vector2.zero);
            return;
        }
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        PC.UpdateMoveVector(moveVector);

        if (Input.GetButtonDown("Pickup")){
            PC.Pickup();
        }

        if (Input.GetButtonDown("Jump")){
            // PC.Jump();
        }

        if (Input.GetButtonDown("Sprint")){
            PC.StartSprint();
        }

        if (Input.GetButtonUp("Sprint")){
            PC.EndSprint();
        }

        if (Input.GetButtonDown("Throw")){
            PC.StartThrow();
        }

        if (Input.GetButtonUp("Throw")){
            PC.EndThrow();
        }

        if (Input.GetButtonDown("FruitBasket")){
            PC.InteractWithFruitBasket();
        }
    }
    public void OnGameOver()
    {
        canMove = false;
    }
}
