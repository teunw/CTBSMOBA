using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Member : MonoBehaviour, IFieldObject
{
    //Member data
    private int Stamina;
    private int Speed;

    //Action saving
    private Action action;

    //Verifying action is done
    private bool actionDone;
    private bool performAction;
    private Vector3 lastLocation;

    public Member(int speed, int stamina)
    {
        this.Speed = speed;
        this.Stamina = stamina;
        this.performAction = false;
        this.actionDone = false;
    }

    /// <summary>
    /// Performs the given action
    /// </summary>
    public void PerformAction()
    {
        this.performAction = true;
        this.actionDone = false;
        this.action.Perform();
    }

    /// <summary>
    /// Sets the action to a walk action to follow a specific pattern
    /// </summary>
    /// <param name="movementpoints">The pattern to follow</param>
    public void SetAction(List<Vector2> movementpoints)
    {
        //Fill in the action with a walk action
        this.action = new WalkAction(movementpoints);
    }

    /// <summary>
    /// Returns whether the member has finished performing its action
    /// </summary>
    /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
    public bool IsActionDone()
    {
        return this.actionDone;
    }

    void Update()
    {
        //If not in the planning phase, skip the validation part
        if (this.actionDone && !this.performAction) return;

        //If the last location exists
        if (this.lastLocation != null)
        {
            //And equals to its parent's last position
            if (this.lastLocation == this.gameObject.transform.position)
            {
                //Then it has not moved, and therefore is done with its action
                this.actionDone = true;
                this.performAction = false;
            }
            else
            {
                //Else it is still in progress
                this.actionDone = false;
            }
        }
        //Set the new last location
        this.lastLocation = this.gameObject.transform.position;
    }
}

