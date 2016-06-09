#region

using System;
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
        public int Speed;
        private bool yourTurn;

        //Member data
        public int Stamina;

        public Member(int speed, int stamina)
        {
            actionsDone = 0;
        }

        /// <summary>
        ///     Returns whether the member has finished performing its action
        /// </summary>
        /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
        public void ActionDone()
        {
            actionsDone++;
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
            Debug.Log(actions.Count() + " actions found");
            currentAction = 0;
            doPerform = true;
        }

        /// <summary>
        ///     Sets the action to a walk action to follow a specific pattern
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
//                WalkAction wa = this.actions[currentAction] as WalkAction;
//                if(wa != null)
//                {
//                    Debug.Log("WA: " + wa.NextStep(false));
//                    Debug.Log("Pos" + transform.position);
//                    rigidbody2D.velocity = wa.NextStep();
//                }
            }
        }

        public void ChangeTurn(bool yourTurn)
        {
            this.yourTurn = yourTurn;
        }
    }
}