using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D myPlayerRB;

    public Animator animator;

    public Vector2 movement;

    public GameObject audioManagerGameobject;

    private bool playerMovementIsPaused = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovementIsPaused)
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


    }

    private void FixedUpdate()
    {
        if (!playerMovementIsPaused)
        {
            // The Actual movement of the character
            myPlayerRB.MovePosition(myPlayerRB.position + movement * moveSpeed * Time.fixedDeltaTime);

        }
    }

    public float GetLastMoveXdirection()
    {
        return animator.GetFloat("LastMoveX");
    }
    public float GetLastMoveYdirection()
    {
        return animator.GetFloat("LastMoveY");
    }

    public void SetLastMoveXdirection(int lastMoveXinput)
    {
        animator.SetFloat("LastMoveX", lastMoveXinput);
        //print("set lat x");
    }
    public void SetLastMoveYdirection(int lastMoveYinput)
    {
        animator.SetFloat("LastMoveY", lastMoveYinput);
    }

    /// <summary> Blocks incoming inputs so we can change facing directions afterwards </summary>
    public void SetAllMovementToZeroAndBlockIncomingMovementInputs()
    {
        myPlayerRB.velocity = new Vector2(0, 0);
        playerMovementIsPaused = true;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("LastMoveX", 0);
        animator.SetFloat("LastMoveY", 0);
    }

    /// <summary> This is the actual pausing of the movement and Rigidbody2d </summary>
    public void PausePlayerAnimatorAndMovement()
    {
        myPlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        myPlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.enabled = false;
    }
    public void ResumePlayerAnimatorAndMovement()
    {
        playerMovementIsPaused = false;
        myPlayerRB.constraints = RigidbodyConstraints2D.None;
        myPlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.enabled = true;

    }
}