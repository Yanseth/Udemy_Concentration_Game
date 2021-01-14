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

    public void SetFlag()
    {
        if (!isFlipped)
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
    }

    public void FlipTile()
    {
        if (!isFlagged && !isFlipped)
        {
            isFlipped = true;
            if (isMine)
            {
                currentSprite.sprite = mineSprite;
            }
            else
            {
                currentSprite.sprite = tileImages[numMinesTouching];
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            SetFlag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            FlipTile();
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
