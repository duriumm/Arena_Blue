using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The audiomanager keeps track of all the sounds we can play and also actually plays them
// To trigger a sound from the animation we need the "Audioplayer" script attached to the gameobject

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEffectsSource;
    public AudioSource enemySoundsSource;
    public AudioSource playerWeaponSoundsSource;



    void Start()
    {
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        enemySoundsSource = GameObject.Find("EnemySounds").GetComponent<AudioSource>();
        playerWeaponSoundsSource = GameObject.Find("PlayerWeaponSoundsSource").GetComponent<AudioSource>();
    }


    ///<summary> Enter list of audioclips and float bolume from 0.0f - 1.0f </summary>
    public void PlayRandomSoundEffectFromList(List<AudioClip> audiosClipToPlay, float volume, AudioSource audioSourceToUse)

    {
        audioSourceToUse.volume = volume;
        int randNumber = Random.Range(0, audiosClipToPlay.Count);
        audioSourceToUse.PlayOneShot(audiosClipToPlay[randNumber]);
    }

    public void PlaySoundEffect(AudioClip audioClipToPlay, float volume, AudioSource audioSourceToUse)
    {
        audioSourceToUse.volume = volume;
        audioSourceToUse.PlayOneShot(audioClipToPlay);
    }
    public void PlayGUISoundEffect(AudioClip audioClipToPlay, AudioSource audioSourceToUse)
    {
        //print("Playing GUI effect");
        audioSourceToUse.volume = 1.0f;
        audioSourceToUse.PlayOneShot(audioClipToPlay);
    }

    public void StopPlayingCurrentSoundEffect(AudioSource audioSourceToUse)
    {
        audioSourceToUse.Stop();
    }

}
