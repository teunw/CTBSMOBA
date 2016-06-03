#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Assets.Scripts
{
    public class MemberLine
    {
        /// <param name="member">Member for later replacement && reference</param>
        public MemberLine(Member member)
        {
            Member = member;
            LineRenderers = new List<LineRenderer>();
            Positions = new List<Vector3>();
        }

        /// <summary>
        ///     Member holder
        /// </summary>
        public Member Member { get; private set; }

        /// <summary>
        ///     LineRenderers stored for later reference
        /// </summary>
        public List<LineRenderer> LineRenderers { get; private set; }

        /// <summary>
        ///     Positions in the line that are saved
        /// </summary>
        public List<Vector3> Positions { get; private set; }

        /// <summary>
        ///     Returns last drawn position of this object
        /// </summary>
        public Vector3 LastPosition
        {
            get { return Positions.Count > 0 ? Positions[Positions.Count - 1] : Member.transform.position; }
        }

        /// <summary>
        ///     Resets this line
        /// </summary>
        public void Reset()
        {
            LineRenderers.Clear();
            Positions.Clear();
            ;
        }

        /// <summary>
        ///     Resets this line, with another starting position
        /// </summary>
        /// <param name="startPos">New starting position</param>
        /// <returns>This object</returns>
        public MemberLine Reset(Vector3 startPos)
        {
            Reset();
            Positions.Add(startPos);
            return this;
        }
    }
}