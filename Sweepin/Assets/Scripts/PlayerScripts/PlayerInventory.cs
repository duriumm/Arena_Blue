using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Since the inventory is in the GUI we just look there to get the items!
 * We can even iterate over inactive objects in the scene and get their data.
 */


public class PlayerInventory : MonoBehaviour
{
    private int amountOfInventorySlots;
    public GameObject draggableInventoryItemPrefabToAdd;
 

    // "List" is transform of backpack menu
    public Transform backPackMenu;
    void Start()
    {
        amountOfInventorySlots = GameObject.Find("BackpackMenu").transform.childCount;
        //print(amountOfInventorySlots);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get items from the scenes backpack menu.
    public void GetInventoryItemsFromInventoryMenu()
    {
        int index = 0;
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if(itemSlotTransform.childCount > 0)
            {
                print($"Item gotten from inventory slot is: {itemSlotTransform.transform.GetComponentInChildren<ItemData>().itemName}");
                print($"Index is: {index}\n");
            }
            index ++;
        }
    }

    // Add a prefab item of the "draggable item" prefab and change its data to add it
    // Adds one item to the inventory
    public void AddItemToInventory()
    {
        

        int index = 0;
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if (itemSlotTransform.childCount == 0)
            {

                GameObject instantiatedGameObject = Instantiate(draggableInventoryItemPrefabToAdd, itemSlotTransform.position, itemSlotTransform.rotation, itemSlotTransform);
                instantiatedGameObject.GetComponent<ItemData>().itemName = draggableInventoryItemPrefabToAdd.GetComponent<ItemData>().itemName;

                print($"Item gotten from inventory slot is: {itemSlotTransform.transform.GetComponentInChildren<ItemData>().itemName}");
                print($"Index is: {index}\n");
                return;
            }
            index++;
        }
    }

    public void RemoveIntemFromInventory(GameObject itemToRemove)
    {
        if(itemToRemove.tag == "DraggableItem")
        {
            Destroy(itemToRemove);
        }
    }
}
