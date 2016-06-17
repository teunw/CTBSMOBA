using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Base : MonoBehaviour {

    /// <summary>
    /// The team where the base belongs to.
    /// </summary>
    public Team team;
    
    /// <summary>
    /// The spawnpoint of the flag in the base.
    /// </summary>
    private Vector2 flagSpawnPoint;

    /// <summary>
    /// The enemy flag, this is needed 
    /// to check if it's in your base.
    /// </summary>
    private Flag EnemyFlag;

    /// <summary>
    /// The collider of the base, 
    /// used for collission checks with the fieldobjects
    /// </summary>
    private Collider2D basecollider;

    /// <summary>
    /// Set the spawnpoint at the start
    /// of the game.
    /// </summary>
    private void Start()
    {
        basecollider = this.GetComponent<Collider2D>();
        flagSpawnPoint = this.transform.position;
    }

    /// <summary>
    /// The update method which checks if the other flag is in this base.
    /// If it is, raise the score of the team.
    /// </summary>
    private void Update()
    {
        if (EnemyFlag == null) return;
        if (basecollider.OverlapPoint(EnemyFlag.GetComponent<Renderer>().bounds.center))
        {
            if (EnemyFlag.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                team.RaiseScore();
                EnemyFlag.ReturnToSpawnPoint();
                EnemyFlag = null;
            }
        }
    }


    /// <summary>
    /// Get the spawnpoint of the flag.
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

    /// <summary>
    /// Checks if the flag leaves
    /// the base.
    /// </summary>
    /// <param name="flag"></param>
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
