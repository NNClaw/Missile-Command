using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class only has universal methods for various calculations, which can be used anywhere.
/// </summary>
public class Calculations
{
    /// <summary>
    /// This method calculates the random point of given axis. Used in random spawning of Missiles.
    /// </summary>
    /// <param name="axis"> Axis name. Can be X, Y or Z. </param>
    /// <param name="minAxisValue"> Minimum value, from which randomness gets calculated. </param>
    /// <param name="maxAxisValue"> Maximum value, to which randomness gets calculated. </param>
    /// <param name="positionX"> Transform position X value. </param>
    /// <param name="positionY"> Transform position Y value. </param>
    /// <param name="positionZ"> Transform position Z value. </param>
    /// <returns></returns>
    public static Vector3 GenerateSpawnPointByAxis(string axis, float minAxisValue, float maxAxisValue, float positionX, float positionY, float positionZ)
    {
        float axisRandomSpawnPoint = Random.Range(minAxisValue, maxAxisValue);
        Vector3 randomSpawnPoint;
        string newAxisString = axis.ToLower();

        switch (newAxisString)
        {
            case "x":
                randomSpawnPoint = new Vector3(axisRandomSpawnPoint, positionY, positionZ);
                return randomSpawnPoint;
            case "y":
                randomSpawnPoint = new Vector3(positionX, axisRandomSpawnPoint, positionZ);
                return randomSpawnPoint;
            case "z":
                randomSpawnPoint = new Vector3(positionX, positionY, axisRandomSpawnPoint);
                return randomSpawnPoint;
            default:
                break;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// The method to calculate the velocity of rigidbody object with given direction. Direction gets calculated from the spawn point to mouse click point.
    /// </summary>
    /// <param name="hitInfo"> Raycast hit info value. </param>
    /// <param name="difference"> The distance between hit info and initial game object position. </param>
    /// <param name="direction"> The direction of the game object, which is calculated using difference and distance. </param>
    /// <param name="movementSpeed"> The speed of moving object. </param>
    /// <param name="transformOfObject"> The transform of the object. </param>
    /// <param name="rotationZ"> How the object is rotated when spawned. Calculated using atan2 function (Reference got from: https://en.wikipedia.org/wiki/Atan2). </param>
    /// <param name="gameObject"> What game object should be used for calculations. </param>
    public static void PropellObject(RaycastHit hitInfo, Vector3 difference, Vector3 direction, float movementSpeed, Transform transformOfObject, float rotationZ, GameObject gameObject)
    {
        difference = hitInfo.point - transformOfObject.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float distance = difference.magnitude;
        direction = difference / distance;
        direction.Normalize();

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        gameObject.GetComponent<Rigidbody>().velocity = movementSpeed * Time.fixedDeltaTime * direction;
    }
}
