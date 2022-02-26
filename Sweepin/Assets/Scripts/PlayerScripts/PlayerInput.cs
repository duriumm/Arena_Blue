using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private PlayerSweeping playerSweeping;


    public void Start()
    {
        playerSweeping = GetComponent<PlayerSweeping>();
    }


    void Update()
    {

        // checks if mousbuttonLEFT is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // TODO - Rename this function maybe..??
            playerSweeping.Sweep();



        }
    }
}
