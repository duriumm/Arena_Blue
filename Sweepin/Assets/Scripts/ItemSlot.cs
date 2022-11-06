using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private AudioManager audioManager;
    public AudioClip audioClipToPlayOnDrop;
    private PlayerStats playerStats;
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
    }

    // What happens when we drop something in this Item slot
    public void OnDrop(PointerEventData eventData)
    {
        // Delete item when dropped in garbage slot
        if(itemSlotType == ItemSlotType.GarbageSlot)
        {
            audioManager.PlayGUISoundEffect(audioClipToPlayOnDrop, audioManager.soundEffectsSource);
            Destroy(eventData.pointerDrag);
        }
        // Return item to previous slot if equipmentslot type does not match items type
        else if (itemSlotType == ItemSlotType.EquipmentSlot)
        {
            var draggableItemsData = eventData.pointerDrag.GetComponent<ItemData>();
            if((int)equipmentSlotItemType != (int)draggableItemsData.itemType)
            {
                return;
            }
        }
        // Equip item with drag n drop
        if (eventData.pointerDrag != null && eventData.pointerDrag.tag == "DraggableItem")
        {
            if (itemSlotType == ItemSlotType.EquipmentSlot)
            {
                eventData.pointerDrag.GetComponent<ItemData>().isEquipped = true;
            }
            else
            {
                eventData.pointerDrag.GetComponent<ItemData>().isEquipped = false;

            }
            audioManager.PlayGUISoundEffect(audioClipToPlayOnDrop, audioManager.soundEffectsSource);
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.transform.parent = gameObject.transform;
            eventData.pointerDrag.GetComponent<MouseHovering>().originalParent = eventData.pointerDrag.transform.parent;
            eventData.pointerDrag.transform.localScale = new Vector2(1, 1);

            // Whenever we drop a item in a slot we want to calculate the equipment data 
            // since we can drop equipment in our backpack slot
            playerStats.CalculateAllEquipmentSlotsAndAddData();

            

        }
    }
    //public void OnBeginDrag(PointerEventData eventData)
    //{
        
    //    var draggableItemsData = eventData.pointerDrag.GetComponent<ItemData>();
    //    print(eventData.pointerDrag);
    //    if (itemSlotType == ItemSlotType.EquipmentSlot)
    //    {
    //        playerStats.DecreaseStats("damage", draggableItemsData.damage);
    //        playerStats.DecreaseStats("armor", draggableItemsData.armor);
    //        playerStats.DecreaseStats("speed", draggableItemsData.speed);
    //    }
    //}

}
