using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundSwitcher : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioClip ambientSoundToPlay;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (audioManager.ambientSoundSource.clip != ambientSoundToPlay && collision.gameObject.tag == "MyPlayer")
        //{
        //    print("We triggered on ambient sound obj");
        //    audioManager.PlayAmbientSound(ambientSoundToPlay);
        //}

    }
}
