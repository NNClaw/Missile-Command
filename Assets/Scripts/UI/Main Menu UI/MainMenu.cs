using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is attached to Main Menu game object and manages very simple main menu operations.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Internal class references

    SoundManager soundManager;

    // Internal struct references

    int currentBuildIndex;

    // Awake

    void Awake()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        soundManager = FindObjectOfType<SoundManager>();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Method to determine, whether the button should play or quit the game. Used for Play button and Quit button in main menu.
    /// </summary>
    /// <param name="gameState"> The name of game state. It is either Play or Quit. </param>
    public void PlayOrQuit(string gameState)
    {
        string newString = gameState.ToLower();

        switch (newString)
        {
            case "play":
                soundManager.PlaySound("Select SFX");
                SceneManager.LoadScene(currentBuildIndex + 1);
                break;

            case "quit":
                soundManager.PlaySound("Select SFX");
                Application.Quit();                         // Maybe put this method in coroutine to delay the quitting for the sfx to play.
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Method to play the select sound. Used for buttons mainly.
    /// </summary>
    public void PlaySelectSound()
    {
        soundManager.PlaySound("Select SFX");
    }
}
