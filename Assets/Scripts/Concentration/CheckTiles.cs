using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTiles : MonoBehaviour
{
    public List<Transform> pickedTiles;

    public bool disableTiles = false;
    public float waitTime;
    public int totalMatches = 0;

    public void TileFlippedOver(Transform tileFlipped)
    {
        pickedTiles.Add(tileFlipped);
        if (pickedTiles.Count == 2)
        {
            disableTiles = true;
            CheckIfMatch();
        }
    }

    public void CheckIfMatch()
    {
        if (pickedTiles[0].name == pickedTiles[1].name)
        {
            StartCoroutine(RemoveMatchingTiles());
        }
        else
        {
            StartCoroutine(FlipTiles());
        }
    }

    public IEnumerator RemoveMatchingTiles()
    {
        totalMatches += 1;
        yield return new WaitForSeconds(waitTime);
        foreach (Transform tile in pickedTiles)
        {
            Destroy(tile.gameObject);
        }
        pickedTiles.Clear();
        disableTiles = false;
    }

    public IEnumerator FlipTiles()
    {
        yield return new WaitForSeconds(waitTime);
        foreach (Transform tile in pickedTiles)
        {
            tile.GetComponent<FlipTile>().FlipTileDown();
        }
        pickedTiles.Clear();
        disableTiles = false;
    }

}
