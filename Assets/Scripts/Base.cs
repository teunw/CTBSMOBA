using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Base : MonoBehaviour {
    
    public Team team;
    
    private Rect rectangle;
    private Vector2 flagSpawnPoint;
    private Flag EnemyFlag;

    private void Start()
    {
        flagSpawnPoint = this.transform.position;
    }

    private void Update()
    {
        if (EnemyFlag == null) return;
        if (EnemyFlag.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            team.RaiseScore();
            EnemyFlag.ReturnToSpawnPoint();
            EnemyFlag = null;
        }
    }


    /// <summary>
    /// get the Spawnpoint
    /// </summary>
    /// <returns> Vector2: The spawnPoint</returns>
    public Vector2 getSpawn()
    {
        return flagSpawnPoint;
    }

    /// <summary>
    /// Called by unity when collision happens. If the colliding object
    /// </summary>
    /// <param name="flag">The collider from the gameobject that makes collision</param>
    void OnTriggerEnter2D(Collider2D flag)
    {
            if (flag.tag == "Flag")
            {
                Flag Foundflag = flag.GetComponent<Flag>();

                if (checkIfEnemyFlag(Foundflag))
                {
                    EnemyFlag = Foundflag;
                }
            }      
    }

    void OnTriggerExit2D(Collider2D flag)
    {
        if (flag.tag == "Flag")
        {
            if (checkIfEnemyFlag(flag.GetComponent<Flag>()))
            {
                EnemyFlag = null;
            }      
        }
    }


    /// <summary>
    /// Checks if the given flag is a enemy flag by checking if the flag does not belong to the team this base is assosiated with.
    /// </summary>
    /// <param name="movementpoints">The pattern to follow</param>
    /// <returns> boolean. 
    /// True: if the given flag is not the same as the flag of the team to which this base belongs.
    /// False: if the given flag is the same as the flag of the team to which this base belongs. No enemy flag
    /// </returns>
    private bool checkIfEnemyFlag(Flag flag)
    {
        if (team.flag.Equals(flag))
        {
            return false;       
        }
        return true;
    }
   
}
