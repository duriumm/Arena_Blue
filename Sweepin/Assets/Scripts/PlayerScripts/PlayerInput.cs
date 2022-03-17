using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private PlayerSweeping playerSweeping;

    private MouseHovering mouseHovering;
    private AudioManager audioManager;
    private PlayerMana playerMana;





    public void Start()
    {
        playerSweeping = GetComponent<PlayerSweeping>();
        mouseHovering = GetComponent<MouseHovering>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerMana = gameObject.GetComponent<PlayerMana>();



    }


    void Update()
    {

        // checks if mousbuttonLEFT is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // TODO - Rename this function maybe..??

            print("PRESSED MOUSEBUTTON");
            playerSweeping.PlaySweepAnimation();
            mouseHovering.DisableSwipeIcon();
            playerMana.isManaIncreasing = false;
        }

        // Is active WHILE mousebuttonLEFT is held down
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Held");
             // Currently the sweepanimation is player swinging sword. 
            playerSweeping.HoldMouseButtonSweep();
        }
        // Gets called ONCE when we release mousebutton 0
        if (Input.GetMouseButtonUp(0))
        {
            playerSweeping.StopSweepingAnimation();
            playerSweeping.StopSweeping();
            if (!mouseHovering.IsDistanceBetweenPlayerAndMouseTooLarge())
            {
                mouseHovering.EnableSwipeIcon();
            }
            audioManager.StopPlayingCurrentSoundEffect();
            playerMana.isManaIncreasing = true;
        }
    }
}
