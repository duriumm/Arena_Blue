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
    public void AddItemToInventory(GameObject itemToAdd, int itemSlotNumber = 100, bool isItemFromEquippedItems = false)
    {

        //print("addding to inventory");
        int index = 0;
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if (index == itemSlotNumber)
            {
                print($"Adding item to invetory on itemslot number: {itemSlotNumber}");
                AddItem(itemToAdd, itemSlotTransform, false, deEquipItemSound);
                return;
            }
            else if(isItemFromEquippedItems && itemSlotTransform.childCount == 0)
            {
                print($"FROM EQUIPPED !! Adding item to invetory on itemslot number: {itemSlotNumber}");
                AddItem(itemToAdd, itemSlotTransform, false, deEquipItemSound);
                return;
            }
            index++;
        }
    }

    // Function which is reused both for equipping items FROM inventory and adding equipped items TO inventory
    // Therefor we reuse it here
    private void AddItem(GameObject itemToAdd, Transform itemSlotTransformForItemToAdd, bool shouldItemBeEquipped, AudioClip inventoryUseItemSound)
    {
        audioManager.PlayGUISoundEffect(inventoryUseItemSound, audioManager.soundEffectsSource);
        itemToAdd.GetComponent<ItemData>().isEquipped = shouldItemBeEquipped;
        itemToAdd.GetComponent<RectTransform>().position = itemSlotTransformForItemToAdd.GetComponent<RectTransform>().position;
        itemToAdd.transform.parent = itemSlotTransformForItemToAdd;
        itemToAdd.GetComponent<MouseHovering>().originalParent = itemSlotTransformForItemToAdd;
        itemToAdd.transform.localScale = new Vector2(1, 1);
        playerStats.CalculateAllEquipmentSlotsAndAddData();
    }


    public void EquipItemFromInventory(GameObject itemGameObjectToEquip)
    {
        // Equip item at correct equipmentslot based on itemtype
        foreach (Transform equipmentSlotTransform in equipmentMenuGameObject.transform)
        {
            // Using enum index here since equipmentSlotItemType and itemType is the same (Check if item type is same)
            if ((int)itemGameObjectToEquip.GetComponent<ItemData>().itemType == (int)equipmentSlotTransform.GetComponent<ItemSlot>().equipmentSlotItemType)
            {
                // If we already have an item equipped we switch between them
                if (equipmentSlotTransform.childCount > 0)
                {
                    AddItemToInventory(equipmentSlotTransform.GetChild(0).gameObject, isItemFromEquippedItems: true); 
                }
                AddItem(itemGameObjectToEquip, equipmentSlotTransform, true, equipItemSound);
                return;
            }
        }
    }

    public void DestroyItemFromInventory(GameObject itemToRemove)
    {
        if(itemToRemove.tag == "DraggableItem")
        {
            Destroy(itemToRemove);
        }
    }

    public void DisableAllInventoryItemsRaycastWhenDraggingItem()
    {
        // Raycast target till OFF för ALLT i inventory och equipment menu

        foreach (Transform equipmentItemSlot in equipmentMenuGameObject.transform)
        {
            if (equipmentItemSlot.childCount > 0)
            {
                //print($"PRÄÄÄÄÄÄÄNT equipmentItemSlot child in equipment: {equipmentItemSlot.GetChild(0)}");
                var equipmentSlotCanvasGroup = equipmentItemSlot.GetChild(0).GetComponent<CanvasGroup>();
                equipmentSlotCanvasGroup.blocksRaycasts = false;

            }
        }
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if (itemSlotTransform.childCount > 0)
            {
                var inventorySlotCanvasGroup = itemSlotTransform.GetChild(0).GetComponent<CanvasGroup>();
                inventorySlotCanvasGroup.blocksRaycasts = false;
            }
        }
    }

    public void EnableAllInventoryItemsRaycastWhenDraggingItem()
    {
        // Raycast target till OFF för ALLT i inventory och equipment menu

        foreach (Transform equipmentItemSlot in equipmentMenuGameObject.transform)
        {
            if (equipmentItemSlot.childCount > 0)
            {
                //print($"PRÄÄÄÄÄÄÄNT equipmentItemSlot child in equipment: {equipmentItemSlot.GetChild(0)}");
                var equipmentSlotCanvasGroup = equipmentItemSlot.GetChild(0).GetComponent<CanvasGroup>();
                equipmentSlotCanvasGroup.blocksRaycasts = true;

            }
        }
        foreach (Transform itemSlotTransform in backPackMenu)
        {
            if (itemSlotTransform.childCount > 0)
            {
                var inventorySlotCanvasGroup = itemSlotTransform.GetChild(0).GetComponent<CanvasGroup>();
                inventorySlotCanvasGroup.blocksRaycasts = true;
            }
        }
    }
}
