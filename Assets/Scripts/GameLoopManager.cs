using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class which puts all things, that are in GameLoop scene together and make the game work
/// </summary>
public class GameLoopManager : MonoBehaviour
{
    // Variables for visibility in Inpector and tweaking

    [Tooltip("A layermask to determine on which collider the bullets will get detected and shot.")]
    [SerializeField] LayerMask shootingLayerMask;

    [Tooltip("The delay of how frequent will the enemies spawn. (In seconds)")]
    [Range(0f, 160f)]
    [SerializeField] float enemySpawnDelay = 5f;

    [Tooltip("The minimum value of an axis range, which is used for random spawning. (Currently used in enemy spawner.)")]
    [Range(0f, -10000f)]
    [SerializeField] float minAxisSpawnRange = -19f;

    [Tooltip("The maximum value of an axis range, which is used for random spawning. (Currently used in enemy spawner.)")]
    [Range(0f, 10000f)]
    [SerializeField] float maxAxisSpawnRange = 18f;

    [Tooltip("This is what spawnTimeModifier gets deducted from each minute.")]
    [Range(0f, 2f)]
    [SerializeField] float spawnTimeCorrector = 0.08f;

    [Tooltip("Just to monitor how much game time has passed. I used it to see how spawnTimeModifier changes every minute.")]
    [SerializeField] float timer;

    [Tooltip("The value, which modifies spawn time of missiles each minute")]
    [Range(0f, 1f)]
    [SerializeField] float spawnTimeModifier = 1f;

    [Tooltip("The lowest value of spawnTimeModifier, so that missiles won't spawn like crazy over time.")]
    [Range(0.1f, 0.9f)]
    [SerializeField] float lowestSpawnTimeModifier = 0.4f;

    // Internal class references

    Camera mainCam;
    ObjectPooler objectPooler;
    GameLoopUI gameLoopUI;
    Transform missileSpawnPoint;

    // Internal struct references

    float nextSpawnTime = 0.5f;
    Vector3 randomXSpawnPoint;
    bool isGameTime;

    // Awake

    void Awake()
    {
        isGameTime = true;
    }

    // Start

    void Start()
    {
        mainCam = Camera.main;
        objectPooler = ObjectPooler.Instance;
        missileSpawnPoint = objectPooler.MissilePosition;
        gameLoopUI = FindObjectOfType<GameLoopUI>();

        randomXSpawnPoint = Calculations.GenerateSpawnPointByAxis("X", minAxisSpawnRange, maxAxisSpawnRange, missileSpawnPoint.position.x, missileSpawnPoint.position.y, missileSpawnPoint.position.z);

        StartCoroutine(SpawnEnemies());
        InvokeRepeating(nameof(ModifySpawnTime), 60, 60);
    }

    // Update

    void Update()
    {
        timer = Time.time;
        ProcessShooting();
    }

    /// <summary>
    /// This method translates mouse pointer position to Vector3 point in game world to determine where to start calculating distance and shoot the bullet.
    /// </summary>
    void Shoot()
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, shootingLayerMask))
            objectPooler.ActivateFromPool("Bullet", Quaternion.identity, hitInfo);
    }

    /// <summary>
    /// This method checks, whether the player can shoot or not. You can't shoot, while the game is paused or Game Over screen is shown.
    /// </summary>
    void ProcessShooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameLoopUI.GameOverScreen.activeInHierarchy && !gameLoopUI.PauseScreen.activeInHierarchy)
            Shoot();
    }

    /// <summary>
    /// This IEnumerator used for delayed spawn of each missile in object pool queue.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        while (isGameTime)
        {
            randomXSpawnPoint = Calculations.GenerateSpawnPointByAxis("X", minAxisSpawnRange, maxAxisSpawnRange, missileSpawnPoint.position.x, missileSpawnPoint.position.y, missileSpawnPoint.position.z);
            yield return new WaitForSeconds(nextSpawnTime);

            // This "if" determines, whether it can spawn the next object
            if (ShouldSpawn())
            {
                objectPooler.ActivateFromPool("Missile", Quaternion.identity, randomXSpawnPoint);
                nextSpawnTime = enemySpawnDelay * spawnTimeModifier;
            }
        }
    }

    /// <summary>
    /// Returns a result, whether the timer has exceeded the time for next object spawn.
    /// </summary>
    /// <returns></returns>
    bool ShouldSpawn()
    {
        return timer >= nextSpawnTime;
    }

    /// <summary>
    /// Modifies spawnTimeModifier each minute. If it's equal or less, than lowestSpawnTimeModifier, it will stay the same, until the game is restarted.
    /// </summary>
    void ModifySpawnTime()
    {
        if(spawnTimeModifier <= lowestSpawnTimeModifier)
            spawnTimeModifier = lowestSpawnTimeModifier;
        else
            spawnTimeModifier -= spawnTimeCorrector;
    }
}
