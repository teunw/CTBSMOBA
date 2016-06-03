using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkAction : Action
{
    private Rigidbody2D rigidbody2D;
    private readonly List<Vector2> Positions;
    private Vector2 CurrentStepPos, NextStepPos;

    private int currentStep;

    /**
	 * 
	 * @param Positions
	 */

    public WalkAction(Member member, List<Vector2> positions) : base(member)
    {
        this.Positions = positions;
        this.currentStep = 0;
        this.rigidbody2D = Member.gameObject.GetComponent<Rigidbody2D>();
    }

    public override bool Update()
    {
        rigidbody2D.velocity = Positions[currentStep];

        return true;
    }

    public bool shouldMoveToNextPoint()
    {
        
    }

    public Vector2 CurrentStep
    {
        get { return Positions[currentStep]; }
    }

    public Vector2 NextStep(bool plus = true)
    {
        if (currentStep >= ListCount)
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