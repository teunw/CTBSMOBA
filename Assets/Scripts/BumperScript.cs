using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BumperScript : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other);
            Debug.Log(other.collider.name);
            Debug.Log(other.collider.gameObject.name);
            other.collider.GetComponent<Member>().WallHit();
        }
    }
}
