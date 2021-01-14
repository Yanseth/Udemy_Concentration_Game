using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    bool isMine = false;
    bool isFlagged = false;
    bool isFlipped = false;
    int numMinesTouching = 0;

    [Header("Tile Sprites")]
    public List<Sprite> tileImages;
    public Sprite flagSprite;
    public Sprite mineSprite;
    public Sprite defaultSprite;

    public SpriteRenderer currentSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void FlagTile()
    {
        isFlagged = !isFlagged;
        if (isFlagged)
        {
            currentSprite.sprite = flagSprite;
        }
        else
        {
            currentSprite.sprite = defaultSprite;
        }
    }

    public void FlipTile()
    {
        if (isMine)
        {
            currentSprite.sprite = mineSprite;
        }
        else
        {
            currentSprite.sprite = tileImages[numMinesTouching];
        }
    }

    public void SetMine()
    {
        isMine = true;
    }

    public bool IsMine()
    {
        return isMine;
    }

    public void SetNumMinesTouching(int numMines)
    {
        numMinesTouching = numMines;
    }

}
