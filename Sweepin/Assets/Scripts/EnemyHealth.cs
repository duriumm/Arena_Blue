using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private AudioManager audioManager;
    public int health = 100;
    public List<AudioClip> enemyDeathAudioList = new List<AudioClip>() { };
    public GameObject enemyDeathParticleEffectGameObject;
    private WavesManager wavesManager;
    private SpriteRenderer enemySpriteRenderer;
    public Color unDamagedCharactedColor, damagedCharactedColor;

    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        wavesManager = GameObject.FindWithTag("WavesManager").GetComponent<WavesManager>();
        enemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;
        StartCoroutine(ChangeColorOnTakeDamage());
        if (health <= 0)
        {
            EnemyDeath();
        }
        //print("Panda took "+ damageToTake + "damage!");
    }

    public void EnemyDeath()
    {
        wavesManager.RemoveEnemyFromCounter();
        audioManager.PlayRandomSoundEffectFromList(enemyDeathAudioList, 0.2f, audioManager.enemySoundsSource);
        // Initiates destroying of the instantiated particle effect object after 2 seconds
        var instantiatedObject = Instantiate(enemyDeathParticleEffectGameObject, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(instantiatedObject, 2.0f);
        Destroy(this.gameObject);
    }

    private IEnumerator ChangeColorOnTakeDamage()
    {
        enemySpriteRenderer.color = damagedCharactedColor;
        yield return new WaitForSeconds(0.2f);
        enemySpriteRenderer.color = unDamagedCharactedColor;

    }
}
