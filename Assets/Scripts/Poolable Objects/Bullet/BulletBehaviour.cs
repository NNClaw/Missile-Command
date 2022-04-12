using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script, which is attached to the Bullet prefab. Determines each bullet's behaviour. IPoolableObject interface is mandatory.
/// </summary>
public class BulletBehaviour : MonoBehaviour, IPoolableObject
{
    // Variables for visibility in Inspector

    [Tooltip("The speed of Bullet")]
    [Range(0f, 10000f)]
    [SerializeField] float bulletSpeed = 500f;

    [Tooltip("Reference for Particle System game object, which is used for trail effect")]
    [SerializeField] ParticleSystem bulletParticles;

    // Internal struct references

    Vector3 difference;
    Vector3 direction;
    float rotationZ;

    // Start

    void Start()
    {
        GetObjectComponents();
    }

    /// <summary>
    /// Used to cache all of the object's components for work
    /// </summary>
    void GetObjectComponents()
    {
        bulletParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Below 2 methods implemented from IPoolableObject. More info about those methods is in respected interface.

    public void OnObjectActivate() { }

    public void OnObjectActivate(RaycastHit hitInfo)
    {
        ResetRigidbodyValues();
        Calculations.PropellObject(hitInfo, difference, direction, bulletSpeed, gameObject.transform, rotationZ, gameObject);
        bulletParticles.Play();
    }


    /// <summary>
    /// Just makes bullet deactivate on impact with objects tagged Earth or Missile.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile") || collision.gameObject.CompareTag("Earth"))
        {
            gameObject.SetActive(false);
            ResetRigidbodyValues();
        }
    }

    // Below method implemented from IPoolableObject. More info about it is in respected interface.

    public void ResetRigidbodyValues()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
