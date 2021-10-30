using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMov : MonoBehaviour
{
    private Vector2 moveDir;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private BoundsInt area; // area of sampled tiles
    public Tilemap tilemap;
    public GridLayout grid;
    private Vector3 position; // the player's position in the world
    private int targetTile; // the tile that the player is moving to; defined by its height relative to the player
    private Boolean isInReach; // whether there is a occupied tile spot within sampled area
    public int acceptableHeightDiff = 1; // the traversable difference between player position and target tile
    private float worldHeightChange; // how much we need to change the player's Y coordinate

    // Start is called before the first frame update
    void Start()
    {


    }
    
    private void Move() {
        //Add velocity to player
        rb.AddForce(moveDir * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Process inputs
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        // Cap direction vector to 1 to prevent diagonal movement from being faster than cardinal
        if (Math.Abs(moveDir.x) + Math.Abs(moveDir.y)  > 1)
        {
           moveDir.Normalize();  
        }

    }

    private void FixedUpdate() 
    {
        // Define sampling area
        position = this.transform.position;

        area.position = grid.WorldToCell(new Vector3(position.x + moveDir.x * 0.2f, position.y + moveDir.y * 0.2f, position.z));
        area.min = new Vector3Int(area.position.x, area.position.y, area.position.z - 2);
        area.max = new Vector3Int(area.position.x + 1, area.position.y + 1, area.position.z + 4);

        // Cycle through sampled tiles to find the heighest tile, starting with the last tile space stored in the array (which is also the heighest)
        TileBase[] tileArray = tilemap.GetTilesBlock(area);
        for (int i = tileArray.Length - 1; i >= 0; i--)
        {
            if (tileArray[i] != null) { // As soon as we reach an occupied tile spot, make it the target tile and break the loop;
                targetTile = i;
                isInReach = true;
                break;
            }
            isInReach = false; // if no such spot is found, assume there are none for the time being and continue cycle
            
            // Debug.Log($"Tile {i}: {tileArray[i]}");
        }

        Debug.Log(isInReach);
        int heightDiff = targetTile - 1; // actual difference between player position and target tile
        Debug.Log(heightDiff);

        if(isInReach == true && Math.Abs(heightDiff) <= acceptableHeightDiff) {
            Move(); // If reachable and inside acceptable difference range, allow player to move;
            worldHeightChange = heightDiff * 0.32f;
            // transform.position = new Vector3(transform.position.x, transform.position.y + worldHeightChange, transform.position.z + heightDiff + 1);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + heightDiff);
        }
        else {
            // rb.velocity = Vector2.zero; // otherwise set player velocity to zero, imitating collision
        }
        

    }



}
