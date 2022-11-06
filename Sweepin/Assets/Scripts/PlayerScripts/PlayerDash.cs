using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D playerRigidBody2D;
    private PlayerMovement playerMovement;
    public int dashPower = 20;
    public bool canPlayerDash = true;
    private float timeItTakesToDash = 0.5f;
    public TrailRenderer playerTrailRenderer;
    private AudioManager audioManager;
    public AudioClip dashAudio;

    void Start()
    {
        playerRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }


    void Update()
    {
        
    }

    public IEnumerator DashTowardsCursor()
    {
        canPlayerDash = false;
        Vector3 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 forceDirection = mouseCursorPos - transform.position;
        playerMovement.enabled = false;
        playerRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody2D.velocity = new Vector2(0, 0);
        audioManager.PlaySoundEffect(dashAudio, 0.3f, audioManager.soundEffectsSource);
        playerRigidBody2D.AddForce(forceDirection.normalized * dashPower, ForceMode2D.Impulse);
        playerTrailRenderer.emitting = true;

        yield return new WaitForSeconds(timeItTakesToDash);

        playerMovement.enabled = true;
        canPlayerDash = true;
        playerTrailRenderer.emitting = false;
        print("player velocity is: " + playerRigidBody2D.velocity);
    }
}
