using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private AudioClip audioClipToPlay;
    private AudioManager audioManager;
    private float audioClipLength;
    private bool isAudioReadyToPlay = true;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Debug.Log("Audio track length is: " + audioClipToPlay.length);
        audioClipLength = audioClipToPlay.length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAudioReadyToPlay == true && collision.gameObject.tag == "MyPlayer")
        {
            StartCoroutine(PlaySoundAndWaitForSeconds(audioClipLength));

        }
    }

    IEnumerator PlaySoundAndWaitForSeconds(float secondsToWait)
    {
        audioManager.PlayTriggerSound(audioClipToPlay);
        isAudioReadyToPlay = false;
        yield return new WaitForSeconds(secondsToWait);
        isAudioReadyToPlay = true;
        Debug.Log("WE ARE READY TO PLAY SOUND");
    }

}
