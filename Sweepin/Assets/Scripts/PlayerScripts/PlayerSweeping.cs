using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSweeping : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerAnimator = this.gameObject.GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sweep()
    {
        StartCoroutine(PlaySweepAnimation());
    }
    private IEnumerator PlaySweepAnimation()
    {
        // Disable movement
        playerMovement.enabled = false;
        rb.simulated = false;

        // Play the attack animation
        playerAnimator.SetBool("isAttacking", true);
        yield return null; // skip a frame and then set isAttacking to false so we wont loop the attack
        playerAnimator.SetBool("isAttacking", false);

        // Wait for double the amount of time that the current animation clip (sweeping) takes before allowing movement again.
        yield return new WaitForSeconds((playerAnimator.GetCurrentAnimatorStateInfo(0).length * 2));

        
        print("current clip length = " + playerAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Enable movement
        playerMovement.enabled = true;
        rb.simulated = true;
    }
}
