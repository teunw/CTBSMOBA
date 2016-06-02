using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkAction : Action
{
    private Member member;
    private List<Vector2> Positions;
    private Rigidbody2D rigidbody2D;

    /**
	 * 
	 * @param positions
	 */

    public WalkAction(Member member, List<Vector2> positions)
    {
        this.Positions = positions;
        this.member = member;
    }

    public void CalculateMovement()
    {
        
    }

    public override void Update()
    {
        base.Update();

    }

    public override void Perform()
    {
        
    }
}