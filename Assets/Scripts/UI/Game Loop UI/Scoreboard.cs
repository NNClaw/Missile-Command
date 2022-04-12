using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is attached to the Scoreboard game object and manages the score in the game.
/// </summary>
public class Scoreboard : MonoBehaviour
{
    // Internal class references

    TMP_Text scoreboardText;

    // Interna struct references

    int score = 0;
    
    // Properties

    public int Score { get { return score; } }

    // Start

    void Start()
    {
        scoreboardText = GetComponent<TMP_Text>();
        scoreboardText.text = "Score: " + score.ToString();
    }
    
    /// <summary>
    /// A very simple method, which updates the score and shows it on the UI.
    /// </summary>
    /// <param name="toUpdate"> What amount of score should be added to the scoreboard. </param>
    public void UpdateScore(int toUpdate)
    {
        score += toUpdate;
        scoreboardText.text = "Score: " + score.ToString();
    }
}
