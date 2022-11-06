using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioSwitcher : MonoBehaviour
{

    // Based on what we collide with we will change the currently active audio
    // for footsteps in the audio manager
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Here we run the audio switching function when we encounter a new tile collider.
    // Get the tiles name (dirt, grass, stone) to input as value to audioManager
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            //audioManager.SwitchSoundType(collision.name);
        }
    }
}
