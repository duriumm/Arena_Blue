using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private SpriteRenderer sweepingSprite;
    private GameObject broomGameObj;
    private Vector3 mousePos;
    private Vector2 mousePos2D;
    private Vector2 broomObjPosition;
    private MouseHovering mouseHovering;
    private DustPointsCalculator dustPointsCalculator;


    private bool isInsideSweepingRadius;

    private bool hasStoppedSweepingAnimOnce = false;

    private bool isMouseButtonHeldDown = false;
    


    void Start()
    {
        mouseHovering = this.gameObject.GetComponent<MouseHovering>();
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
        dustPointsCalculator = GameObject.Find("Canvas").gameObject.transform.Find("DirtLeftImage").GetComponent<DustPointsCalculator>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySweepAnimation()
    {
        if (!isInsideSweepingRadius)
        {
            return;
        }
        // Disable movement
        playerMovement.enabled = false;
        rb.simulated = false;

        // Play the attack animation
        sweepingAnimator.SetBool("isSweeping", true);
        print("plaing sweep");
        broomGameObj.SetActive(true);
        hasStoppedSweepingAnimOnce = false;
        print("SHOULD PLAY ABILITY NOW");
        playerAnimator.SetBool("isUsingAbility", true);


    }
    public void StopSweepingAnimation()
    {
        if(hasStoppedSweepingAnimOnce == true)
        {
            return;
        }
        sweepingAnimator.SetBool("isSweeping", false);
        print("stopping sweep sweep");
        broomGameObj.SetActive(false);

        // Enable movement
        playerMovement.enabled = true;
        rb.simulated = true;
        hasStoppedSweepingAnimOnce = true;

        playerAnimator.SetBool("isUsingAbility", false);
    }
    public void HoldMouseButtonSweep()
    {
        if (isInsideSweepingRadius == false)
        {
            isMouseButtonHeldDown = false;
            return;
        }
        // This cell will only play once whenever we are pressing the mouse button
        if(isMouseButtonHeldDown == false && broomGameObj.activeSelf == false && isInsideSweepingRadius == true)
        {
            PlaySweepAnimation();
            mouseHovering.DisableSwipeIcon();
            isMouseButtonHeldDown = true;

        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = mousePos;

        broomObjPosition = mousePos2D;
        broomObjPosition.y = broomObjPosition.y + 1; // This sets the broom to be higher (y axis) than the mouse so we can see what we are sweeping
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


        // TODO : REMAKE THIS CODE TO BE NICER TO READ. WE ARE NOT REUSING VARIABLES ETCC ETC
        int tilesRemoved = 0;
        if (playersCurrentFacingDir == "Right")
        {
            if (dirtTilemap.GetTile(position) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(abovePosition) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(belowPosition) != null) { tilesRemoved += 1; }

            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(abovePosition, null);
            dirtTilemap.SetTile(belowPosition, null);

            dustPointsCalculator.DecreaseAmountOfDirtLeft(tilesRemoved);

        }
        else if (playersCurrentFacingDir == "Left")
        {
            if (dirtTilemap.GetTile(position) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(abovePosition) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(belowPosition) != null) { tilesRemoved += 1; }

            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(abovePosition, null);
            dirtTilemap.SetTile(belowPosition, null);

            dustPointsCalculator.DecreaseAmountOfDirtLeft(tilesRemoved);
        }
        else if (playersCurrentFacingDir == "Up")
        {
            if (dirtTilemap.GetTile(position) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(leftOfPosition) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(rightOfPosition) != null) { tilesRemoved += 1; }

            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(leftOfPosition, null);
            dirtTilemap.SetTile(rightOfPosition, null);

            dustPointsCalculator.DecreaseAmountOfDirtLeft(tilesRemoved);
        }
        else if (playersCurrentFacingDir == "Down")
        {
            if (dirtTilemap.GetTile(position) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(leftOfPosition) != null) { tilesRemoved += 1; }
            if (dirtTilemap.GetTile(rightOfPosition) != null) { tilesRemoved += 1; }

            dirtTilemap.SetTile(position, null);
            dirtTilemap.SetTile(leftOfPosition, null);
            dirtTilemap.SetTile(rightOfPosition, null);

            dustPointsCalculator.DecreaseAmountOfDirtLeft(tilesRemoved);
        }
    }

    public void StopSweeping()
    {
        print("Stopped sweeping, only a print - no code");
        
    }

    public void SetIsInsideSweepingRadius(bool isMouseInsideSweepingRadius)
    {
        isInsideSweepingRadius = isMouseInsideSweepingRadius;
    }

    public void ActivateBroomObj()
    {
        broomGameObj.SetActive(true);
    }
    public void DeactivateBroomObj()
    {
        broomGameObj.SetActive(false);
    }
}
