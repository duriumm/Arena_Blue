using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{

    public Canvas mainCanvas;
    public GameObject inventoryMenuesGameObject; 
    public GameObject deathMenuGameObject;
    public GameObject allMenuesGameObject; // Holds all menues
    public List<string> menuesList = new List<string>(){};
    public PlayerInventory playerInventory;

    private void Start()
    {
        PopulateMenuList();
        playerInventory = GameObject.Find("InventoryManager").GetComponent<PlayerInventory>();

    }


    /// <summary>
    /// Enables menu of choice and disables the rest, or disables chosen menu if its active
    /// </summary>
    /// <param name="menuToEnable"></param>
    public void ToggleMenu(string menuToEnable)
    {
        Time.timeScale = 0;
        playerInventory.isInventoryOpen = true;
        GameObject menuToEnableGameObject = allMenuesGameObject.transform.Find(menuToEnable).gameObject;

        if(menuToEnableGameObject.activeSelf == true)
        {
            menuToEnableGameObject.SetActive(false);
            // Disable draggable item under canvas parent when closing inventory window
            foreach (Transform item in mainCanvas.transform)
            {
                if (item.gameObject.name == "DraggableItem")
                {
                    item.gameObject.SetActive(false);
                    item.transform.GetChild(0).gameObject.SetActive(false);

                }
            }
            Time.timeScale = 1;
            playerInventory.isInventoryOpen = false;
            return;
        }

        foreach (Transform menu in allMenuesGameObject.transform)
        {
            if (menu.name != menuToEnable) { menu.gameObject.SetActive(false); }
            else { menuToEnableGameObject = menu.gameObject; }
        }
        foreach (Transform item in mainCanvas.transform)
        {
            if (item.gameObject.name == "DraggableItem")
            {
                item.gameObject.SetActive(true);
                item.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        menuToEnableGameObject.SetActive(true);
    }

    public void PopulateMenuList()
    {
        foreach (Transform child in allMenuesGameObject.transform)
        {
            menuesList.Add(child.name);
        }
    }
}
