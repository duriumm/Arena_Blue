using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private Vector3 playerPosition;
    void Start()
    {
        playerPosition = GameObject.Find("MyCharacter").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = mousePos;
        this.gameObject.transform.position = mousePos2D;
    }
}
