using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BumperScript : MonoBehaviour
    {
        /// <summary>
        /// Check if the bumper gets hit by another object.
        /// In this case, member. And call the member.WallHit() method
        /// it will take care of the rest.
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter2D(Collision2D other)
        {
            other.collider.GetComponent<Member>().WallHit();
        }
    }
}
