using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// The script, which is attached to GameLoopUI game object. Manages all of the UI within the game loop.
/// </summary>
public class GameLoopUI : MonoBehaviour
{
    // Variables for visibility, tweaking and referencing in the Inspector

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] TMP_Text scoreForGameOverText;

    // Internal class references

    Scoreboard scoreboard;
    SoundManager soundManager;

    // Internal struct references

    int currentSceneIndex;
    public static bool gameIsPaused = false;

    // Properties

    public GameObject GameOverScreen { get { return gameOverScreen; } }
    public GameObject PauseScreen { get { return pauseScreen; } }

    // Start

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        scoreboard = FindObjectOfType<Scoreboard>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update

    void Update()
    {
        ProcessPause();    
    }

    /// <summary>
    /// Shows Game Over screen.
    /// </summary>
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        scoreForGameOverText.text = "Your Score is: " + scoreboard.Score.ToString();
    }

    /// <summary>
    /// Processes the pause menu based on user input. Pause menu is currently only accessible by pressing ESC button.
    /// </summary>
    void ProcessPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverScreen.activeInHierarchy)
        {
            if (gameIsPaused)
                PauseOrResume("Resume");
            else
                PauseOrResume("Pause");
        }
    }

    /// <summary>
    /// The method, which manages the pause state, whether to Pause or Resume.
    /// </summary>
    /// <param name="gameState"> The name of the pause state. Can be either Pause or Resume. </param>
    public void PauseOrResume(string gameState)
    {
        string newString = gameState.ToLower();

        switch (newString)
        {
            case "pause":
                soundManager.PlaySound("Select SFX");
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
                break;

            case "resume":
                soundManager.PlaySound("Select SFX");
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
                gameIsPaused = false;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// The method, which manages the game over state, whether to Restart or Get back to main menu.
    /// </summary>
    /// <param name="gameState"> The name of game over state. Can be either Restart or Menu. </param>
    public void RestartLevelOrGetBackToMenu(string gameState)
    {
        string newString = gameState.ToLower();

        switch (newString)
        {
            case "restart":
                soundManager.PlaySound("Select SFX");
                SceneManager.LoadScene(currentSceneIndex);
                break;

            case "menu":
                soundManager.PlaySound("Select SFX");
                SceneManager.LoadScene(currentSceneIndex - 1);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Method to play select sound. Used primarily for buttons.
    /// </summary>
    public void PlaySelectSound()
    {
        soundManager.PlaySound("Select SFX");
    }
}
