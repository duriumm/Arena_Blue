using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can be attached to any gameobject with the function to play a single audioclip or several
// We have this audioplayer since we need the script on the gameobject to play it from animation 
// Therefor we call this script and then the audiomanager to play the actual sound :)

public class AudioPlayer : MonoBehaviour
{
    public AudioManager audioManager;
    public List<AudioClip> audioClipList;
    public AudioClip audioClip;
    [Header("Enter volume from 0.0 to 1.0")]
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void PlayOneRandomClipFromList()
    {
    
        audioManager.PlayRandomSoundEffectFromList(audioClipList, volume);
    }
    private void PlayAudioClip()
    {
        audioManager.PlaySoundEffect(audioClip, volume);
    }
}
