using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : MonoBehaviour
{
    public Animator bowAnimator;
    public Animator movementAnimator;

    private GameObject weaponsProjectileToCopy;
    private GameObject instantiatedProjectile;
    private AudioManager audioManager;

    public float bowShootingPower;
    public SpriteRenderer playerSpriteRenderer;

    public Sprite leftPlayerSprite;
    public Sprite rightPlayerSprite;
    public Sprite upPlayerSprite;
    public Sprite downPlayerSprite;

    private PlayerMovement playerMovement;


    public bool isBowFullyCharged;
    // These lists of audioclips are populated in the inspector
    public List<AudioClip> weaponChargeupAudioList = new List<AudioClip>(){};
    public List<AudioClip> weaponAttackAudioList = new List<AudioClip>() { };

    void Start()
    {
        weaponsProjectileToCopy = gameObject.transform.Find("Projectile").gameObject;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        playerMovement = GameObject.FindWithTag("MyPlayer").gameObject.GetComponent<PlayerMovement>();
    }


    public void ResetBowPropertiesForIdleState()
    {
        isBowFullyCharged = false;
        //print("In idle state again");
        weaponsProjectileToCopy.SetActive(true);
        //print("WE ARE RESETTING SHIT");
        playerMovement.ResumePlayerAnimatorAndMovement();
    }


    public void ChargeBow()
    {

        MakePlayerFaceWhereHeIsAiming();
        bowAnimator.SetTrigger("ChargingBow");
        audioManager.PlayRandomSoundEffectFromList(weaponChargeupAudioList, 0.5f, audioManager.enemySoundsSource);
    }

    public void BowIsFullyCharged()
    {
        isBowFullyCharged = true;
        playerMovement.PausePlayerAnimatorAndMovement();
    }


    public void InstantiateProjectileAndShoot()
    {
        if (!isBowFullyCharged) 
        {
            bowAnimator.SetTrigger("BowDidNotFullyCharge");
            isBowFullyCharged = false;
            return;
        }
        audioManager.PlayRandomSoundEffectFromList(weaponAttackAudioList, 0.5f, audioManager.enemySoundsSource);
        bowAnimator.SetTrigger("ShootBow");
        instantiatedProjectile = Instantiate(weaponsProjectileToCopy, weaponsProjectileToCopy.transform.position, weaponsProjectileToCopy.transform.rotation);
        
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mouseCursorPos - instantiatedProjectile.GetComponent<Rigidbody2D>().position;
        lookDir.Normalize();
        // The original sprite for the players arrow should appear gone as reloading for a while
        weaponsProjectileToCopy.SetActive(false);
        instantiatedProjectile.GetComponent<Rigidbody2D>().velocity = lookDir * bowShootingPower;
        instantiatedProjectile.GetComponent<Projectile>().isProjectilePlayersOriginal = false;
        // Collider should be enabled for instantiated objects but not the players original projectile object
        instantiatedProjectile.GetComponent<PolygonCollider2D>().enabled = true;
        isBowFullyCharged = false;
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
