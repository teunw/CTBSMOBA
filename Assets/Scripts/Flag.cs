using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Flag : MonoBehaviour, IFieldObject
{
    public Base homeBase;
    private int actionsDone;


    /// <summary>
    /// Returns the flag back to its original spawnpoint
    /// </summary>
    public void ReturnToSpawnPoint()
    {
        this.transform.position = homeBase.getSpawn();
        this.ActionDone();
    }

    public void ActionDone()
    {
        this.actionsDone++;
    }
}
