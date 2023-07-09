using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasket : MonoBehaviour{
    int currentFruitCount = 0;
    Stack<ItemParent> fruitStack = new Stack<ItemParent>();
    int maxCapacity;

    // Start is called before the first frame update
    void Start(){
        maxCapacity = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddToFruitBasket(ItemParent item){
        if (currentFruitCount < maxCapacity){
            fruitStack.Push(item);

            currentFruitCount++;

            Debug.Log(fruitStack.Peek().name);

            return true;
        }
        else{
            return false;
        }
    }

    public ItemParent RemoveFromFruitBasket(){
        if (currentFruitCount > 0){
            ItemParent item;

            item = fruitStack.Pop();

            currentFruitCount--;

            return item;
        }
        else{
            return null;
        }
    }

    public void SetMaxCapacity(int newMaxCapacity){
        maxCapacity = newMaxCapacity;
    }
}
