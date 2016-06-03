﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{

    public class Member : MonoBehaviour, IFieldObject
    {

        //Member data
        public int Stamina;
        public int Speed;
        public DrawManager DrawManager;

        //Action saving
        private List<Action> actions;

        //Verifying action is done
        private int actionsDone;
        private Vector3 lastLocation;

        private bool doPerform = false;
        private int currentAction;

        public Member(int speed, int stamina)
        {
            this.actionsDone = 0;
        }

        private void Start()
        {
            this.actions = new List<Action>();
            if (DrawManager == null)
            {
                throw new NullReferenceException("Drawmanager not set");
            }
        }

        /// <summary>
        /// Performs the given action
        /// </summary>
        public void PerformActions()
        {
            Debug.Log(this.actions.Count() + " actions found");
            this.currentAction = 0;
            this.doPerform = true;
        }

        /// <summary>
        /// Sets the action to a walk action to follow a specific pattern
        /// </summary>
        /// <param name="movementpoints">The pattern to follow</param>
        public void AddAction(Action action)
        {
            this.actions.Add(action);
        }

        /// <summary>
        /// Returns whether the member has finished performing its action
        /// </summary>
        /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
        public void ActionDone()
        {
            this.actionsDone++;
        }

        void Update()
        {
            
        }

        void OnMouseDown()
        {
            DrawManager.SetMember(this);
        }

        public void FixedUpdate()
        {
            if (doPerform)
            {
                bool done = this.actions[currentAction].Update();
//                WalkAction wa = this.actions[currentAction] as WalkAction;
//                if(wa != null)
//                {
//                    Debug.Log("WA: " + wa.NextStep(false));
//                    Debug.Log("Pos" + transform.position);
//                    rigidbody2D.velocity = wa.NextStep();
//                }
            }
        }
    }
}
