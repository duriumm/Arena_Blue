using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public float movementSpeed = 10.0f;
    void Start()
    {
        player = GameObject.FindWithTag("MyPlayer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.position = Vector2.MoveTowards(
            this.gameObject.transform.position, player.transform.position, movementSpeed * Time.deltaTime
            );
    }
}
