using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkAction : Action
{
    private Rigidbody2D rigidbody2D;
    private readonly List<Vector2> Positions;

    private int currentStep;
    private Vector2 CurrentStepStartPosition, NextStepStartPosition;
    private float StepRadius;

    /**
	 * 
	 * @param Positions
	 */

    public WalkAction(Member member, List<Vector2> positions) : base(member)
    {
        this.Positions = positions;
        this.currentStep = 0;
        this.rigidbody2D = Member.gameObject.GetComponent<Rigidbody2D>();
        this.CurrentStepStartPosition = Vector2.zero;
        this.NextStepStartPosition = Vector2.zero;
    }

    public override bool Update()
    {
        rigidbody2D.velocity = Positions[currentStep] * Member.Speed;
        return ShouldMoveToNextPoint();
    }

    public bool ShouldMoveToNextPoint()
    {
        // Calculate positions
        if (CurrentStepStartPosition == Vector2.zero)
        {
            CurrentStepStartPosition = Member.transform.position;
            try
            {
                NextStepStartPosition = (Vector2) Member.transform.position + NextStep(true);
            }
            catch (IndexOutOfRangeException e)
            {
                rigidbody2D.velocity = Vector2.zero;
                return true;
            }
            StepRadius = Vector2.Distance(CurrentStepStartPosition, NextStepStartPosition);
        }
        // Calculate distance between positions (radius)
        // Get current position
        // Get distance between this position and step position
        // If this distance is equal or larger than the radius, it should move to the next point
        Vector2 currentMemberPosition = Member.transform.position;
        float currentRadius = Vector2.Distance(CurrentStepStartPosition, currentMemberPosition);
        if (currentRadius > StepRadius)
        {
            CurrentStepStartPosition = Vector2.zero;
            NextStepStartPosition = Vector2.zero;
            StepRadius = -1.0f;
            if (currentStep >= ListCount)
            {
                rigidbody2D.velocity = Vector2.zero;
                return true;
            }
        }
        return false;

    }

    public Vector2 CurrentStep
    {
        get { return Positions[currentStep]; }
    }

    public Vector2 NextStep(bool plus = true)
    {
        if (currentStep + 1 >= ListCount)
        {
            throw new IndexOutOfRangeException();
        }
        return plus ? Positions[++currentStep] : Positions[currentStep];
    }

    public int ListCount
    {
        get
        {
            return this.Positions.Count;
        } 
    }
}