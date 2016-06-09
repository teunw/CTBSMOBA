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
        ///     Performs the given action
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

        public void ChangeTurn(bool yourTurn)
        {
            this.yourTurn = yourTurn;
        }

        public void RemoveLines()
        {
            DrawManager.ClearLine(this);
        }

        public void WallHit()
        {
            actions.Clear();
        }

        public void IsHit(Vector3 velocity)
        {
            actions.Clear();
            transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}