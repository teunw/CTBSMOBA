using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WalkAction : MonoBehaviour, Action
    {
        public List<Vector2> Positions;

        private bool shouldWalk;
        private Rigidbody2D _rigidbody2D;
        private Vector2 currentStepPosition, nextStepPosition;
        private float stepRadius;
        private int currentStep;

        private int MemberSpeed;

        public string Name
        {
            get { return "Walk action"; }
        }

        void Start()
        {
            this._rigidbody2D = GetComponent<Rigidbody2D>();
            this.MemberSpeed = GetComponent<Member>().Speed;
        }

        void Update()
        {
            if (!shouldWalk) return;
            _rigidbody2D.velocity = transform.forward*MemberSpeed*Time.deltaTime;
            CheckNextPosition();
        }

        void OnMemberWalk()
        {
            shouldWalk = true;
        }

        public void CheckNextPosition()
        {
            // Calculate positions
            if (currentStepPosition == Vector2.zero)
            {
                currentStepPosition = Positions[currentStep];

                try
                {
                    // Moves to next step
                    nextStepPosition = NextStep();
                    transform.LookAt(Positions[currentStep]);
                }
                catch (IndexOutOfRangeException e)
                {
                    EndWalkAction();
                }

                // Checks the radius for this step, to see if the member is outside the current step
                stepRadius = Vector2.Distance(currentStepPosition, nextStepPosition);
            }
            /* 
            Calculate distance between positions (radius)
            Get current position
            Get distance between this position and step position
            If this distance is equal or larger than the radius, it should move to the next point 
            */
            Vector2 currentMemberPosition = transform.position;
            float currentRadius = Vector2.Distance(currentStepPosition, currentMemberPosition);
            // Checks if the radius has been reached, and goes to the next step is it did
            if (currentRadius > stepRadius)
            {
                currentStepPosition = Vector2.zero;
                nextStepPosition = Vector2.zero;
                stepRadius = -1.0f;
                if (currentStep >= Positions.Count)
                {
                    // When and has been reached, stop the player from moving
                    _rigidbody2D.velocity = Vector2.zero;
                    EndWalkAction();
                }
            }
        }

        /// <summary>
        ///     Gets the next step
        /// </summary>
        /// <param name="plus">True if the method should go to the next step</param>
        /// <returns>Next step position</returns>
        public Vector2 NextStep(bool plus = true)
        {
            if (currentStep + 1 >= Positions.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return plus ? Positions[++currentStep] : Positions[currentStep];
        }

        public void EndWalkAction()
        {
            _rigidbody2D.velocity = new Vector2(0,0);
            shouldWalk = false;
            SendMessage(ActionConstants.OnMemberWalkDoneString);
            Destroy(this);
        }
    }
}