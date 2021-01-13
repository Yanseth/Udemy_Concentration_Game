using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTiles : MonoBehaviour
{
    public List<GameObject> tiles;
    public int colCount;
    public int tileCount;


    // Start is called before the first frame update
    void Start()
    {
        ShuffleTiles();
        PlaceTiles();
        tileCount = tiles.Count / 2;  // Will always be a multiple of 2
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShuffleTiles()
    {
        // Implement Fisher-Yates Shuffle
        for (int i = tiles.Count - 1; i > 0; i--)
        {
            int randomPosition = Random.Range(0, i);
            GameObject tempTile = tiles[randomPosition];
            tiles[randomPosition] = tiles[i];
            tiles[i] = tempTile;
        }
    }


    public void PlaceTiles()
    {
        // Okay, a big thing to keep in mind.  The Vetor2 that is the position of the tile, that is the center of the
        // tile, not the upper left corner.  This affects where we want our tiles positioned.  We may need to shift by
        // half a position
        float initX = CalculateInitX();
        float initY = CalculateInitY();
        // Debug.Log("InitX: " + initX);
        // Debug.Log("InitY: " + initY);

        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2 tileLocation = new Vector2(initX + i % colCount, initY - i / colCount);
            Instantiate(tiles[i], tileLocation, Quaternion.identity);
        }
    }

    public int CalculateRows()
    {
        int rows = tiles.Count / colCount;
        if (tiles.Count % colCount > 0)
        {
            rows += 1; // Need an exta row for the remaining tiles
        }
        return rows;
    }

    public float CalculateInitX()
    {
        // This is caculating the center of the tile, push 1/2 tile to the right
        // Ahh!  the pushing half a tile right, puts the left side of the tile at x = 0, now our calculations work
        return -(float)colCount / 2f + 0.5f;
    }

    public float CalculateInitY()
    {
        // This is calculating the center of the tile, so push half a tile down
        int rowCount = CalculateRows();
        float initY = (float)rowCount / 2f - 0.5f;
        return initY;
    }
}
