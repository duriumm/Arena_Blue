using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The audiomanager keeps track of all the sounds we can play and also actually plays them
// To trigger a sound from the animation we need the "Audioplayer" script attached to the gameobject

public class AudioManager : MonoBehaviour
{

    private AudioClip grass_footstep_1;
    private AudioClip dirt_footstep;
    private AudioClip sand_footstep;
    private AudioClip woodplank_footstep;
    private AudioClip beachloop_ambient;
    private AudioClip sweep_1;
    private AudioClip sweep_2;
    private AudioClip sweep_3;

    static AudioSource audioSource;
    private AudioClip currentActiveFootstepSound;
    private GameObject footStepColliderObject;
    public AudioSource ambientSoundSource;
    public AudioSource backgroundSoundSource;
    public AudioSource soundEffectsSource;
    private GameObject player;
    


    void Start()
    {
        sweep_1 = Resources.Load<AudioClip>("/SweepSounds/sweep_1");
        sweep_2 = Resources.Load<AudioClip>("/SweepSounds/sweep_2");
        sweep_3 = Resources.Load<AudioClip>("/SweepSounds/sweep_3");
        grass_footstep_1 = Resources.Load<AudioClip>("grass_footstep_1");
        dirt_footstep = Resources.Load<AudioClip>("dirt_footstep");
        sand_footstep = Resources.Load<AudioClip>("sand_footstep");
        woodplank_footstep = Resources.Load<AudioClip>("woodplanks_footstep");
        beachloop_ambient = Resources.Load<AudioClip>("Beachloop");
        player = GameObject.Find("MyCharacter");

        audioSource = GetComponent<AudioSource>();
        ambientSoundSource = GameObject.Find("AmbientSound_1").GetComponent<AudioSource>();
        backgroundSoundSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        soundEffectsSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();

        footStepColliderObject = player.transform.Find("Colliders").transform.Find("FootstepCollider").gameObject;
        currentActiveFootstepSound = grass_footstep_1; // Starting footstep is always grass on sceneload, switch this later on
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.pitch = Random.Range(0.90f, 1.10f);
        audioSource.PlayOneShot(currentActiveFootstepSound);
    }

    public void TurnOffThenOnAudioFootstepCollider()
    {
        footStepColliderObject.SetActive(false);
        footStepColliderObject.SetActive(true);
    }
    // Not great using magic strings here so preferably an enum for future use in audioManager
    public void SwitchSoundType(string typeOfGround)
    {
        switch (typeOfGround)
        {
            // Add more cases here which should be the name of the Tile gameobject stepped on
            case "Dirt":
                currentActiveFootstepSound = dirt_footstep;
                break;
            case "Grass":
                currentActiveFootstepSound = grass_footstep_1;
                break;
            case "Sand":
                currentActiveFootstepSound = sand_footstep;
                break;
            case "WoodFloor":
                currentActiveFootstepSound = woodplank_footstep;
                break;
            default:
                break;
        }

    }

    public void PlayAmbientSound(AudioClip ambientSoundClip)
    {
        ambientSoundSource.clip = ambientSoundClip;
        ambientSoundSource.Play();
    }

    public void PlayBackgroundMusic(AudioClip backgroundMusicClip)
    {
        backgroundSoundSource.clip = backgroundMusicClip;
        backgroundSoundSource.Play();
    }

    // This will pitch just like the walking sounds but i think its fine since thats interesting :)
    public void PlayTriggerSound(AudioClip audioClipToPlay)
    {
        audioSource.PlayOneShot(audioClipToPlay);
        Debug.Log("We shud play old man");
    }

    public void PlayRandomSoundEffectFromList(List<AudioClip> audiosClipToPlay, float volume)
    {
        soundEffectsSource.volume = volume;
        int randNumber = Random.Range(0, audiosClipToPlay.Count);
        soundEffectsSource.PlayOneShot(audiosClipToPlay[randNumber]);
    }

    public void PlaySoundEffect(AudioClip audioClipToPlay, float volume)
    {
        print("in audiomanager playng");
        soundEffectsSource.volume = volume;
        soundEffectsSource.PlayOneShot(audioClipToPlay);
    }
    public void PlayGUISoundEffect(AudioClip audioClipToPlay)
    {
        print("Playing GUI effect");
        soundEffectsSource.volume = 1.0f;
        soundEffectsSource.PlayOneShot(audioClipToPlay);
    }

    public void StopPlayingCurrentSoundEffect()
    {
        soundEffectsSource.Stop();
    }

}
