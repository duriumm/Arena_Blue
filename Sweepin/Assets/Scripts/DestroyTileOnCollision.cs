using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTileOnCollision : MonoBehaviour
{
    
    private Vector3 tilesPosition;
    private Vector3Int tileGridCoordinate;


    public GameObject dirtTilemapGameobject;

    private Grid dirtGrid;
    private Tilemap dirtTilemap;


    void Start()
    {
        dirtGrid = GameObject.Find("Dirty Tiles").GetComponent<Grid>();
        dirtTilemap = dirtTilemapGameobject.GetComponent<Tilemap>();
        Debug.Log("dirtmap is " + dirtTilemap);
        Debug.Log("dirtGRID is " + dirtGrid);


        //tilesPosition = gameObject.transform.position;
        //tileGridCoordinate = tilemap.WorldToCell(tilesPosition);
        //print($"Tile Grid coordinate: {tileGridCoordinate}");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if(collision.gameObject.tag == "Dirty_Tile")
        {

            tilesPosition = collision.transform.position;
            print("WE TRIGGERED ON Tile: " + collision.gameObject.name);
            print("DIRT GOT SWEPT, DESTROYING MYSELF");


            // THIS WORKS, BUT ONLY TO WHERE WE ARE POINTING AT. 
            // TODO 
            // 1. Get all tiles withing our reach in an array
            // 2. Run dirtTilemap.SetTile(position, null); on all of these tiles with a for loop
            // 3. Success!!!
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int position = dirtGrid.WorldToCell(mousePos);
            dirtTilemap.SetTile(position, null);


        }

    }


}
