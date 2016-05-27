using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class MemberLine
    {
        /// <summary>
        /// Member holder
        /// </summary>
        public Member Member { get; private set; }
        /// <summary>
        /// LineRenderers stored for later reference
        /// </summary>
        public List<LineRenderer> LineRenderers { get; private set; }
        /// <summary>
        /// Positions in the line that are saved
        /// </summary>
        public List<Vector3> Positions { get; private set; }

        /// <param name="member">Member for later replacement && reference</param>
        public MemberLine(Member member)
        {
            this.Member = member;
            this.LineRenderers = new List<LineRenderer>();
            this.Positions = new List<Vector3>();
        }

        /// <summary>
        /// Resets this line
        /// </summary>
        public void Reset()
        {
            this.LineRenderers.Clear();
            this.Positions.Clear();;
        }

        /// <summary>
        /// Resets this line, with another starting position
        /// </summary>
        /// <param name="startPos">New starting position</param>
        /// <returns>This object</returns>
        public MemberLine Reset(Vector3 startPos)
        {
            Reset();
            this.Positions.Add(startPos);
            return this;
        }

        /// <summary>
        /// Returns last drawn position of this object
        /// </summary>
        public Vector3 LastPosition
        {
            get { return (Positions.Count > 0) ? Positions[Positions.Count - 1] : Member.transform.position; }
        }


    }
}
