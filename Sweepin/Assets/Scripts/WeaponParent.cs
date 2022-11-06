using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is for changing the layering on the weapon if we look up or down
    
*/    
public class WeaponParent : MonoBehaviour
{
    private Vector2 mousePointerPos;
    private Vector2 direction;
 
    private SpriteRenderer weaponSprite;
    public SpriteRenderer projectileSprite;
    public SpriteRenderer meleeLeftPlayerHand;
    public SpriteRenderer meleeRightPlayerHand;   
    
    public SpriteRenderer bowLeftPlayerHand;
    public SpriteRenderer bowRightPlayerHand;

    private Vector2 scale;
    public enum CurrentActiveWeapon {
        Bow,
        Melee
    };
    public CurrentActiveWeapon currentActiveWeapon;
    


    private SpriteRenderer playerSprite;
    void Start()
    {

        weaponSprite = this.gameObject.transform.GetComponentInChildren<SpriteRenderer>();
        playerSprite = this.gameObject.transform.GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        mousePointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = (mousePointerPos - (Vector2)transform.position).normalized;

        direction = (mousePointerPos - (Vector2)transform.position).normalized;

        
        if(currentActiveWeapon == CurrentActiveWeapon.Melee)
        {
            // Sorting Layers
            // If mouse pointer is above player x axis
            if (direction.y > 0)
            {
                weaponSprite.sortingOrder = playerSprite.sortingOrder - 2;
                meleeLeftPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
                meleeRightPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;

            }
            // If mouse pointer is below player x axis
            else
            {
                weaponSprite.sortingOrder = playerSprite.sortingOrder + 1;
                projectileSprite.sortingOrder = weaponSprite.sortingOrder;
                meleeLeftPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
                meleeRightPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
            }
            // Flipping melee weapon sprite right/left
            scale = transform.localScale;
            if (direction.x > 0) { scale.y = -1; }
            else { scale.y = 1; }
            transform.localScale = scale;
        }
        else if(currentActiveWeapon == CurrentActiveWeapon.Bow)
        {
            print("bow");
            // Makes bow shoot straight arrows and not using melee weapons scale flip
            if(transform.localScale.y != 1) {
                print("changing scale");
                scale = transform.localScale;
                scale.y = 1;
                transform.localScale = scale;
            }

            // If mouse pointer is above player x axis
            if (direction.y > 0)
            {
                weaponSprite.sortingOrder = playerSprite.sortingOrder - 2;
                projectileSprite.sortingOrder = weaponSprite.sortingOrder - 1;
                bowLeftPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
                bowRightPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;

            }
            // If mouse pointer is below player x axis
            else
            {
                weaponSprite.sortingOrder = playerSprite.sortingOrder + 1;
                projectileSprite.sortingOrder = weaponSprite.sortingOrder;
                bowLeftPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
                bowRightPlayerHand.sortingOrder = weaponSprite.sortingOrder + 1;
            }
        }
    }
}
