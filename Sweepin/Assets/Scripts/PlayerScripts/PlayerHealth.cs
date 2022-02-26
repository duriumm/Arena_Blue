using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    private Slider mySlider;
    private GameObject canvasPrefab;
    private GameObject myPlayerObject;
    private Vector2 playerSpawnPoint;
    void Start()
    {
        canvasPrefab = GameObject.FindWithTag("Canvas");
        mySlider = canvasPrefab.transform.GetChild(0).gameObject.GetComponent<Slider>();
 

        mySlider.value = playerHealth;
        myPlayerObject = this.gameObject;
        playerSpawnPoint = myPlayerObject.transform.position;

    }


    public void TakeDamage(int damageToReceive)
    {
        playerHealth -= damageToReceive;
        mySlider.value = playerHealth;
        if (playerHealth <= 0)
        {
            respawnPlayer();
        }
    }

    public void GainHealth(int healthToGain)
    {
        playerHealth += healthToGain;
        mySlider.value = playerHealth;
    }

    private void respawnPlayer()
    {
        myPlayerObject.transform.position = playerSpawnPoint;
        playerHealth = 100;
        mySlider.value = playerHealth;

    }
}