#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Assets.Scripts
{
    public class Member : MonoBehaviour, IFieldObject
    {
        //Action saving
        private List<Action> actions;

        //Verifying action is done
        private int actionsDone;
        private int currentAction;

        private bool doPerform;
        public DrawManager DrawManager;
        private Vector3 lastLocation;
        private bool notMoving;
        public int Speed;
        private bool yourTurn;

        //Member data
        public int Stamina;

        public Member(int speed, int stamina)
        {
            actionsDone = 0;
        }

        /// <summary>
        /// Check if player is moving.
        /// Sets boolean notMoving to it's value.
        /// Sets position, then waits one second
        /// and checks it's position once more.
        /// If it didn't change it will set
        /// the boolean notMoving to true.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckMoving()
        {
            Vector3 startPos = transform.position;
            yield return new WaitForSeconds(1f);
            Vector3 finalPos = transform.position;
            if (startPos.x == finalPos.x && startPos.y == finalPos.y
                && startPos.z == finalPos.z)
            {
                notMoving = true;
            }
                
        }

        /// <summary>
        /// Returns whether the member has finished performing its action
        /// </summary>
        /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
        public bool ActionDone()
        {
            if (actions.Count != 0)
            {
                if (!actions.Last().isDone())
                {
                    return false;
                }
            }

            StartCoroutine(CheckMoving());

            if (notMoving == true)
            {
                notMoving = false;
                return true;
            }

            else
            {
                return false;
            }
        }

        private void Start()
        {
            actions = new List<Action>();
            if (DrawManager == null) throw new NullReferenceException("DrawManager is null!");
        }

        /// <summary>
        /// Performs the given action
        /// </summary>
        public void PerformActions()
        {
            currentAction = 0;
            doPerform = true;
        }


        /// <summary>
        /// Sets the action to a walk action to follow a specific pattern
        /// </summary>
        /// <param name="movementpoints">The pattern to follow</param>
        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        private void OnMouseDown()
        {
            if (yourTurn)
            {
                DrawManager.SetMember(this);
            }
        }

        public void FixedUpdate()
        {
            if (doPerform)
            {
                if (currentAction >= actions.Count)
                {
                    doPerform = false;
                    actions.Clear();
                    return;
                }

                bool done = actions[currentAction].Update();
                if (done)
                    currentAction++;
            }
        }

        /// <summary>
        /// Use this to make sure the player
        /// can't give input when he's not
        /// supposed to. If the boolean parameter
        /// is false the drawmanager won't be 
        /// able to draw a line so -> make an action.
        /// </summary>
        /// <param name="yourTurn">
        /// The boolean which decides 
        /// if this member may move.
        /// </param>
        public void ChangeTurn(bool yourTurn)
        {
            this.yourTurn = yourTurn;
        }

        /// <summary>
        /// Remove all the lines of the player.
        /// Does not remove the actions.
        /// </summary>
        public void RemoveLines()
        {
            DrawManager.ClearLine(this);
        }

        /// <summary>
        /// Clear the actions of the player.
        /// This is called when a wall has
        /// been hit.
        /// </summary>
        public void WallHit()
        {
            actions.Clear();
        }

        /// <summary>
        /// Checks if this user is hit.
        /// If it is hit, clear it's actions
        /// and set a velocity and it's current speed.
        /// Rigidbody will take care of the rest of 
        /// the user's movement.
        /// </summary>
        /// <param name="velocity">
        /// The velocity of the player at the moment of impact.
        /// </param>
        public void IsHit(Vector3 velocity)
        {
            actions.Clear();
            transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}