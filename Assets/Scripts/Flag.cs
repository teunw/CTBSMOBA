using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Flag : MonoBehaviour, IFieldObject
{
    /// <summary>
    /// The homebase of the flag
    /// </summary>
    public Base homeBase;

    /// <summary>
    /// A boolean which indicates if this
    /// flag is moving or not.
    /// </summary>
    private bool notMoving;


    /// <summary>
    /// Returns the flag back to its original spawnpoint
    /// </summary>
    public void ReturnToSpawnPoint()
    {
        this.transform.position = homeBase.getSpawn();
        this.ActionDone();
    }

    /// <summary>
    /// Method which checks if the flag is moving.
    /// It notes it's position, waits for a certain time
    /// and then checks it's position again. If the position
    /// is the same then the boolean notMoving is set to true.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = transform.position;
        if (startPos.x == finalPos.x && startPos.y == finalPos.y
            && startPos.z == finalPos.z)
            notMoving = true;
    }

    /// <summary>
    /// Checks if this flag is standing still
    /// by looking at the boolean notMoving which 
    /// gets manipulated by CheckMoving method.
    /// </summary>
    /// <returns>
    /// Returns true if the flag is standing still. 
    /// False if not.
    /// </returns>
    public bool ActionDone()
    {
        StartCoroutine(CheckMoving());

        if (notMoving == true)
        {
            notMoving = false;
            return true;
        }

        else
        {
            return false;
        }
    }
}
