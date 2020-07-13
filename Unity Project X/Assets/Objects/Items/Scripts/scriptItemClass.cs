using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptItemClass : MonoBehaviour
{
    public GameObject self;
    public string itemName = "Generic Item";
    public string description = "Generic Description";
    public Sprite iconOnInventory;
    public Sprite spriteOnGround;
    public bool canWear = false;
    public bool canUse = false;

    public virtual void useItem(){
        Debug.Log("Trying to use item "+name+", no useItem function programmed for this item! Fix this!");
    }

    public virtual void wearItem(){
        Debug.Log("Trying to wear item "+name+", no wearItem function programmed for this item! Fix this!");
    }

    public virtual void onInteraction(){
        Debug.Log("Trying to interact with "+this.name+", it has no OnInteraction override programmed!");
    }
}
