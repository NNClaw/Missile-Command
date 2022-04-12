using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is attached to every Missile game object and declares it's behaviour. Also implements IPoolableObject interface.
/// </summary>
public class MissileBehaviour : MonoBehaviour, IPoolableObject
{
    // Variables for tweaking in Inspector

    [Tooltip("The speed of falling of Missile")]
    [Range(0f, -1000f)]
    [SerializeField] float fallingSpeed = -100;

    [Tooltip("The score, which player gets for destroying Missile.")]
    [Range(0, 2000)]
    [SerializeField] int scoreForKill = 200;

    // Internal class references

    Rigidbody rb;
    Scoreboard scoreboard;
    Lives lives;
    GameLoopUI gameLoopUI;

    // Awake

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        scoreboard = FindObjectOfType<Scoreboard>();
        lives = FindObjectOfType<Lives>();
        gameLoopUI = FindObjectOfType<GameLoopUI>();
    }

    // Below 2 methods implemented from IPoolableObject. More info about those methods is in respected interface.

    public void OnObjectActivate() 
    {
        ObjectMovement();
    }

    public void OnObjectActivate(RaycastHit hitInfo) { }

    /// <summary>
    /// This method checks for different behaviours for game objects with tags Earth and Bullet.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            // When Missile collides with earth, game object gets deactivated and number of lives gets updated.

            case "Earth":
                gameObject.SetActive(false);
                lives.UpdateNumberOfLives();

                // If there are no more lives...
                if(lives.NumberOfLives == 0)
                {
                    // ...show Game Over screen.
                    gameLoopUI.ShowGameOverScreen();
                }

                ResetRigidbodyValues();
                break;

            // When Missile collides with bullet, game object gets deactivated and score gets updated
            case "Bullet":
                gameObject.SetActive(false);
                scoreboard.UpdateScore(scoreForKill);

                ResetRigidbodyValues();
                break;

            default:
                break;
        }
    }

    // Below method implemented from IPoolableObject. More info about it is in respected interface.

    public void ResetRigidbodyValues()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Missile movement behaviour.
    /// </summary>
    void ObjectMovement()
    {
        rb.velocity = new Vector3(0, fallingSpeed * Time.fixedDeltaTime, 0);
    }
}
