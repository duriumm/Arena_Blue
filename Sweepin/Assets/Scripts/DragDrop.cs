using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * This script handles all dragging, dropping and clicking of draggable inventory items
 */
public class DragDrop : MonoBehaviour, IPointerDownHandler, 
    IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 itemsOriginalVector2;
    private MouseHovering mouseHovering;
    public GameObject equipmentMenuGameObject;
    public PlayerInventory playerInventory;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        mouseHovering = gameObject.GetComponent<MouseHovering>();
        equipmentMenuGameObject = GameObject.Find("EquipmentMenu").gameObject;
        playerInventory = GameObject.Find("InventoryManager").GetComponent<PlayerInventory>();



    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHovering.hoverItemCanvasGameObject.SetActive(false);
        mouseHovering.enabled = false;
        itemsOriginalVector2 = rectTransform.anchoredPosition;
        gameObject.transform.parent = canvas.transform;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = itemsOriginalVector2;
        mouseHovering.enabled = true;
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
            var itemGameObjectToEquip = eventData.pointerClick;

            // Usable items cant be equipped so we return 
            if (itemGameObjectToEquip.GetComponent<ItemData>().itemType == ItemData.ItemType.Usable)
            {
                return;
            }
            // If item is currently equipped, add itt to inventory
            else if (itemGameObjectToEquip.GetComponent<ItemData>().isEquipped == true)
            {
                playerInventory.AddItemToInventory(itemGameObjectToEquip);
            }

            // If item is NOT equipped, add it to equipped items
            else if (itemGameObjectToEquip.GetComponent<ItemData>().isEquipped == false)
            {
                playerInventory.EquipItemFromInventory(itemGameObjectToEquip);
            }
        }
    }
}
