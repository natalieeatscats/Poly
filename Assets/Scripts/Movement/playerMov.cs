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
    public PolygonCollider2D coll;
    private BoundsInt area;
    public Tilemap tilemap;
    public GridLayout grid;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //Process inputs
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        //Cap direction vector to 1 to prevent diagonal movement from being faster than cardinal
        if (moveDir.x + moveDir.y > 1)
        {
           moveDir.Normalize();  
        }

        position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        area.position = grid.WorldToCell(new Vector3(position.x + moveDir.x, position.y + moveDir.y, position.z));
        // area.position = grid.WorldToCell(position);
        area.min = new Vector3Int(area.position.x, area.position.y, area.position.z - 2);
        area.max = new Vector3Int(area.position.x + 1, area.position.y + 1, area.position.z + 4);

        TileBase[] tileArray = tilemap.GetTilesBlock(area);
        for (int index = 0; index < tileArray.Length; index++)
        {
            Debug.Log($"Tile {index}: {tileArray[index]}");
        }
        

    }

    private void FixedUpdate() {


        
        //Add velocity to player
        rb.AddForce(moveDir.normalized * moveSpeed * Time.deltaTime);
    }



}
