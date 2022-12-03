using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * This script handles all dragging, dropping and clicking of draggable inventory items
 * aswell as items in the hotbar. This script will be attached to all usable items
 */
public class DragDrop : MonoBehaviour, IPointerDownHandler, 
    IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler, IDropHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector2 itemsOriginalVector2;
    public Transform itemsOriginalParent;
    private MouseHovering mouseHovering;
    public GameObject equipmentMenuGameObject;
    public PlayerInventory playerInventory;
    private PlayerHealth playerHealth;
    private AudioManager audioManager;




    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        mouseHovering = gameObject.GetComponent<MouseHovering>();
        equipmentMenuGameObject = GameObject.Find("EquipmentMenu").gameObject;
        playerInventory = GameObject.Find("InventoryManager").GetComponent<PlayerInventory>();
        playerHealth = GameObject.Find("MyCharacter").GetComponent<PlayerHealth>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();




    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItemGameObject = eventData.pointerClick;
        ItemData itemData = droppedItemGameObject.GetComponent<ItemData>();

        print($"dropping {droppedItemGameObject}  on {gameObject} ");
        // Using enum index here since equipmentSlotItemType and} itemType is the same
        print($"Equipmentslot type is: {gameObject.transform.parent.GetComponent<ItemSlot>()}");




        //if ((int)itemData.itemType == (int)gameObject.transform.GetComponent<ItemSlot>().equipmentSlotItemType && gameObject.GetComponent<ItemData>().isEquipped)
        //{
        //    print($"Item we dropped on: {gameObject} is currently equipped");
            
        //}
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        // Raycast target till OFF för ALLT i inventory och equipment menu
        playerInventory.DisableAllInventoryItemsRaycastWhenDraggingItem();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHovering.hoverItemCanvasGameObject.SetActive(false);
        mouseHovering.enabled = false;
        itemsOriginalParent = gameObject.transform.parent;
        itemsOriginalVector2 = rectTransform.anchoredPosition;
        gameObject.transform.parent = canvas.transform;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = itemsOriginalVector2;
        mouseHovering.enabled = true;
        playerInventory.EnableAllInventoryItemsRaycastWhenDraggingItem();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    // Right clicking items in the inventory should use them
    // Right clicking one of our equipped items should add them to inventory
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            var clickedItemGameObject = eventData.pointerClick;
            ItemData itemData = clickedItemGameObject.GetComponent<ItemData>();
            // Usable items cant be equipped so we return 
            if (itemData.itemType == ItemData.ItemType.Usable)
            {
                return;
            }
            // Usable items cant be equipped so we return 
            else if (itemData.itemType == ItemData.ItemType.Healing)
            {
                playerHealth.GainHealth(itemData.healingAmount);
                audioManager.PlayGUISoundEffect(itemData.audioClip, audioManager.soundEffectsSource);
                Destroy(clickedItemGameObject);
                return;
            }


            // Everything below here is for EQUIPPING an inventory item

            // If item is currently equipped, add itt to inventory
            else if (itemData.isEquipped == true)
            {
                playerInventory.AddItemToInventory(clickedItemGameObject, 100, true); // 100 as itemslot since we want to add to the first available slot
            }

            // If item is NOT equipped, add it to equipped items
            else if (itemData.isEquipped == false)
            {
                playerInventory.EquipItemFromInventory(clickedItemGameObject);
            }
        }
    }
}
