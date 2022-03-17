using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isScreenPaused = true;
    private Animator playerAnimator;
    private GameObject broomPositionGameobject;

    void Start()
    {
        pauseMenu = GameObject.FindWithTag("Canvas").transform.Find("PauseMenu").gameObject;
        playerAnimator = GameObject.FindWithTag("MyPlayer").gameObject.GetComponent<Animator>();
        broomPositionGameobject = GameObject.FindWithTag("MyPlayer").gameObject.transform.Find("BroomPosition").gameObject;
        SwitchPauseScreen();
    }

    public void SwitchPauseScreen()
    {
        print("pausing screen: " + pauseMenu);
        if (isScreenPaused == true) // When screen is not active/paused
        {
            pauseMenu.SetActive(false);
            isScreenPaused = false;
            Time.timeScale = 1; // Resume game from paused state
            playerAnimator.enabled = true;
            broomPositionGameobject.SetActive(true);

        }
        else if(isScreenPaused == false) // When screen is paused
        {
            pauseMenu.SetActive(true);
            isScreenPaused = true;
            Time.timeScale = 0; // Pause game
            playerAnimator.enabled = false;
            broomPositionGameobject.SetActive(false);
        }
    }
}
