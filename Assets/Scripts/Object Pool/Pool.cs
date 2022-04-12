using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class declares default values for each pool. Values can be modified in Inspector.
/// </summary>
[System.Serializable]
public class Pool
{
    // Default parameters in object pool. Visible in Inspector.

    [Tooltip("The tag of the pool")]
    [SerializeField] string tag;

    [Tooltip("The size of pool")]
    [SerializeField] int size;

    [Tooltip("Game object to spawn")]
    [SerializeField] GameObject prefab;

    [Tooltip("The position from which the object will spawn")]
    [SerializeField] Transform objectPosition;

    // Constructor. In case a programmer would like to declare all pools in code.

    public Pool(string tag, int size, GameObject prefab, Transform objectPosition)
    {
        this.tag = tag;
        this.size = size;
        this.prefab = prefab;
        this.objectPosition = objectPosition;
    }

    // Properties

    public string Tag { get { return tag; } }
    public int Size { get { return size; } }
    public GameObject Prefab { get { return prefab; } }
    public Transform ObjectPosition { get { return objectPosition; } }
}
