﻿using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour, IFieldObject {
    /// <summary>
    /// Returns the flag back to its original spawnpoint
    /// </summary>
    /// <param name="flagSpawnPoint">Vector2 location of the flags original spawnpoint</param>
    public void ReturnToSpawnPoint(Vector2 flagSpawnPoint)
    {
        this.transform.position = new Vector3(flagSpawnPoint.x, flagSpawnPoint.y, 0);
        this.IsActionDone();
    }

    public bool IsActionDone()
    {
        return true;
    }
}
