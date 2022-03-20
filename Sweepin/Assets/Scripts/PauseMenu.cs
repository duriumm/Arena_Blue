using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isScreenPaused = true;
    private Animator playerAnimator;
    private GameObject broomPositionGameobject;
    private GameObject blackBackgroundWhenPaused;

    void Start()
    {
        pauseMenu = GameObject.FindWithTag("Canvas").transform.Find("PauseMenu").gameObject;
        blackBackgroundWhenPaused = GameObject.FindWithTag("Canvas").transform.Find("BlackBackgroundWhenPaused").gameObject;


        playerAnimator = GameObject.FindWithTag("MyPlayer").gameObject.GetComponent<Animator>();
        broomPositionGameobject = GameObject.FindWithTag("MyPlayer").gameObject.transform.Find("BroomPosition").gameObject;
        SwitchPauseScreen();
    }

    public void SwitchPauseScreen()
    {
        print("pausing screen: " + pauseMenu);
        if (isScreenPaused == true) 
        {
            StartCoroutine(WaitBeforeExitingPauseMenu());
            // This is When we are exiting the paused menu
  

        }
        else if(isScreenPaused == false) // When screen is paused
        {
            // This is When we are Entering the paused menu
            pauseMenu.SetActive(true);
            isScreenPaused = true;
            Time.timeScale = 0; // Pause game
            playerAnimator.enabled = false;
            broomPositionGameobject.SetActive(false);
            blackBackgroundWhenPaused.SetActive(true);
        }
    }

    // The waiting when closing menu is here because the sound will otherwise get interrupted
    // TODO: Create sliding function for the menu on exiting
    IEnumerator WaitBeforeExitingPauseMenu()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSecondsRealtime(0.4f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isScreenPaused = false;

        Time.timeScale = 1; // Resume game from paused state
        playerAnimator.enabled = true;
        broomPositionGameobject.SetActive(true);
        pauseMenu.SetActive(false);
        blackBackgroundWhenPaused.SetActive(false);


    }
}
