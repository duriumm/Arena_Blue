﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private PlayerSweeping playerSweeping;
    private Tilemap dirtTilemap;
    private Grid dirtGrid;
    private PlayerMovement playerMovement;
    public GameObject dirtTilemapGameobject;
    


    public void Start()
    {
        playerSweeping = GetComponent<PlayerSweeping>();
        dirtTilemap = dirtTilemapGameobject.GetComponent<Tilemap>();
        dirtGrid = GameObject.Find("Dirty Tiles").GetComponent<Grid>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

    }


    void Update()
    {

        // checks if mousbuttonLEFT is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // TODO - Rename this function maybe..??
            



        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Held");
            //playerSweeping.Sweep();
            TestHoldMouseButtonSweep();
        }
    }

    void TestHoldMouseButtonSweep()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int position = dirtGrid.WorldToCell(mousePos);
        var distance = Vector2.Distance(this.gameObject.transform.position, mousePos);
        // TODO : Check the way player is facing, and depending on that view only remove dirt. 
        // So if the player is facing right, only remove dirt from the right side.
        // TODO:
        // 1. Get players last facing direction
        // 2. Decide if we are looking left, right, up or down (make a enum or some easy readable list)
        // 3. If the player is facing right -> Return from function sweeping if mousePosition is not to the right of player
        // if(playerpos == right && mousePos == right){ sweep() }
        //print("Distance between player and mouse is: " + distance);
        print("Mousepos is: " + mousePos);
        Vector3 playerPreferedPosition = this.gameObject.transform.position;
        playerPreferedPosition.y = playerPreferedPosition.y - 0.4f;
        print("player actual pos is: " + this.gameObject.transform.position);
        print("playerPrefered pos a little bit lower (at the feet) is: " + playerPreferedPosition);
 
        string playersCurrentFacingDir = "";
        if (playerMovement.GetLastMoveXdirection() == 1f)
        {   // == Player is facing right
            print("Player is facing right");
            playersCurrentFacingDir = "Right";
        }
        else if(playerMovement.GetLastMoveXdirection() == -1f)
        {   // == Player is facing left
            print("Player is facing left");
            playersCurrentFacingDir = "Left";
        }
        else if (playerMovement.GetLastMoveYdirection() == 1f)
        {   // == Player is facing up
            print("Player is facing up");
            playersCurrentFacingDir = "Up";
        }
        else if (playerMovement.GetLastMoveYdirection() == -1f)
        {   // == Player is facing down
            print("Player is facing down");
            playersCurrentFacingDir = "Down";
        }

        float allowedYaxisSweep = 0.4f;
        float allowedXaxisSweep = 0.4f;

        // TODO : CREATE SO IF PLAYER IS LOOKING DOWN WE CAN ONLY SWEEP LOWER THAN HIS CURRENT Y POSITION && DISTANCE OF 0.7F
        if (distance > 0.7f) //&&  MousePos is in AllowedDirectionToSweep -> MousePos must be PlayerPos and towards allowedDir )
        {
            return;
        }
        else if (playersCurrentFacingDir == "Right" && mousePos.x > playerPreferedPosition.x + 0.1f)
        {
            dirtTilemap.SetTile(position, null);
        }
        else if (playersCurrentFacingDir == "Left" && mousePos.x < playerPreferedPosition.x - 0.1f)
        {
            dirtTilemap.SetTile(position, null);
        }
        else if (playersCurrentFacingDir == "Up" && mousePos.y > playerPreferedPosition.y + 0.1f)
        {
            dirtTilemap.SetTile(position, null);
        }
        else if (playersCurrentFacingDir == "Down" && mousePos.y < playerPreferedPosition.y - 0.1f)
        {
            dirtTilemap.SetTile(position, null);
        }

    }
}
