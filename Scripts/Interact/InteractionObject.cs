using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    
    public bool inventory;          //if true this object can be stored in inventory
    public bool openable;           //if true this object can open
    public bool locked;             //if this is true object is locked
    public GameObject itemNeeded;   //logic so object knows what is needed to open i.e. door needs key
    public string itemType;         //defines the item type and helps identify unique items from one another

    public Collider2D LockedDoor;
    public Animator anim;

    public void DoInteraction()
    {
        //Picked up and put in inventory
        gameObject.SetActive (false);
    }

    public void Open()
    {
        anim.SetBool("open", true);
    }

}
