using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Flag : MonoBehaviour, IFieldObject
{
    public Base homeBase;
    private int actionsDone;
    private Vector3 lastPosition;
    private bool notMoving;


    /// <summary>
    /// Returns the flag back to its original spawnpoint
    /// </summary>
    public void ReturnToSpawnPoint()
    {
        this.transform.position = homeBase.getSpawn();
        this.ActionDone();
    }

    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = transform.position;
        if (startPos.x == finalPos.x && startPos.y == finalPos.y
            && startPos.z == finalPos.z)
            notMoving = true;
    }

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
