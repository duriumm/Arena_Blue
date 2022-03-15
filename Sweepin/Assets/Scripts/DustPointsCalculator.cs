using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DustPointsCalculator : MonoBehaviour
{
    private GameObject dirtTilemapGameobject;
    private Tilemap dirtTilemap;
    private Grid dirtGrid;
    private TextMeshProUGUI amountOfDirtTilesLeftText;
    // Start is called before the first frame update
    void Start()
    {

        dirtTilemapGameobject = GameObject.Find("Dirty Tiles").transform.gameObject.transform.Find("Regular_Dirt_Stains").gameObject;
        dirtTilemap = dirtTilemapGameobject.GetComponent<Tilemap>();
        dirtGrid = GameObject.Find("Dirty Tiles").GetComponent<Grid>();
        amountOfDirtTilesLeftText = gameObject.transform.Find("DirtLeftText").gameObject.GetComponent<TextMeshProUGUI>();
        amountOfDirtTilesLeftText.text = GetAmountOfDirtTilesAtStartOfGame().ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetAmountOfDirtTilesAtStartOfGame()
    {
        dirtTilemap.CompressBounds(); // To only read the tiles that we have painted
        int amount = 0;
        foreach (var pos in dirtTilemap.cellBounds.allPositionsWithin)
        {
            Tile tile = dirtTilemap.GetTile<Tile>(pos);
            if(tile != null) { amount += 1; }
        }

        print("Amount of tiles: " + amount);
        return amount;
    }

    public void DecreaseAmountOfDirtLeft(int amountOfDirtToRemove)
    {
        print("Tiles removed was: " + amountOfDirtToRemove);
        int amountOfDirtInInteger = Convert.ToInt32(amountOfDirtTilesLeftText.text);
        amountOfDirtInInteger -= amountOfDirtToRemove;
        amountOfDirtTilesLeftText.text = amountOfDirtInInteger.ToString();
    }

}
