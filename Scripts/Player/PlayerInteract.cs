using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //TO DO: Create open door animation
    //TO DO: set heal for using potion

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractableObject = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    GameObject DDOL;

    //creating the input key for interaction with objects

    private void Start()
    {
        DDOL = GameObject.FindGameObjectWithTag("DDOL");
        inventory = DDOL.GetComponentInChildren<Inventory>();
    }

    void Update()
    {   
        //performing check to see if there is a current interactable object nearby
        if (Input.GetButtonDown ("Interact") && currentInteractableObject)
        {
            //check if object can be stored in inventory
            //checks the objects script boolean variable
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem (currentInteractableObject);
                
            }

            //check if object can be opened
            if (currentInterObjScript.openable)
            {
                //check if openable object is locked
                if (currentInterObjScript.locked)
                {
                    //check if we have the object needed to unlock by checking our inventory
                    //Search our inventory for item - if found unlock object
                    if (inventory.FindItem (currentInterObjScript.itemNeeded))
                    {
                        //if we've found item in inventory
                        currentInterObjScript.locked = false;
                        currentInterObjScript.LockedDoor.enabled = false;
                        Debug.Log (currentInteractableObject.name + " was unlocked");
                    }
                    else
                    {
                        Debug.Log(currentInteractableObject.name + " was not unlocked");
                    }
                }
                else
                {
                    //object is not locked and we want to open the object
                    Debug.Log (currentInteractableObject.name + " is unlocked");
                    currentInterObjScript.Open();
                }
            }

        }

        //use a potion
        if(Input.GetButtonDown("Use Potion"))
        {
            //check the inventory for a potion
            GameObject potion = inventory.FindItemByType ("Health Potion");
            //logic for finding potion
            if (potion != null)
            {
                //if we have potion use it - apply effect
                inventory.RemoveItem(potion);

            }

            //remove the potion from inventory
        }

    }

    //stores information of the object that the player has walked into
    void OnTriggerEnter2D(Collider2D other) 
    {
        //if the object has the tag "InteractableObject" store it on the player when triggered
        if (other.CompareTag ("InteractableObject"))
        {
            Debug.Log (other.name);
            //storing the game object
            currentInteractableObject = other.gameObject;
            //go into the objects script and store it in our variable "currentInterObjScript"
            currentInterObjScript = currentInteractableObject.GetComponent <InteractionObject> ();
        }      

    }

    //removes the object when the player is no longer in range
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag ("InteractableObject"))
        {
            if (other.gameObject == currentInteractableObject)
            {
                currentInteractableObject = null;
            }
            
        }
    }

}
