using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    //creating an array to hold items - using Unitys built in array system
    public GameObject[] inventory = new GameObject[8];
    public Button[] InventoryButtons = new Button[8];

    //creating a function we can call to add an item to our inventory
    public void AddItem(GameObject item)
    {
        bool itemAdded = false;

        //find the first open slot in the inventory
        //loops through array looking for empty slot
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory [i] == null)
            {
                inventory [i] = item;
                Sprite sprite1 = item.GetComponent<SpriteRenderer>().sprite;

                //Adds Item GameObject to DDOL "Inventory Items"
                item.transform.SetParent(transform.parent);


                //goes to the button - goes to its image component and over rides it with the sprite image for the item picked up
                InventoryButtons[i].GetComponent<Image>().sprite = sprite1;
                Debug.Log (item.name + " was added");
                //stop running the loop otherwise we add item picked up to every slot
                itemAdded = true;
                //Do something with object
                item.SendMessage ("DoInteraction");
                break;
            }
        }

        //inventory full
        if (!itemAdded)
        {
            Debug.Log ("Inventory Full - item not added");
        }
    }

    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                //we found required item
                return true;
            }
        }
        //item not found
        return false;
    }

    public GameObject FindItemByType(string itemType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                //checks if the inventories item "itemType" is equal to the items "itemType"
                if (inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    //found the item of type we needed - return item to what called function
                    return inventory[i];
                }
            }
        }
        //item of type not found - return nothing
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                //Remove item when found
                inventory[i] = null;
                Debug.Log(item.name + " was removed from inventory");
                //update the UI to reflect item being removed
                InventoryButtons[i].image.overrideSprite = null;
                break;
            }
        }
    }

}
