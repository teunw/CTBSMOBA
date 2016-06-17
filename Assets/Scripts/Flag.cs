using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts;

public class Flag : IFieldObject
{
    /// <summary>
    /// The homebase of the flag
    /// </summary>
    public Base homeBase;

    /// <summary>
    /// Returns the flag back to its original spawnpoint
    /// </summary>
    public void ReturnToSpawnPoint()
    {
        this.transform.position = homeBase.getSpawn();
        this.ActionDone();
        ResetPoints();
    }
}
