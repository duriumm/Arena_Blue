using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private AudioManager audioManager;
    private PauseMenu pauseMenu;
    public PlayerBowAttack playerBowAttack;
    public PlayerMeleeAttack playerMeleeAttack;
    public EnemySpawner enemySpawner;
    private PlayerDash playerDash;
    private GuiManager guiManager;
    public PlayerInventory playerInventory;
    public ItemData itemDataTest;
    private Hotbar hotbar;





    public void Start()
    {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        guiManager = GameObject.Find("GuiManager").GetComponent<GuiManager>();
        playerDash = gameObject.GetComponent<PlayerDash>();
        hotbar = GameObject.Find("Hotbar").GetComponent<Hotbar>();

        //pauseMenu = GameObject.Find("GameManager").gameObject.GetComponent<PauseMenu>();

    }


    void Update()
    {
        // We still want to switch the pause screen on/off hence why this is here
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    pauseMenu.SwitchPauseScreen();
        //}
        //// If the screen is paused, we dont want to act on ANY inputs
        //if (pauseMenu.isScreenPaused == true)
        //{
        //    return;
        //}
        // -------------- BELOW LINES ONLY ACT WHEN SCREEN IS NOT IN PAUSED STATE  -------------- //

        // checks if mousbuttonLEFT is pressed
        if (Input.GetMouseButtonDown(1))
        {
            playerBowAttack.ChargeBow();
        }
        // checks if mousbuttonRIGHT is pressed
        if (Input.GetMouseButtonDown(0))
        {
            playerMeleeAttack.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playerDash.canPlayerDash == true)
            {
                StartCoroutine(playerDash.DashTowardsCursor());
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerInventory.GetInventoryItemsFromInventoryMenu();
            guiManager.ToggleMenu("InventoryMenues");
        }



        //// Gets called ONCE when we release mousebutton 0
        if (Input.GetMouseButtonUp(1))
        {
            playerBowAttack.InstantiateProjectileAndShoot();

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            print("STARTED enemy spawning");
            enemySpawner.StartEnemySpawning(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Pressed nr 1");
            hotbar.SetHotbarActive(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("Pressed nr 2");
            hotbar.SetHotbarActive(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("Pressed nr 3");
            hotbar.SetHotbarActive(3);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            playerInventory.AddItemToInventory();
            //enemySpawner.StopEnemySpawning();
            //print("STOPPED enemy spawning");
        }


    }
}
