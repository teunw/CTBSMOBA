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
        /// <summary>
        /// A list of actions which this player has.
        /// </summary>
        private List<Action> actions;

        /// <summary>
        /// The current action which the player is performing.
        /// </summary>
        private int currentAction;

        /// <summary>
        /// A bool which indicates if the member should perform
        /// his actions.
        /// </summary>
        private bool doPerform;

        /// <summary>
        /// The DrawManager which is responsible for the drawing
        /// the lines and making the actions.
        /// </summary>
        public DrawManager DrawManager;

        /// <summary>
        /// A bool which indicates if this member is allowed to 
        /// draw actions.
        /// </summary>
        private bool yourTurn;

        /// <summary>
        /// A bool which indicates if the player is moving.
        /// </summary>
        private bool notMoving;

        /// <summary>
        /// The speed of this player.
        /// The speed of the character is based on this.
        /// </summary>
        public int Speed;

        /// <summary>
        /// The stamina of this player.
        /// The length of the line is based on this.
        /// </summary>
        public int Stamina;
        
        /// <summary>
        /// The soundmanager which is responsible for making sounds.
        /// </summary>
        public Sound soundManager;

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
            yield return new WaitForSeconds(0.03f);
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

        /// <summary>
        /// The start method, which checks
        /// if the drawmanager is null. And
        /// it initializes a list of actions.
        /// </summary>
        private void Start()
        {
            actions = new List<Action>();
            if (DrawManager == null) throw new NullReferenceException("DrawManager is null!");
        }

        /// <summary>
        /// Sets the currentAction to 0,
        /// and sets the boolean doPerform to true.
        /// </summary>
        public void PerformActions()
        {
            currentAction = 0;
            doPerform = true;
        }


        /// <summary>
        /// Adds an action to this member to execute once the round has started
        /// If the action is a new walk action, all other actions are deleted first
        /// </summary>
        /// <param name="movementpoints">The pattern to follow</param>
        public void AddAction(Action action)
        {
            if (action is WalkAction)
            {
                actions.Clear();
            }
            actions.Add(action);
        }

        /// <summary>
        /// Checks if you're allowed
        /// to draw and if so, it draws a line
        /// from this member.
        /// </summary>
        private void OnMouseDown()
        {
            if (yourTurn)
            {
                DrawManager.SetMember(this);
            }
        }

        /// <summary>
        /// If the member is allowed to perform it's actions,
        /// it will till it has reached the end.
        /// </summary>
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
            soundManager.playBumpSound();
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
            soundManager.playBumpSound();
            actions.Clear();
            transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        
        /// <summary>
        /// Resets the actions of this member.
        /// The list gets cleared.
        /// </summary>
        public void ResetActions()
        {
            this.actions.Clear();
        }
    }
}