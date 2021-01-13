using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTile : MonoBehaviour
{

    public bool flipUp = false;
    public bool flipDown = false;
    Vector3 downAngle;
    Vector3 flippedAngle;
    float timeToRotate = 0.75f;
    float startTime;
    public CheckTiles checkTiles;

    // Start is called before the first frame update
    void Start()
    {
        downAngle = transform.eulerAngles;
        flippedAngle = downAngle + 180f * Vector3.up; //vector3.up is the vector 0, 1, 0
        checkTiles = FindObjectOfType<CheckTiles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flipUp)  //flipup is a boolean
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, flippedAngle, (Time.time - startTime) / timeToRotate);
            if (transform.eulerAngles == flippedAngle)
            {
                flipUp = false;
            }
        }
        if (flipDown)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, downAngle, (Time.time - startTime) / timeToRotate);
            if (transform.eulerAngles == downAngle)
            {
                flipDown = false;
            }
        }
    }

    private void OnMouseDown()
    {
        // Debug.Log("Mouse Down");
    }

    private void OnMouseUp()
    {
        // Debug.Log("Mouse Up");
        // transform.Rotate(0, 180, 0);
        startTime = Time.time;
        if (transform.eulerAngles == downAngle)
        {
            if (checkTiles.disableTiles == false)
            {
                flipUp = true;
                checkTiles.TileFlippedOver(transform);
            }
        }
    }

    public void FlipTileDown()
    {
        startTime = Time.time;
        flipDown = true;
    }
}
