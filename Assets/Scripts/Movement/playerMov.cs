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
    private int targetTile; // the tile that the player is moving to; defined by its height relative to the player
    public int acceptableHeightDiff = 1; // the traversable difference between player position and target tile
    private float worldHeightChange; // how much we need to change the player's Y coordinate
    private int heightDiff = 0;

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

                // Define sampling area
        if (Math.Abs(moveDir.x) > 0 || Math.Abs(moveDir.y) > 0) {
            Vector3 position = this.transform.position; // the player's position in the world
            Vector2 samplingOffset; // how far i the direction of movement should sampling take place
            int playerZOffset = 1; // how much the player's Z position differs from the Z position of the floor
            Boolean isInReach = false; // whether there is a occupied tile spot within sampled area
            samplingOffset.x = 0.4f;
            samplingOffset.y = 0.2f;

            area.position = grid.WorldToCell(new Vector3(position.x + moveDir.x * samplingOffset.x, position.y + moveDir.y * samplingOffset.y, position.z - playerZOffset));
            area.min = new Vector3Int(area.position.x, area.position.y, area.position.z - 2);
            area.max = new Vector3Int(area.position.x + 1, area.position.y + 1, area.position.z + 5);

            // Cycle through sampled tiles to find the heighest tile, starting with the last tile space stored in the array (which is also the heighest)
            TileBase[] tileArray = tilemap.GetTilesBlock(area);
            for (int i = tileArray.Length - 1; i >= 0; i--)
            {
                if (tileArray[i] != null) { // As soon as we reach an occupied tile spot, make it the target tile and break the loop;
                    heightDiff = i - 2; // actual difference between player position and target tile
                    isInReach = true;
                    break;
                }
                
                // Debug.Log($"Tile {i}: {tileArray[i]}");
                Debug.Log($"Array Length = {tileArray.Length}");
            }

            Debug.Log(isInReach);
            // int heightDiff = i - 1; 

            if(isInReach == true && Math.Abs(heightDiff) <= acceptableHeightDiff) {
                worldHeightChange = heightDiff * 0.32f;
                // transform.position = new Vector3(transform.position.x, transform.position.y + worldHeightChange, transform.position.z + heightDiff);
                Debug.Log(heightDiff);
                transform.position = new Vector3(position.x, position.y, position.z + heightDiff);
                Move(); // If reachable and inside acceptable difference range, allow player to move;

            }
            else {
                // rb.velocity = Vector2.zero; // otherwise set player velocity to zero, imitating collision
            }
        }
        

    }

    private void FixedUpdate() 
    {

        

    }



}
