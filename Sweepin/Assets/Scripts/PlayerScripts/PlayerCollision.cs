using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public float knockBackForce = 50f;
    private PlayerMovement playerMovement;
    public PlayerBowAttack playerAttack;
    private PlayerHealth playerHealth;

    private bool canPlayerGetKnockedBack = true;
    private AudioManager audioManager;
    public List<AudioClip> playerHurtAudioList = new List<AudioClip>() { };

    void Start()
    {
        playerRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();

    }

    // Moved the whole damage player part to ShotCollide script instead of here in trigggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "LootableItem")
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D objectCollidedWith)
    {
        if(objectCollidedWith.gameObject.tag == "Enemy")
        {
            playerHealth.TakeDamage(1); // Player always takes 1 damage. Its just the enemys difference in attacks that varies

            if (canPlayerGetKnockedBack == true)
            {
                print("COROUTINE COLLISION");
                StartCoroutine(KnockCoroutine(playerRigidBody2D, objectCollidedWith.gameObject, 1f));
            }
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D playerRigidBody2D, GameObject objectCollidedWith, float secondsToWait)
    {
        audioManager.PlayRandomSoundEffectFromList(playerHurtAudioList, 0.2f, audioManager.enemySoundsSource);
        canPlayerGetKnockedBack = false;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerRigidBody2D.velocity = Vector2.zero;
        Vector2 forceDirection = transform.position - objectCollidedWith.transform.position;

        //print("Force direction is: " + forceDirection);
        Vector2 force = forceDirection.normalized * knockBackForce;

        playerRigidBody2D.velocity = force;
        yield return new WaitForSeconds(secondsToWait);

        playerRigidBody2D.velocity = new Vector2();
        playerMovement.enabled = true;
        canPlayerGetKnockedBack = true;
        playerAttack.enabled = true;
    }
}
