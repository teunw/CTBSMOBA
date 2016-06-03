#region

using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

#endregion

[RequireComponent(typeof(Rigidbody2D))]
public class WalkAction : Action
{
    private readonly List<Vector2> _positions;
    private readonly Rigidbody2D _rigidbody2D;

    private int _currentStep;
    private Vector2 _currentStepStartPosition, _nextStepStartPosition;
    private float _stepRadius;

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
        _rigidbody2D.velocity = _positions[_currentStep]*Member.Speed;
        return ShouldMoveToNextPoint();
    }

    public bool ShouldMoveToNextPoint()
    {
        // Calculate positions
        if (_currentStepStartPosition == Vector2.zero)
        {
            _currentStepStartPosition = Member.transform.position;
            try
            {
                _nextStepStartPosition = (Vector2) Member.transform.position + NextStep(true);
            }
            catch (IndexOutOfRangeException e)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return true;
            }
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
        if (currentRadius > _stepRadius)
        {
            _currentStepStartPosition = Vector2.zero;
            _nextStepStartPosition = Vector2.zero;
            _stepRadius = -1.0f;
            if (_currentStep >= ListCount)
            {
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