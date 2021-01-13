using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MatchCounter : MonoBehaviour
{

    public CreateTiles createScript;
    public CheckTiles checkTilesScript;
    public Text matchesRemainingText;
    public int matchesRemaining;
    public CanvasGroup replayButton;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMatcheRemainingText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMatcheRemainingText();
        if (matchesRemaining == 0)
        {
            GameOver();
        }
    }

    public int GetMatchesRemaining()
    {
        return createScript.tiles.Count / 2 - checkTilesScript.totalMatches;
    }

    public void UpdateMatcheRemainingText()
    {
        matchesRemaining = GetMatchesRemaining();
        matchesRemainingText.text = "Matches Remaining: " + matchesRemaining;
    }

    public void GameOver()
    {
        replayButton.alpha = 1;
        replayButton.blocksRaycasts = true;
        replayButton.interactable = true;
    }
}
