using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface, which implements to every class, which is defined as Object Pool. String "tag" and Quaternion "rotation" are mandatory.
/// </summary>
public interface IObjectPool
{
    /// <summary>
    /// Activate object using position and mouse click info.
    /// </summary>
    /// <param name="tag"> The tag of game object. </param>
    /// <param name="rotation"> The initial rotation of game object. </param>
    /// <param name="hitInfo"> Raycast hit info value of mouse point. </param>
    /// <param name="position"> The initial position of game object. </param>
    /// <returns></returns>
    public GameObject ActivateFromPool(string tag, Quaternion rotation, RaycastHit hitInfo, Vector3 position);

    /// <summary>
    /// Activate object using only position
    /// </summary>
    /// <param name="tag"> The tag of game object. </param>
    /// <param name="rotation"> The initial rotation of game object. </param>
    /// <param name="position"> The initial position of game object. </param>
    /// <returns></returns>
    public GameObject ActivateFromPool(string tag, Quaternion rotation, Vector3 position);

    /// <summary>
    /// Activate object using only mandatory data
    /// </summary>
    /// <param name="tag"> The tag of game object. </param>
    /// <param name="rotation"> The initial rotation of game object. </param>
    /// <returns></returns>
    public GameObject ActivateFromPool(string tag, Quaternion rotation);

    /// <summary>
    /// Activate object using only mouse click info
    /// </summary>
    /// <param name="tag"> The tag of game object. </param>
    /// <param name="rotation"> The initial rotation of game object. </param>
    /// <param name="hitInfo"> Raycast hit info value of mouse point. </param>
    /// <returns></returns>
    public GameObject ActivateFromPool(string tag, Quaternion rotation, RaycastHit hitInfo);
}
