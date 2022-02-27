using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D myPlayerRB;
    //public Animator bootsAnimator;        // Use when equipment is being added
    public Animator animator;

    //public float bootsSpeedGang;          // Use when equipment is being added

    //public GameObject feetSocketTest;     // Use when equipment is being added

    public Vector2 movement;

    public AudioManager audioManager;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Input for movement on both axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Animate character based on movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Save the last move position of the character so we can load the Idle animation correctly based on last move position
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }


        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // The Actual movement of the character
        myPlayerRB.MovePosition(myPlayerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Playing from here since the playing script need to be attached to the player
    // for the sound to be able to play in animation keyframe
    public void PlayerFootstepSound()
    {
        audioManager.PlaySound();

    }
    // Triggering of the footstep collider is done 2 times per player step
    // twice is because we want to check groundtype more often
    public void TriggerFootstepCollider()
    {
        audioManager.TurnOffThenOnAudioFootstepCollider();
    }
}