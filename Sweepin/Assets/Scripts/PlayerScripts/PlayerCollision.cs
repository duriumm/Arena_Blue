using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject myPlayer;

    void Start()
    {
        myPlayer = this.gameObject;       
    }

    // Moved the whole damage player part to ShotCollide script instead of here in trigggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "LootableItem")
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collisionenter2d on PLAYER fired");
    }
}
