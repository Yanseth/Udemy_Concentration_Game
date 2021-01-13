using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GenerateMineField : MonoBehaviour
{
    public Camera theCamera;
    [Header("Tile Prefabs")]
    public GameObject mineSweeperTilePrefab;

    [System.Serializable]
    public class LevelsAttributes
    {
        public int cols;
        public int rows;
        public int mines;
    }

    [Header("Levels")]
    public LevelsAttributes[] levelsArray;

    [Header("Difficulty")]
    public int difficultyLevel;

    int colCount;
    int rowCount;
    int mineCount;
    int totalTiles;
    List<GameObject> tiles = new List<GameObject>();

    void Start()
    {
        colCount = levelsArray[difficultyLevel].cols;
        rowCount = levelsArray[difficultyLevel].rows;
        mineCount = levelsArray[difficultyLevel].mines;
        totalTiles = colCount * rowCount;
        PlaceTiles();

        // Resize the camera
        theCamera.orthographicSize = (float)rowCount / 2 + 1;
    }

    public void PlaceTiles()
    {
        // Okay, a big thing to keep in mind.  The Vetor2 that is the position of the tile, that is the center of the
        // tile, not the upper left corner.  This affects where we want our tiles positioned.  We may need to shift by
        // half a position
        float initX = CalculateInitX();
        float initY = CalculateInitY();

        for (int i = 0; i < totalTiles; i++)
        {
            Vector2 tileLocation = new Vector2(initX + i % colCount, initY - i / colCount);
            tiles.Add(Instantiate(mineSweeperTilePrefab, tileLocation, Quaternion.identity, this.transform));
        }
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
        float initY = (float)rowCount / 2f - 0.5f;
        return initY;
    }
}
