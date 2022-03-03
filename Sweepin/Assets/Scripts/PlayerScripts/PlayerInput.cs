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

            print("PRESSED MOUSEBUTTON");
            playerSweeping.PlaySweepAnimation();

        }

        // Is active WHILE mousebuttonLEFT is held down
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Held");
             // Currently the sweepanimation is player swinging sword. 
            playerSweeping.HoldMouseButtonSweep();
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerSweeping.StopSweepingAnimation();
            playerSweeping.StopSweeping();
        }
    }
}
