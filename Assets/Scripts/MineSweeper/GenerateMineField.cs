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
    List<GameObject> shuffledTiles;

    [Header("Sprites")]
    public Sprite mineSprite;

    void Start()
    {
        colCount = levelsArray[difficultyLevel].cols;
        rowCount = levelsArray[difficultyLevel].rows;
        mineCount = levelsArray[difficultyLevel].mines;
        totalTiles = colCount * rowCount;
        SetUpBoard();

        // Resize the camera
        theCamera.orthographicSize = (float)rowCount / 2 + 1;
    }

    public void SetUpBoard()
    {
        PlaceTiles();
        ShuffleTiles();
        DetermineMines();
        SetMinesTouching();
        // FlipAllTiles();  // This is just for debuging.
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

    public void ShuffleTiles()
    {
        shuffledTiles = new List<GameObject>(tiles);
        // Implement Fisher-Yates Shuffle
        for (int i = shuffledTiles.Count - 1; i > 0; i--)
        {
            int randomPosition = Random.Range(0, i);
            GameObject tempTile = shuffledTiles[randomPosition];
            shuffledTiles[randomPosition] = shuffledTiles[i];
            shuffledTiles[i] = tempTile;
        }
    }

    public void DetermineMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            TileHandler handler = shuffledTiles[i].GetComponent<TileHandler>();
            handler.SetMine();
        }
    }

    public void SetMinesTouching()
    {
        for (int i = 0; i < tiles.Count; i += 1)
        {
            int mineCount = CalculateNeiboringMines(i);
            tiles[i].GetComponent<TileHandler>().SetNumMinesTouching(mineCount);
        }
    }

    void FlipAllTiles()
    {
        for (int i = 0; i < tiles.Count; i += 1)
        {
            tiles[i].GetComponent<TileHandler>().FlipTile();
        }
    }

    public int CalculateNeiboringMines(int tileIdx)
    {
        int minesTouchingCount = 0;
        bool checkLeft = true;
        bool checkRight = true;
        bool checkAbove = true;
        bool checkBelow = true;

        if (tileIdx % colCount == 0) { checkLeft = false; }
        if (tileIdx - colCount < 0) { checkAbove = false; }
        if (tileIdx % colCount == colCount - 1) { checkRight = false; }
        if (tileIdx + colCount >= totalTiles) { checkBelow = false; }
        // Check left
        if (checkLeft)
        {
            minesTouchingCount += CheckLeft(tileIdx, checkAbove, checkBelow);
        }
        if (checkRight)
        {
            minesTouchingCount += CheckRight(tileIdx, checkAbove, checkBelow);
        }
        minesTouchingCount += CheckAboveBelow(tileIdx, checkAbove, checkBelow);

        return minesTouchingCount;
    }

    public int CheckAboveBelow(int tileIdx, bool checkAbove, bool checkBelow)
    {
        int minesTouchingCount = 0;

        if (checkAbove)
        {
            if (tiles[tileIdx - colCount].GetComponent<TileHandler>().IsMine())
            {
                minesTouchingCount += 1;
            }
        }
        if (checkBelow)
        {
            if (tiles[tileIdx + colCount].GetComponent<TileHandler>().IsMine())
            {
                minesTouchingCount += 1;
            }
        }
        return minesTouchingCount;
    }

    public int CheckLeft(int tileIdx, bool checkAbove, bool checkBelow)
    {
        int minesTouchingCount = 0;
        if (tiles[tileIdx - 1].GetComponent<TileHandler>().IsMine())
        {
            minesTouchingCount += 1;
        }
        minesTouchingCount += CheckAboveBelow(tileIdx - 1, checkAbove, checkBelow);
        return minesTouchingCount;
    }

    public int CheckRight(int tileIdx, bool checkAbove, bool checkBelow)
    {
        int minesTouchingCount = 0;
        if (tiles[tileIdx + 1].GetComponent<TileHandler>().IsMine())
        {
            minesTouchingCount += 1;
        }
        minesTouchingCount += CheckAboveBelow(tileIdx + 1, checkAbove, checkBelow);
        return minesTouchingCount;
    }

}
