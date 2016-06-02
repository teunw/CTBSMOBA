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
        private bool performAction;
        private Vector3 lastLocation;

        private bool ready = false;
        private int currentAction;
        private int currentStep;

        public Member(int speed, int stamina)
        {
            this.performAction = false;
            this.actionsDone = 0;
        }

        private void Start()
        {
            this.actions = new List<Action>();
        }

        /// <summary>
        /// Performs the given action
        /// </summary>
        public void PerformActions()
        {
            this.performAction = true;
            this.actionsDone = 0;
            Debug.Log(this.actions.Count() + " actions found");
            this.currentAction = 0;
            this.currentStep = 0;
            this.ready = true;
        }

        /// <summary>
        /// Sets the action to a walk action to follow a specific pattern
        /// </summary>
        /// <param name="movementpoints">The pattern to follow</param>
        public void SetAction(Action action)
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
            if (DrawManager != null)
                DrawManager.SetMember(this);
        }

        public void FixedUpdate()
        {
            if (ready)
            {
                if(this.actions[currentAction].GetType().Equals(typeof(WalkAction)))
                {
                    WalkAction wa = (WalkAction)this.actions[currentAction];
                    Debug.Log("WA: " + wa.getStep(currentStep + 1));
                    Debug.Log("Pos" + transform.position);
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x, transform.position.y) + wa.getStep(currentStep + 1);
                    if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), wa.getStep(currentStep + 1)) <= 0.1f) {
                        if(currentStep + 2 <= wa.getListCount())
                        {
                            currentStep++;
                        } else
                        {
                            currentAction = 1;
                        }
                    }
                }
            }
        }
    }
}
