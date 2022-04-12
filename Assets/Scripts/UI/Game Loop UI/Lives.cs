using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is attached to Lives game object and manages the amount of lives the player has and it's methods.
/// </summary>
public class Lives : MonoBehaviour
{
    // Internal class references

    TMP_Text livesToShow;

    // Internal struct references

    int numberOfLives = 3;

    public int NumberOfLives { get { return numberOfLives; } }

    // Start is called before the first frame update
    void Start()
    {
        livesToShow = GetComponent<TMP_Text>();
        livesToShow.text = "Lives: " + numberOfLives.ToString();
    }

    public void UpdateNumberOfLives()
    {
        numberOfLives--;
        if(numberOfLives <= 0)
        {
            numberOfLives = 0;
        }
        livesToShow.text = "Lives: " + numberOfLives.ToString();
    }

}
