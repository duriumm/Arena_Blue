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
    private PlayerMana playerMana;
    private Renderer sweepingMarkerRenderer;



    private bool isInsideSweepingRadius;

    private bool hasStoppedSweepingAnimOnce = false;

    private bool isMouseButtonHeldDown = false;

    private GameObject broomOnBack;

    private Vector3 downSweepingSquarePosition;

    public Sprite leftSweepSprite;
    public Sprite rightSweepSprite;
    public Sprite midSweepSprite;
    public Sprite idleSprite;

    private SpriteRenderer playerSpriteRenderer;

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
        playerMana = gameObject.GetComponent<PlayerMana>();

        broomOnBack = this.gameObject.transform.Find("CurrentlyEquippedSweeper").gameObject;
        downSweepingSquarePosition = this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position;
        //print("--------MARKER POS START: "+downSweepingSquarePosition);
        sweepingMarkerRenderer = this.gameObject.transform.Find("SweepingPosition").Find("Marker").GetComponent<SpriteRenderer>();
  
        StopSweepingAnimation();


        playerSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
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

        broomGameObj.SetActive(true);
        hasStoppedSweepingAnimOnce = false;
        print("In PlaySweepAnimation() ");
        playerAnimator.SetBool("isUsingAbility", true);
        broomOnBack.SetActive(false);

        playerMana.isManaIncreasing = false;

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
        broomOnBack.SetActive(true);
        playerAnimator.Play("Player_All_Idles");
        playerAnimator.SetBool("isUsingAbility", false);
    }
    public void HoldMouseButtonSweep()
    {
        //// If we are outside sweepingradius we should exit func
        //if (isInsideSweepingRadius == false)
        //{
        //    // This will stop the mana drain if we are holding down mousebutton inside sweeping radius and then dragging it outside radius
        //    if (playerMana.isManaIncreasing == false) { playerMana.isManaIncreasing = true; }

        //    isMouseButtonHeldDown = false;
        //    return;
        //}
        //// This cell will only play if we hold down the mousebutton outside the sweeping radius and drag it inside the sweepingradius
        //if(isMouseButtonHeldDown == false && broomGameObj.activeSelf == false && isInsideSweepingRadius == true)
        //{
        //    PlaySweepAnimation();
        //    mouseHovering.DisableSwipeIcon();
        //    isMouseButtonHeldDown = true;
        //    print("should set mana increase to false now");
        //    playerMana.isManaIncreasing = false;
        //}

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = mousePos;
        Vector3Int position = dirtGrid.WorldToCell(mousePos);

        // TESTING NEW NEW NEW WAY WITH MOVABLE SWEEPER WITH MOUSE // TESTING NEW NEW NEW WAY WITH MOVABLE SWEEPER WITH MOUSE
        this.gameObject.GetComponent<Animator>().enabled = false;
        Vector3 playerPosition = gameObject.transform.position;
        print("----- PLAYER POS: " + playerPosition);
        print("----- MOUSE POS: " + mousePos);



        // If mouse is to the left of player
        if (mousePos.x < playerPosition.x - 0.3f) 
        {
            playerSpriteRenderer.sprite = leftSweepSprite;
        
            if(this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position.x < playerPosition.x - 0.2f) { return; }
            else
            {
                Vector3 temp = new Vector3(-0.1f, 0, 0);
                this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position += temp;
            }        
        }
        // If mouse is to the right of player
        else if (mousePos.x > playerPosition.x + 0.3f)
        {

            if (this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position.x > playerPosition.x + 0.2f) { return; }
            else
            {
                Vector3 temp = new Vector3(+0.1f, 0, 0);
                this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position += temp;
            }
            playerSpriteRenderer.sprite = rightSweepSprite;
        }
        else
        {
            playerSpriteRenderer.sprite = midSweepSprite;
        }
        // TESTING NEW NEW NEW WAY WITH MOVABLE SWEEPER WITH MOUSE // TESTING NEW NEW NEW WAY WITH MOVABLE SWEEPER WITH MOUSE

       

        broomObjPosition = mousePos2D;
        broomObjPosition.y = broomObjPosition.y + 1; // This sets the broom to be higher (y axis) than the mouse so we can see what we are sweeping
        broomGameObj.transform.parent.position = broomObjPosition;

        Vector3 aboveMousePos = mousePos;
        aboveMousePos.y = aboveMousePos.y + 0.1f;
        Vector3Int abovePosition = dirtGrid.WorldToCell(aboveMousePos);

        Vector3 belowMousePos = mousePos;
        belowMousePos.y = belowMousePos.y - 0.1f;
        Vector3Int belowPosition = dirtGrid.WorldToCell(belowMousePos);

        Vector3 leftOfMousePos = downSweepingSquarePosition;
        leftOfMousePos.x = leftOfMousePos.x - 0.1f;
        Vector3Int leftOfPosition = dirtGrid.WorldToCell(leftOfMousePos);

        Vector3 rightOfMousePos = downSweepingSquarePosition;
        rightOfMousePos.x = rightOfMousePos.x + 0.1f;
        Vector3Int rightOfPosition = dirtGrid.WorldToCell(rightOfMousePos);



        


        // TESTING WITH THE NEW SWEEPING SYSTEM
        //Vector2 sweepPos2D = downSweepingSquarePosition;
        downSweepingSquarePosition = this.gameObject.transform.Find("SweepingPosition").Find("Marker").transform.position;
        Vector3Int markerSweepPosition = dirtGrid.WorldToCell(downSweepingSquarePosition);
        position = markerSweepPosition;
        //print("----- OLD V3INT POS: " + position);
        //print("----- NEW TEST V3INT POS: " + markerSweepPosition);




        string playersCurrentFacingDir = "";
        if (playerMovement.GetLastMoveXdirection() == 1f)
        {   // == Player is facing right
            //print("Player is facing right");
            playersCurrentFacingDir = "Right";

        }
        else if (playerMovement.GetLastMoveXdirection() == -1f)
        {   // == Player is facing left
            //print("Player is facing left");
            playersCurrentFacingDir = "Left";
        }
        else if (playerMovement.GetLastMoveYdirection() == 1f)
        {   // == Player is facing up
            //print("Player is facing up");
            playersCurrentFacingDir = "Up";
        }
        else if (playerMovement.GetLastMoveYdirection() == -1f)
        {   // == Player is facing down
            //print("Player is facing down");
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

            //// TEST WITH NEW SWEEPING SYSTEM
            //print("--------- HERE WE SHOULD REMOVE DIRT BELOW PLAYER");
            //dirtTilemap.SetTile(markerSweepPosition, null);
            //print("--------- In function MarkersweepPosition is: "+ markerSweepPosition);
          

        }
    }

    public void StopSweeping()
    {
        print("Stopped sweeping, printing once");
        playerMana.isManaIncreasing = true;
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
