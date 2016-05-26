using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
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
            action = new WalkAction();
        }

        /// <summary>
        /// Performs the given action
        /// </summary>
        public void PerformAction()
        {
            performAction = true;
            actionDone = false;
            action.Perform();
        }

        /// <summary>
        /// Sets the action to a walk action to follow a specific pattern
        /// </summary>
        /// <param name="movementpoints">The pattern to follow</param>
        public void SetAction(List<Vector2> movementpoints)
        {
            //Fill in the action with a walk action
            action.WalkAction(movementpoints);
        }

        /// <summary>
        /// Returns whether the member has finished performing its action
        /// </summary>
        /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
        public bool IsActionDone()
        {
            return actionDone;
        }

        void Update()
        {
            //If not in the planning phase, skip the validation part
            if (actionDone && !performAction) return;

            //If the last location exists
            if (lastLocation != null)
            {
                //And equals to its parent's last position
                if (lastLocation == gameObject.transform.position)
                {
                    //Then it has not moved, and therefore is done with its action
                    actionDone = true;
                    performAction = false;
                }
                else
                {
                    //Else it is still in progress
                    actionDone = false;
                }
            }
            //Set the new last location
            lastLocation = gameObject.transform.position;
        }
    }
}
