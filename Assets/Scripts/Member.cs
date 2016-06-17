#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Skills;
using UnityEditor;
using UnityEngine;

#endregion

namespace Assets.Scripts
{
    public class Member : MonoBehaviour, IFieldObject
    {
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
        /// A bool indicating whether the skills are done 
        /// </summary>
        private bool skillsDone;

        /// <summary>
        /// The speed of this player.
        /// The speed of the character is based on this.
        /// </summary>
        [Range(0, 250)] public int Speed;

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
        /// The name of the character
        /// </summary>
        public string PlayerName;
        
        /// <summary>
        /// Threshold for the speed for when a member has officially stopped moving
        /// </summary>
        private float noMovementThreshold = 0.0001f;

        /// <summary>
        /// Amount of frames where the member has to be non-moving
        /// </summary>
        private const int noMovementFrames = 3;

        /// <summary>
        /// Storage of the locations in these frames
        /// </summary>
        Vector3[] previousLocations = new Vector3[noMovementFrames];

        /// <summary>
        /// Boolean showing whether a member has stopped moving or not
        /// </summary>
        private bool isMoving;
        public bool IsMoving
        {
            get { return isMoving; }
        }

        /// <summary>
        /// Returns whether the member has finished performing its action
        /// </summary>
        /// <returns>Whether their action is finished, or if the phase is in planning mode, whichever is true</returns>
        public bool ActionDone()
        {
            if (GetComponent<KickAction>() == null || GetComponents<TiedTogetherAction>() == null) skillsDone = true;
            //Debug.Log(gameObject.name + (IsMoving ? ": \tis moving" : ": \tis not moving") + " (done: " + (skillsDone && !IsMoving) + ")");
            return (skillsDone && !IsMoving);
        }

        void Update()
        {
            if (GameScript.instance.teamStatus != TeamStatus.Executing) return;
            // Move the locations
            for (int i = 0; i < previousLocations.Length - 1; i++)
            {
                previousLocations[i] = previousLocations[i + 1];
            }
            // Set last location to the current location
            previousLocations[previousLocations.Length - 1] = transform.position;

            // Check the distances between the points in your previous locations
            // If for the past several updates, there are no movements smaller than the threshold,
            // you can most likely assume that the object is not moving
            for (int i = 0; i < previousLocations.Length - 1; i++)
            {
                // If it is larger than the threshold, it is moving, else not
                if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    break;
                }
            }
        }


        /// <summary>
        /// The start method, which checks
        /// if the drawmanager is null. And
        /// it initializes a list of actions.
        /// </summary>
        private void Start()
        {
            if (DrawManager == null) throw new NullReferenceException("DrawManager is null!");
            for (int i = 0; i < previousLocations.Length; i++)
            {
                previousLocations[i] = Vector3.zero;
            }
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
        /// Performs the actions
        /// </summary>
        public void PerformActions()
        {
            SendMessage(ActionConstants.OnMemberWalkString);
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
        /// Performs an action after the member has walked
        /// </summary>
        void OnMemberWalkDone()
        {
            if (GetComponent<KickAction>() == null)
            {
                skillsDone = true;
            }
        }

        /// <summary>
        /// Performs an action after the skills have been executed
        /// </summary>
        void OnSkillExecuted()
        {
            skillsDone = true;
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
            transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }

        public void ActionPressed(Type action)
        {
            if (action.IsSubclassOf(typeof(MonoBehaviour))) throw new Exception("Type isn't monobehaviour!");
            Component c = GetComponent(action);
            if (c != null)
            {
                Destroy(c);
            }
            else
            {
                gameObject.AddComponent(action);
            }
        }
    }
}