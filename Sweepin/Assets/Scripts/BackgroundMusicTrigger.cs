using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTrigger : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioClip backgroundMusic;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (audioManager.backgroundSoundSource.clip != backgroundMusic && collision.gameObject.tag == "MyPlayer")
        {
            print("We triggered on BG music sound obj");
            audioManager.PlayBackgroundMusic(backgroundMusic);
        }

    }
}
