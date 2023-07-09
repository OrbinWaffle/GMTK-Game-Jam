using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    PlayerController PC;
    void Start()
    {
        PC = transform.parent.GetComponent<PlayerController>();
    }
    public void OnStep()
    {
        PC.OnStep();
    }
}
