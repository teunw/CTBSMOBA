using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkAction : Action
{
    private Rigidbody2D rigidbody2D;

    private Member member;
    private List<Vector2> positions;

    private int currentStep;
    private bool ready = false;

    /**
	 * 
	 * @param positions
	 */

    public WalkAction(Member member, List<Vector2> positions)
    {
        this.positions = positions;
        this.member = member;
        this.currentStep = 0;
    }

    public void setRigidbody2D(Rigidbody2D rigidbody2D)
    {
        this.rigidbody2D = rigidbody2D;
        Debug.Log("Rigidbody2D set succesfully");
    }

    public void CalculateMovement()
    {
        
    }

    public override void Perform(Member member)
    {
        //Set rigidbody2D from the member
        Debug.Log("Setting rigidbody2D");
        this.setRigidbody2D(this.member.gameObject.GetComponent<Rigidbody2D>());

        Debug.Log("Ready up");
        this.ready = true;
    }

    public Vector2 getStep(int index)
    {
        return this.positions[index];
    }

    public int getListCount()
    {
        return this.positions.Count;
    }
}