using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private PlayerSweeping playerSweeping;

    private MouseHovering mouseHovering;
    private AudioManager audioManager;
    private PauseMenu pauseMenu;





    public void Start()
    {
        playerSweeping = GetComponent<PlayerSweeping>();
        mouseHovering = GetComponent<MouseHovering>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        pauseMenu = GameObject.Find("GameManager").gameObject.GetComponent<PauseMenu>();

    }


    void Update()
    {
        // We still want to switch the pause screen on/off hence why this is here
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenu.SwitchPauseScreen();
        }
        // If the screen is paused, we dont want to act on ANY inputs
        if (pauseMenu.isScreenPaused == true)
        {
            return;
        }
        // -------------- BELOW LINES ONLY ACT WHEN SCREEN IS NOT IN PAUSED STATE  -------------- //

        // checks if mousbuttonLEFT is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // TODO - Rename this function maybe..??

            print("PRESSED MOUSEBUTTON");
            playerSweeping.PlaySweepAnimation();
            mouseHovering.DisableSwipeIcon();
            
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

        }


    }
}
