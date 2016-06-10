#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WalkAction : Action
    {

        private readonly List<Vector2> _positions;
        private readonly Rigidbody2D _rigidbody2D;

        private int _currentStep;
        private Vector2 _currentStepStartPosition, _nextStepStartPosition;
        private float _stepRadius;
        private Transform transform;

        /// <summary>
        ///     Constructs walk action
        /// </summary>
        /// <param name="member">Member to apply this action to</param>
        /// <param name="positions">Positions to move to</param>
        public WalkAction(Member member, List<Vector2> positions) : base(member)
        {
            _positions = positions;
            _currentStep = 0;
            _rigidbody2D = Member.gameObject.GetComponent<Rigidbody2D>();
            _currentStepStartPosition = Vector2.zero;
            _nextStepStartPosition = Vector2.zero;
            transform = _rigidbody2D.gameObject.transform;
        }

        /// <summary>
        ///     Gets the current step of this walkaction
        /// </summary>
        public Vector2 CurrentStep
        {
            get { return _positions[_currentStep]; }
        }

        public int ListCount
        {
            get { return _positions.Count; }
        }

        /// <summary>
        ///     Moves the Rigidbody component and checks for next node
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            _rigidbody2D.velocity = transform.forward * Member.Speed * Time.deltaTime;
            return ShouldMoveToNextPoint();
        }

        public override bool isDone()
        {
            // Checks if all steps have been done
            return _currentStep == _positions.Count - 1;
        }

        public bool ShouldMoveToNextPoint()
        {
            // Calculate positions
            if (_currentStepStartPosition == Vector2.zero)
            {
                _currentStepStartPosition = CurrentStep;
                try
                {
                    // Moves to next step
                    _nextStepStartPosition = NextStep(true);
                    transform.LookAt(_positions[_currentStep]);
                }
                catch (IndexOutOfRangeException e)
                {
                    // When the last element has been selected, this exception will be thrown when last element has been reached
                    _rigidbody2D.velocity = Vector2.zero;
                    return true;
                }
                // Checks the radius for this step, to see if the member is outside the current step
                _stepRadius = Vector2.Distance(_currentStepStartPosition, _nextStepStartPosition);
            }
            /* 
        Calculate distance between positions (radius)
        Get current position
        Get distance between this position and step position
        If this distance is equal or larger than the radius, it should move to the next point 
        */
            Vector2 currentMemberPosition = Member.transform.position;
            float currentRadius = Vector2.Distance(_currentStepStartPosition, currentMemberPosition);
            // Checks if the radius has been reached, and goes to the next step is it did
            if (currentRadius > _stepRadius)
            {
                _currentStepStartPosition = Vector2.zero;
                _nextStepStartPosition = Vector2.zero;
                _stepRadius = -1.0f;
                if (_currentStep >= ListCount)
                {
                    // When and has been reached, stop the player from moving
                    _rigidbody2D.velocity = Vector2.zero;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Gets the next step
        /// </summary>
        /// <param name="plus">True if the method should go to the next step</param>
        /// <returns>Next step position</returns>
        public Vector2 NextStep(bool plus = true)
        {
            if (_currentStep + 1 >= ListCount)
            {
                throw new IndexOutOfRangeException();
            }

            return plus ? _positions[++_currentStep] : _positions[_currentStep];
        }
    }
}