using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class FrontCollisionChecker : MonoBehaviour
    {
        /// <summary>
        /// The member of this collisionChecker.
        /// </summary>
        public Member member;

        /// <summary>
        /// Checks with which object this collider collides.
        /// If it's the arena then it will call the wallhit of the member.
        /// If it's another character it will or get bumped or bump the other member,
        /// depending on the movement of the other member.
        /// </summary>
        /// <param name="other">The object which collides with this collider</param>
        void OnTriggerEnter2D(Collider2D other)
        {
            string otherTag = other.tag;
            if (otherTag.Equals("Arena"))
            {
                transform.parent.GetComponent<Member>().WallHit();
            }
            if (otherTag.Equals("Character"))
            {
                Member otherMember = other.GetComponent<Member>();

                if (!this.member.ActionDone())
                {
                    otherMember.IsHit(transform.parent.transform.GetComponent<Rigidbody2D>().velocity);
                }

                if (!this.member.ActionDone() && !otherMember.ActionDone())
                {
                    member.IsHit(otherMember.transform.GetComponent<Rigidbody2D>().velocity);
                }
            }
        }
    }
}
