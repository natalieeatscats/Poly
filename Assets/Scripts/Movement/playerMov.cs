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
    private RaycastHit2D hit;
    public BoundsInt area;
    public Tilemap tilemap;
    public Grid grid;

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

        ////Cap direction vector to 1 to prevent diagonal movement from being faster than cardinal
        //if (moveDir.x + moveDir.y > 1)
        //{
        //    moveDir.Normalize();  
        //}
        
       // area.position = WorldToCell(this.transform.position);
       // area.max = new Vector3Int(1,1,6);
//
       // print(area);
//
       // TileBase[] tileArray = tilemap.GetTilesBlock(area);
       // //print(tileArray[0]);
       // for (int index = 0; index < tileArray.Length; index++)
       // {
       //     print(tileArray[index]);
       // }
    }

    private void FixedUpdate() {


        
        //Add velocity to player
        rb.AddForce(moveDir * moveSpeed * Time.deltaTime);
    }



}
