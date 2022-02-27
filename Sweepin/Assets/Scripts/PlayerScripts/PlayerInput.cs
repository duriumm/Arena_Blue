using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private PlayerSweeping playerSweeping;
    private Tilemap dirtTilemap;
    private Grid dirtGrid;
    public GameObject dirtTilemapGameobject;
    


    public void Start()
    {
        playerSweeping = GetComponent<PlayerSweeping>();
        dirtTilemap = dirtTilemapGameobject.GetComponent<Tilemap>();
        dirtGrid = GameObject.Find("Dirty Tiles").GetComponent<Grid>();

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
        print("player pos is: " + this.gameObject.transform.position);
        if (distance > 0.7f)
        {
            return;
        }
        else
        {
            dirtTilemap.SetTile(position, null);
        }
        
    }
}
