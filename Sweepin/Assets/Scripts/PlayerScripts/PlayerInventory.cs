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
    //public GameObject draggableInventoryItemPrefabToAdd;

    public bool isInventoryOpen = false;
    // "List" is transform of backpack menu
    public Transform backPackMenu;
    public ItemSlot itemSlot;
    private PlayerStats playerStats;
    private GameObject equipmentMenuGameObject;
    private AudioManager audioManager;
    public AudioClip equipItemSound;
    public AudioClip deEquipItemSound;



    void Start()
    {
        amountOfInventorySlots = GameObject.Find("BackpackMenu").transform.childCount;
        playerStats = GameObject.Find("MyCharacter").GetComponent<PlayerStats>();
        equipmentMenuGameObject = GameObject.Find("EquipmentMenu").gameObject;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();



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
    public void AddItemToInventory(GameObject itemToAdd)
    {

        print("addding to inventory");
        int index = 0;
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if (itemSlotTransform.childCount == 0)
            {
                audioManager.PlayGUISoundEffect(deEquipItemSound, audioManager.soundEffectsSource);
                itemToAdd.GetComponent<ItemData>().isEquipped = false;
                itemToAdd.GetComponent<RectTransform>().position = itemSlotTransform.GetComponent<RectTransform>().position;
                itemToAdd.transform.parent = itemSlotTransform;
                itemToAdd.GetComponent<MouseHovering>().originalParent = itemSlotTransform;
                itemToAdd.transform.localScale = new Vector2(1, 1);
                playerStats.CalculateAllEquipmentSlotsAndAddData();
                return;
            }
            index++;
        }
    }


    // TODO: Refactor equip and deequip into one function since they work similarly
    public void EquipItemFromInventory(GameObject itemGameObjectToEquip)
    {
        // Equip item at correct equipmentslot based on itemtype
        foreach (Transform equipmentSlot in equipmentMenuGameObject.transform)
        {
            // Using enum index here since equipmentSlotItemType and itemType is the same
            if ((int)itemGameObjectToEquip.GetComponent<ItemData>().itemType == (int)equipmentSlot.GetComponent<ItemSlot>().equipmentSlotItemType)
            {
                audioManager.PlayGUISoundEffect(equipItemSound, audioManager.soundEffectsSource);
                itemGameObjectToEquip.GetComponent<ItemData>().isEquipped = true;
                itemGameObjectToEquip.GetComponent<RectTransform>().position = equipmentSlot.GetComponent<RectTransform>().position;
                itemGameObjectToEquip.transform.parent = equipmentSlot;
                itemGameObjectToEquip.GetComponent<MouseHovering>().originalParent = equipmentSlot;
                itemGameObjectToEquip.transform.localScale = new Vector2(1, 1);
                playerStats.CalculateAllEquipmentSlotsAndAddData();
            }
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
