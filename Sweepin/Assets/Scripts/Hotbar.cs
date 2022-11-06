using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{ 
    public List<object> hotbarSlots = new List<object>() { };
    private string currentActiveHotbarSlotString;
    private GameObject chosenHotbarSlotArrow;
    private GameObject currentActiveHotbarSlotGameObject;
    void Start()
    {
        //var childHotbarItemSlotGameObjects = GetComponentsInChildren<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            print(child);
            if(child.name == "ChosenHotbarSlotArrow")
            {
                chosenHotbarSlotArrow = child.gameObject;
                print("ChosenHotbarSlotArrow");
            }
        }



        

    }

    public void SetHotbarActive(int hotbarToActivate)
    {
        print("hotbar to set active is: " + hotbarToActivate);
        currentActiveHotbarSlotString = $"HotbarItemSlot {hotbarToActivate.ToString()}";
        chosenHotbarSlotArrow.transform.parent = gameObject.transform.Find(currentActiveHotbarSlotString);
        var rectTransform = chosenHotbarSlotArrow.GetComponent<RectTransform>();
        // Set anchors to center the arrow on hotbar slot
        chosenHotbarSlotArrow.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        chosenHotbarSlotArrow.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        chosenHotbarSlotArrow.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

        // ABABAB
        currentActiveHotbarSlotGameObject = chosenHotbarSlotArrow.transform.parent.gameObject;
        Image image;
        image = currentActiveHotbarSlotGameObject.GetComponent<Image>();
        image.color = new Color(255, 255, 255);

        foreach (Transform child in gameObject.transform)
        {

            if (child.name != currentActiveHotbarSlotString)
            {
                print("gej");
                Image image2;
                image2 = child.gameObject.GetComponent<Image>();
                image2.color = new Color(0.7f, 0.7f, 0.7f);
            }
        }



        chosenHotbarSlotArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 95);
        print(currentActiveHotbarSlotString);

    }

}
