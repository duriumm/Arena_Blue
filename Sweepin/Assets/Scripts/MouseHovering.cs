using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverItemCanvasGameObject;
    private Canvas canvas;

    private ItemData itemData;
    private TextMeshProUGUI itemText;
    public Transform originalParent;
    public bool hasItemStartedDragging = false;

    private void Start()
    {
        originalParent = gameObject.transform.parent;
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        hoverItemCanvasGameObject = gameObject.transform.Find("HoverItemCanvas").gameObject;
        hoverItemCanvasGameObject.SetActive(true);
        var childGameObjects = GetComponentsInChildren<Transform>();
        foreach (var child in childGameObjects)
        {
            if (child.name == "HoverItemText") { itemText = child.gameObject.GetComponent<TextMeshProUGUI>(); }
        }

        itemData = gameObject.GetComponent<ItemData>();
        hoverItemCanvasGameObject.SetActive(false);

        //print(itemText);
        

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverItemCanvasGameObject.SetActive(true);
        gameObject.transform.SetParent(canvas.transform, true);
        print($"This items original parent is: {originalParent}!");

        if (itemData.itemType == ItemData.ItemType.Weapon)
        {
            itemText.text = $"{itemData.itemName}\n\nDamage: {itemData.damage}\nValue: {itemData.value}\n\n{itemData.description}";

        }
        else if(itemData.itemType == ItemData.ItemType.Armor)
        {
            itemText.text = $"{itemData.itemName}\nArmor: {itemData.armor}\nValue: {itemData.value}\n\n{itemData.description}";

        }
        //print(itemData);


    }
    public void OnPointerExit(PointerEventData eventData)
    {

       // print("we are exiting item HOVER");
        hoverItemCanvasGameObject.SetActive(false);
        gameObject.transform.SetParent(originalParent, true);
 

    }
}









