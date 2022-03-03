using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSweeping : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playerAnimator;
    private Animator sweepingAnimator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private GameObject dirtTilemapGameobject;
    private Tilemap dirtTilemap;
    private Grid dirtGrid;
    private GameObject sweepingAreaObj;
    private SpriteRenderer sweepingSprite;
    private Color sweepColor;
    private GameObject broomGameObj;


    void Start()
    {
        broomGameObj = this.gameObject.transform.Find("BroomPosition").Find("Broom").gameObject;
        sweepingAnimator = broomGameObj.GetComponent<Animator>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        dirtTilemapGameobject = GameObject.Find("Dirty Tiles").transform.gameObject.transform.Find("Regular_Dirt_Stains").gameObject;
        print("dirt tilemap gameobj: " + dirtTilemapGameobject);
        dirtTilemap = dirtTilemapGameobject.GetComponent<Tilemap>();
        dirtGrid = GameObject.Find("Dirty Tiles").GetComponent<Grid>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        sweepingAreaObj = gameObject.transform.Find("Colliders").transform.Find("Sweeparea").gameObject;
        sweepingSprite = sweepingAreaObj.GetComponent<SpriteRenderer>();
        sweepingSprite.color = Color.green;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySweepAnimation()
    {
        // Disable movement
        playerMovement.enabled = false;
        rb.simulated = false;

        // Play the attack animation
        sweepingAnimator.SetBool("isSweeping", true);
        print("plaing sweep");
        broomGameObj.SetActive(true);


    }
    public void StopSweepingAnimation()
    {
        sweepingAnimator.SetBool("isSweeping", false);
        print("stopping sweep sweep");
        broomGameObj.SetActive(false);
        //print("current clip length = " + sweepingAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Enable movement
        playerMovement.enabled = true;
        rb.simulated = true;
    }
    public void HoldMouseButtonSweep()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // TODO: Fix this below, doesnt work cos of animation using a specific position


        Vector3 playerPreferedPosition = this.gameObject.transform.position;
        playerPreferedPosition.y = playerPreferedPosition.y - 0.4f;
        print("player actual pos is: " + this.gameObject.transform.position);

        var distance = Vector2.Distance(playerPreferedPosition, mousePos); // Calculate the distance from players feet to the current mouse pos

        print("playerPrefered pos a little bit lower (at the feet) is: " + playerPreferedPosition);
        if (distance > 0.7f) //&&  MousePos is in AllowedDirectionToSweep -> MousePos must be PlayerPos and towards allowedDir )
        {

            sweepColor.a = 0;
            sweepingSprite.color = Color.green;
            sweepingSprite.color = sweepColor;
            return;
        }
        
        sweepingSprite.color = Color.green;
        sweepingSprite.color = sweepColor;
        sweepColor.a = 1;
        print("Mousepos is: " + mousePos);
        print("sweepObj is: " + sweepingAreaObj);

        Vector2 mousePos2D = mousePos;
        sweepingAreaObj.transform.position = mousePos2D;

        //broomGameObj.transform.parent.position = mousePos2D;

        Vector2 broomObjPosition = mousePos2D;
        broomObjPosition.y = broomObjPosition.y + 1; // This sets the broom to be higher (y) than the mouse so we can see what we are sweeping
        broomGameObj.transform.parent.position = broomObjPosition;


        Vector3 aboveMousePos = mousePos;
        aboveMousePos.y = aboveMousePos.y + 0.1f;
        Vector3Int abovePosition = dirtGrid.WorldToCell(aboveMousePos);

        Vector3 belowMousePos = mousePos;
        belowMousePos.y = belowMousePos.y - 0.1f;
        Vector3Int belowPosition = dirtGrid.WorldToCell(belowMousePos);

        Vector3 leftOfMousePos = mousePos;
        leftOfMousePos.x = leftOfMousePos.x - 0.1f;
        Vector3Int leftOfPosition = dirtGrid.WorldToCell(leftOfMousePos);

        Vector3 rightOfMousePos = mousePos;
        rightOfMousePos.x = rightOfMousePos.x + 0.1f;
        Vector3Int rightOfPosition = dirtGrid.WorldToCell(rightOfMousePos);



        Vector3Int position = dirtGrid.WorldToCell(mousePos);
        

        // TODO: 
        // 3. If the player is facing right -> Return from function sweeping if mousePosition is not to the right of player
        // if(playerpos == right && mousePos == right){ sweep() }
        //print("Distance between player and mouse is: " + distance);

        // TODO: 
        // 1. Get the sweepingCollider to follow the mousepointer



       


        string playersCurrentFacingDir = "";
        if (playerMovement.GetLastMoveXdirection() == 1f)
        {   // == Player is facing right
            print("Player is facing right");
            playersCurrentFacingDir = "Right";
        }
        else if (playerMovement.GetLastMoveXdirection() == -1f)
        {   // == Player is facing left
            print("Player is facing left");
            playersCurrentFacingDir = "Left";
        }
        else if (playerMovement.GetLastMoveYdirection() == 1f)
        {   // == Player is facing up
            print("Player is facing up");
            playersCurrentFacingDir = "Up";
        }
        else if (playerMovement.GetLastMoveYdirection() == -1f)
        {   // == Player is facing down
            print("Player is facing down");
            playersCurrentFacingDir = "Down";
        }


        // TODO : CREATE SO IF PLAYER IS LOOKING DOWN WE CAN ONLY SWEEP LOWER THAN HIS CURRENT Y POSITION && DISTANCE OF 0.7F

        if (playersCurrentFacingDir == "Right" && mousePos.x > playerPreferedPosition.x + 0.1f)
        {
            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(abovePosition, null);
            dirtTilemap.SetTile(belowPosition, null);

        }
        else if (playersCurrentFacingDir == "Left" && mousePos.x < playerPreferedPosition.x - 0.1f)
        {
            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(abovePosition, null);
            dirtTilemap.SetTile(belowPosition, null);
        }
        else if (playersCurrentFacingDir == "Up" && mousePos.y > playerPreferedPosition.y + 0.1f)
        {
            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(leftOfPosition, null);
            dirtTilemap.SetTile(rightOfPosition, null);
        }
        else if (playersCurrentFacingDir == "Down" && mousePos.y < playerPreferedPosition.y - 0.1f)
        {
            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(leftOfPosition, null);
            dirtTilemap.SetTile(rightOfPosition, null);
        }
    }

    public void StopSweeping()
    {
        sweepColor.a = 0;
        sweepingSprite.color = Color.green;
        sweepingSprite.color = sweepColor;
    }
}
