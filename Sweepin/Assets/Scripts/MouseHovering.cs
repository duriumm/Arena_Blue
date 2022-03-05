using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHovering : MonoBehaviour
{
    public GameObject usedGameobject;
    private Vector3 playerPreferedPosition;
    private Vector3 mousePos;
    private float distanceMouseToPlayer;
    Vector2 swipeableIconLocation = new Vector2(0,0);
    private bool isInRadius;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private PlayerSweeping playerSweeping;
    void Start()
    {
        playerSweeping = gameObject.GetComponent<PlayerSweeping>();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void Update()
    {
        // Calculate distance whenever
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPreferedPosition = this.gameObject.transform.position;
        playerPreferedPosition.y = playerPreferedPosition.y - 0.4f;
        distanceMouseToPlayer = Vector2.Distance(playerPreferedPosition, mousePos);


        swipeableIconLocation.Set(mousePos.x - 0.2f, mousePos.y + 0.3f);
        usedGameobject.transform.position = swipeableIconLocation;

        // If we are inside sweeping radius
        if (distanceMouseToPlayer < 0.7f && isInRadius == false)
        {
            print("in IF gang");
            EnableSwipeIcon();
            playerSweeping.SetIsInsideSweepingRadius(true);
            isInRadius = true;

        }
        else if(distanceMouseToPlayer > 0.7f && isInRadius == true) // If we are Outside sweeping radius
        {
            print("in else gang");
            DisableSwipeIcon();
            playerSweeping.StopSweepingAnimation();
            playerSweeping.SetIsInsideSweepingRadius(false);

            isInRadius = false;
        }
    }

    public void DisableSwipeIcon()
    {
        usedGameobject.SetActive(false);
    }

    public void EnableSwipeIcon()
    {
        usedGameobject.SetActive(true);
    }

    public bool IsDistanceBetweenPlayerAndMouseTooLarge()
    {
        if(distanceMouseToPlayer > 0.7f) { return true; }
        else { return false; }     
    }
        
}
