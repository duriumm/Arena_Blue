using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * PlayerHealth is attached to the player and connects to the heartsmanager which is the one removing/adding hearts.
 * The reason we have two scripts is that this PlayerHealth script is the one taking the damage and is attached
 * to the player. So the playerhealth is just pushing the data forward to heartsmanager
 */ 
public class PlayerHealth : MonoBehaviour
{
    private GameObject myPlayerObject;
    private Vector2 playerSpawnPoint;
    public SpriteRenderer playerBodySprite, playerRightHandSprite, playerLeftHandSprite;
    public Color unDamagedCharactedColor, damagedCharactedColor;
    public HeartsManager heartsManager;
    public GameObject deathCanvas;
    public GameObject playerDeathParticleEffectGameObject;
    private GuiManager guiManager;

    public int currentAmountOfHearts;
    private bool didPlayerDie = false;
    void Start()
    {
        myPlayerObject = this.gameObject;
        playerSpawnPoint = myPlayerObject.transform.position;
        guiManager = GameObject.Find("GuiManager").GetComponent<GuiManager>();

    }

    // Damage is hearts based where 1 damage equals one heart removed
    public void TakeDamage(int heartsToRemove)
    {
        currentAmountOfHearts = heartsManager.DecreaseHearts(heartsToRemove);

        if (currentAmountOfHearts <= 0)
        {
            StopAllCoroutines();
            var pl = gameObject.GetComponent<PlayerCollision>();
            pl.StopAllCoroutines();
            PlayerDeath();
        }
        else
        {
            print("COROUTINE CHANGE COLOR");

            StartCoroutine(ChangeColorOnTakeDamage());
        }

    }

    public void GainHealth(int heartsToAdd)
    {
        heartsManager.IncreaseHearts(heartsToAdd);
    }

    private void respawnPlayer()
    {
        myPlayerObject.transform.position = playerSpawnPoint;
        heartsManager.IncreaseHearts(3);
    }

    private IEnumerator ChangeColorOnTakeDamage()
    {
        playerBodySprite.color = damagedCharactedColor;
        playerRightHandSprite.color = damagedCharactedColor;
        playerLeftHandSprite.color = damagedCharactedColor;
        yield return new WaitForSeconds(0.2f);
        playerBodySprite.color = unDamagedCharactedColor;
        playerRightHandSprite.color = unDamagedCharactedColor;
        playerLeftHandSprite.color = unDamagedCharactedColor;
    }

    public void PlayerDeath()
    {
        playerDeathParticleEffectGameObject.SetActive(true);
        foreach (Transform child in myPlayerObject.transform)
        {
            child.gameObject.SetActive(false);

        }
        var animator = myPlayerObject.GetComponent<Animator>();
        animator.SetTrigger("PlayerDeath");
    }


    // This function is to be able to call it from the animation
    public void OpenDeathScreenCanvas()
    {
        guiManager.ToggleMenu("DeathCanvas");
    }
}