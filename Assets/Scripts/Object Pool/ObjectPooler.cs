using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an implementation of ObjectPool, which is made of list of pools, which have a queue of game objects. IObjectPool interface is mandatory.
/// </summary>
public class ObjectPooler : MonoBehaviour, IObjectPool
{
    // Variables for visibility in Inspector

    [SerializeField] Transform missilePosition;
    [SerializeField] List<Pool> pools;

    // Internal class references

    Dictionary<string, Queue<GameObject>> poolDictionary;

    // Properties

    public Transform MissilePosition { get { return missilePosition; } }

    // Singleton

    #region Singleton

    public static ObjectPooler Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion

    // Start

    void Start()
    {
        PrepopulatePool();
    }

    /// <summary>
    /// Prepopulates List of pools with set prefab and initial transform referenced in inspector 
    /// </summary>
    void PrepopulatePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab, pool.ObjectPosition);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    // The following methods are all the same, but there are differences in what data should be passed. If you want more info on these methods, look for explanation in IObjectPool interface 

    public GameObject ActivateFromPool(string tag, Quaternion rotation, RaycastHit hitInfo)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToActivate = poolDictionary[tag].Dequeue();

        objectToActivate.SetActive(true);
        objectToActivate.transform.rotation = rotation;

        foreach (Pool pool in pools)
        {
            objectToActivate.transform.position = pool.ObjectPosition.position;
        }

        IPoolableObject poolableObject = objectToActivate.GetComponent<IPoolableObject>();

        if (poolableObject != null)
            poolableObject.OnObjectActivate(hitInfo);
        else
            Debug.LogWarning("No object has been found");

        poolDictionary[tag].Enqueue(objectToActivate);

        return objectToActivate;
    }

    public GameObject ActivateFromPool(string tag, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToActivate = poolDictionary[tag].Dequeue();

        objectToActivate.SetActive(true);
        objectToActivate.transform.rotation = rotation;

        foreach (Pool pool in pools)
        {
            objectToActivate.transform.position = pool.ObjectPosition.position;
        }

        IPoolableObject poolableObject = objectToActivate.GetComponent<IPoolableObject>();

        if (poolableObject != null)
            poolableObject.OnObjectActivate();
        else
            Debug.LogWarning("No object has been found");

        poolDictionary[tag].Enqueue(objectToActivate);

        return objectToActivate;
    }

    public GameObject ActivateFromPool(string tag, Quaternion rotation, RaycastHit hitInfo, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToActivate = poolDictionary[tag].Dequeue();

        objectToActivate.SetActive(true);
        objectToActivate.transform.rotation = rotation;
        objectToActivate.transform.position = position;

        IPoolableObject poolableObject = objectToActivate.GetComponent<IPoolableObject>();

        if (poolableObject != null)
            poolableObject.OnObjectActivate(hitInfo);
        else
            Debug.LogWarning("No object has been found");

        poolDictionary[tag].Enqueue(objectToActivate);

        return objectToActivate;
    }

    public GameObject ActivateFromPool(string tag, Quaternion rotation, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToActivate = poolDictionary[tag].Dequeue();

        objectToActivate.SetActive(true);
        objectToActivate.transform.rotation = rotation;
        objectToActivate.transform.position = position;

        IPoolableObject poolableObject = objectToActivate.GetComponent<IPoolableObject>();

        if (poolableObject != null)
            poolableObject.OnObjectActivate();
        else
            Debug.LogWarning("No object has been found");

        poolDictionary[tag].Enqueue(objectToActivate);

        return objectToActivate;
    }
}
