using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private AudioManager audioManager;
    public AudioClip audioClipToPlayOnDrop;
    private PlayerStats playerStats;
    public PlayerInventory playerInventory;
    private int itemSlotNumber;
    public enum ItemSlotType
    {
        BackpackSlot,
        GarbageSlot,
        EquipmentSlot,
        HotbarSlot
    };
    public ItemSlotType itemSlotType;

    // This is for equipment slots. They need to have itemType for specific items to
    // be placed here. Sword in swordslot, shield in shieldslot etc. 
    public enum ItemType
    {
        Weapon,
        Armor,
        Headgear,
        Offhand,
        Ring,
        Amulet,
        Ranged,
        Boots
    }
    public ItemType equipmentSlotItemType;



    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        playerStats = GameObject.Find("MyCharacter").GetComponent<PlayerStats>();
        playerInventory = GameObject.Find("InventoryManager").GetComponent<PlayerInventory>();
        // Assign and print the itemslot number for every itemslot for backpack :) 
        itemSlotNumber = int.Parse(gameObject.name.Split(' ')[1]);
        if(itemSlotType == ItemSlotType.BackpackSlot)
        {
            print($"Gameobject: {gameObject} -- item slot number: {itemSlotNumber}");
        }

    }

    // What happens when we drop something in this Item slot
    public void OnDrop(PointerEventData eventData)
    {

        var droppedGameObject = eventData.pointerDrag;
        var draggableItemsData = droppedGameObject.GetComponent<ItemData>();

        // Equip item with drag n drop
        if (droppedGameObject != null && droppedGameObject.tag == "DraggableItem")
        {
            // Our item slot type is Garbage 
            if (itemSlotType == ItemSlotType.GarbageSlot)
            {
                print("Dropping item in garbage");
                audioManager.PlayGUISoundEffect(audioClipToPlayOnDrop, audioManager.soundEffectsSource);
                playerInventory.DestroyItemFromInventory(droppedGameObject);
            }
            // Our itemslot type is Equipment 
            else if (itemSlotType == ItemSlotType.EquipmentSlot)
            {
                print("Dropping item in equipment slot");
                // Trying to drop equipment in slot which does not match item type will exit the code
                if ((int)equipmentSlotItemType != (int)draggableItemsData.itemType)
                {
                    return;
                }

                droppedGameObject.GetComponent<ItemData>().isEquipped = true;

                // If this itemslot already has an item equipped we want to move the equipped item to the inventory
                if (gameObject.transform.childCount > 0)
                {
                    var originalParentsItemSlotNumber = droppedGameObject.GetComponent<MouseHovering>().originalParent.GetComponent<ItemSlot>().itemSlotNumber;
                    GameObject equippedGameObject = gameObject.transform.GetChild(0).gameObject;
                    playerInventory.AddItemToInventory(equippedGameObject, originalParentsItemSlotNumber);
                }
                playerInventory.EquipItemFromInventory(droppedGameObject);
                playerStats.CalculateAllEquipmentSlotsAndAddData();

            }
            else if(itemSlotType == ItemSlotType.BackpackSlot)
            {
                print("Dropping item in inventory");
                droppedGameObject.GetComponent<ItemData>().isEquipped = false;

                // If this itemslot already has an item equipped we want to move the equipped item to the inventory
                if (gameObject.transform.childCount > 0)
                {
                    var originalParentsItemSlotNumber = droppedGameObject.GetComponent<MouseHovering>().originalParent.GetComponent<ItemSlot>().itemSlotNumber;
                    GameObject invSlotGameObject = gameObject.transform.GetChild(0).gameObject;
                    playerInventory.AddItemToInventory(invSlotGameObject, originalParentsItemSlotNumber);
                }

                playerInventory.AddItemToInventory(droppedGameObject, itemSlotNumber);
            }
        }
    }
}
