using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private AudioManager audioManager;
    public Animator meleeAttackAnimator;

    public int weaponDamage;
    public SpriteRenderer playerSpriteRenderer;

    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;
    public Sprite upPlayerSprite;
    public Sprite downPlayerSprite;

    private PlayerMovement playerMovement;

    // These lists of audioclips are populated in the inspector
    public List<AudioClip> ironSlicerSwooshSounds = new List<AudioClip>() { };
    public List<AudioClip> ironSlicerHitEnemySounds = new List<AudioClip>() { };



    public TrailRenderer attackTrailRenderer;
    public PolygonCollider2D meleeWeaponCollider;
    List<string> collisionTagsToIgnore = new List<string>()
    {
        "MyPlayer",
        "Tile",
    };

    public bool didMeleeAttackJustKillAnEnemy = false; // With this, the palyer can only attack one enemy at a time with melee

    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        playerMovement = GameObject.FindWithTag("MyPlayer").gameObject.GetComponent<PlayerMovement>();
    }


    public void Attack()
    {
        MakePlayerFaceWhereHeIsAiming();
        
        meleeAttackAnimator.SetTrigger("AttackIronSlicer");

    }

    public void ResetMovementAfterAttack()
    {
        //print("resetting movement");
        playerMovement.ResumePlayerAnimatorAndMovement();
        DisableWeaponTrailAndCollider(); // Just to be sure we deactivate it again
        didMeleeAttackJustKillAnEnemy = false;
    }
    public void EnableWeaponTrailAndCollider()
    {
        attackTrailRenderer.emitting = true;
        meleeWeaponCollider.enabled = true;
    }
    public void DisableWeaponTrailAndCollider()
    {
        attackTrailRenderer.emitting = false;
        meleeWeaponCollider.enabled = false;
    }
    
    public void PlaySoundEffectOnSwing()
    {
        audioManager.PlayRandomSoundEffectFromList(ironSlicerSwooshSounds, 0.3f, audioManager.playerWeaponSoundsSource);
    }
    private void OnTriggerEnter2D(Collider2D objectWeCollidedWith)
    {
        //print("------- Sword COLLIDED WITH: " + objectWeCollidedWith.gameObject.name);
        if (collisionTagsToIgnore.Contains(objectWeCollidedWith.gameObject.tag))
        {
            //print("THIS Sword collided with list of ignored tags, we will not acceppt cllision");
            return;
        }
        if (objectWeCollidedWith.gameObject.tag == "Enemy" && didMeleeAttackJustKillAnEnemy == false)
        {
            didMeleeAttackJustKillAnEnemy = true; // With this, the palyer can only attack one enemy at a time with melee
            objectWeCollidedWith.gameObject.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);
            audioManager.PlayRandomSoundEffectFromList(ironSlicerHitEnemySounds, 0.2f, audioManager.playerWeaponSoundsSource);
        }
    }

    private void MakePlayerFaceWhereHeIsAiming()
    {

        playerMovement.SetAllMovementToZeroAndBlockIncomingMovementInputs();

        Vector2 mousePointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePointerPos - (Vector2)transform.position).normalized;
        //print("direction IS: " + direction);

        // If mouse pointer is RIGHT of player (cone shaped from players origin)
        if (direction.x >= 0.1f && direction.y <= 0.6f && direction.y >= -0.7f)
        {
            playerMovement.SetLastMoveXdirection(1);
            playerMovement.SetLastMoveYdirection(0);
        }
        // If mouse pointer is LEFT of player (cone shaped from players origin)
        else if (direction.x <= -0.1f && direction.y <= 0.6f && direction.y >= -0.8f)
        {
            playerMovement.SetLastMoveXdirection(-1);
            playerMovement.SetLastMoveYdirection(0);
        }
        // If mouse pointer is ABOVE player (cone shaped from players origin)
        else if (direction.y >= 0.1f && direction.x >= -0.8f && direction.x <= 0.8f)
        {
            playerMovement.SetLastMoveXdirection(0);
            playerMovement.SetLastMoveYdirection(1);
        }
        // If mouse pointer is BELOW player (cone shaped from players origin)
        else if (direction.y <= -0.1f && direction.x >= -0.8f && direction.x <= 0.8f)
        {
            playerMovement.SetLastMoveXdirection(0);
            playerMovement.SetLastMoveYdirection(-1);
        }
        else
        {
            print("noone got it...");
        }

    }
}
