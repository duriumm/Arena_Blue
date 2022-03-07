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
    private PlayerSweeping playerSweeping;
    [SerializeField]
    private float allowedSweepingDistance = 0.7f;

    // Set mousecursor to sprite
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot;
    
    void Start()
    {
        hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        playerSweeping = gameObject.GetComponent<PlayerSweeping>();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void Update()
    {
        // Calculate distance whenever
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPreferedPosition = this.gameObject.transform.position;
        playerPreferedPosition.y = playerPreferedPosition.y - 0.3f;
        distanceMouseToPlayer = Vector2.Distance(playerPreferedPosition, mousePos);


        swipeableIconLocation.Set(mousePos.x - 0.2f, mousePos.y + 0.3f);
        usedGameobject.transform.position = swipeableIconLocation;

        // If we are inside sweeping radius
        if (distanceMouseToPlayer < allowedSweepingDistance && isInRadius == false)
        {
            print("in IF gang");
            EnableSwipeIcon();
            playerSweeping.SetIsInsideSweepingRadius(true);
            isInRadius = true;

        }
        else if(distanceMouseToPlayer > allowedSweepingDistance && isInRadius == true) // If we are Outside sweeping radius
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
        if(distanceMouseToPlayer > allowedSweepingDistance) { return true; }
        else { return false; }     
    }
        
}
