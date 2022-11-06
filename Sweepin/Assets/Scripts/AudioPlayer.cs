using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can be attached to any gameobject with the function to play a single audioclip or several
// We have this audioplayer since we need the script on the gameobject to play it from animation 
// Therefor we call this script and then the audiomanager to play the actual sound :)


// So basucally. If you want to play something from animation, attach THIS script.
// Else, just use audio manager

// Audiomanager seems to work fine.. not sure why i have this script?

public class AudioPlayer : MonoBehaviour
{
    
    private AudioManager audioManager;
    public List<AudioClip> audioClipList;
    [Header("Use this script on gameobject in animation to play audio")]
    public AudioClip audioClip;
    [Header("Enter volume from 0.0 to 1.0")]
    public float volume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void PlayOneRandomClipFromList()
    {
    
        audioManager.PlayRandomSoundEffectFromList(audioClipList, volume, audioManager.enemySoundsSource);
    }
    public void PlayAudioClip()
    {
        print("playing audio");
        audioManager.PlaySoundEffect(audioClip, volume, audioManager.enemySoundsSource);
    }
}
