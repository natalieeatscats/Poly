using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileHeightCheck : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject player;
    private BoundsInt area;
    public GridLayout grid;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        area.position = grid.WorldToCell(position);
        area.min = new Vector3Int(area.position.x, area.position.y, area.position.z);
        area.max = new Vector3Int(area.position.x + 1, area.position.y + 1, area.position.z + 4);
        print(area);    

        TileBase[] tileArray = tilemap.GetTilesBlock(area);
        //print(tileArray[0]);
        for (int index = 0; index < tileArray.Length; index++)
        {
           print(tileArray[index]);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
