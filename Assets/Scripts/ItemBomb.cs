using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : ItemParent{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator Lifespan(){
        yield return new WaitForSeconds(lifespan);

        scoreManager.AddPoints(-(points * 2));

        KillMe();
    }
}
