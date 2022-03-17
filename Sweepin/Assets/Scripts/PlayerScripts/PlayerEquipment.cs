using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private Animator walkingAnimator;
    private SpriteRenderer equipmentOnPlayersBack;
    private SpriteRenderer playerSpriteRenderer;
    void Start()
    {
        walkingAnimator = gameObject.GetComponent<Animator>();
        equipmentOnPlayersBack = gameObject.transform.Find("CurrentlyEquippedSweeper").gameObject.GetComponent<SpriteRenderer>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public void SwitchEquipmentLayerOnPlayersBack()
    {
        
        if (walkingAnimator.GetFloat("Vertical") == 1f || walkingAnimator.GetFloat("LastMoveY") == 1f)
        {
            //print("walkin up");
            // Set broom to layer 3
            equipmentOnPlayersBack.sortingOrder = (playerSpriteRenderer.sortingOrder + 1);
        }
        else
        {
            //print("NOT walkin up");
            // set broom to layer 1
            equipmentOnPlayersBack.sortingOrder = (playerSpriteRenderer.sortingOrder - 1);
        }
    }

    
}
