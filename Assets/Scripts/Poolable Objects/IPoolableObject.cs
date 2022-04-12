using UnityEngine;

/// <summary>
/// Interface, which every poolable object should implement. Otherwise the activations won't work.
/// </summary>
public interface IPoolableObject
{
    /// <summary>
    /// Needed when activation of an object doesn't need anything for it being spawned.
    /// </summary>
    public void OnObjectActivate();

    /// <summary>
    /// Needed when activation of an object depends on RaycastHit value. For example, calculation of the distance between mouse point and Transform for bullet to travel.
    /// </summary>
    /// <param name="hitInfo"></param>
    public void OnObjectActivate(RaycastHit hitInfo);

    /// <summary>
    /// Resets Rigidbody values of poolable game object.
    /// </summary>
    public void ResetRigidbodyValues();
}
