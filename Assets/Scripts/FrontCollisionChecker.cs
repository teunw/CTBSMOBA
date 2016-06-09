using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class FrontCollisionChecker : MonoBehaviour
    {


        void OnTriggerEnter2D(Collider2D other)
        {
            string otherTag = other.tag;
            if (otherTag.Equals("Arena"))
            {
                transform.parent.GetComponent<Member>().WallHit();
            }
            if (otherTag.Equals("Character"))
            {
                other.GetComponent<Member>().IsHit(transform.parent.transform.GetComponent<Rigidbody2D>().velocity);
            }
        }
    }
}
