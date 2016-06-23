using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class IFieldObject : MonoBehaviour
    {
        /// <summary>
        /// Threshold for the speed for when a member has officially stopped moving
        /// </summary>
        protected float noMovementThreshold = 0.0001f;

        /// <summary>
        /// Amount of frames where the member has to be non-moving
        /// </summary>
        protected const int noMovementFrames = 3;

        /// <summary>
        /// Storage of the locations in these frames
        /// </summary>
        protected Vector3[] previousLocations = new Vector3[noMovementFrames];

        /// <summary>
        /// Boolean showing whether a member has stopped moving or not
        /// </summary>
        protected bool isMoving;

        protected virtual void Start()
        {
            ResetPoints();
        }

        /// <summary>
        /// Method for calculating the distance of whether an object is done with its action or not
        /// </summary>
        /// <returns>Whether it has done its action</returns>
        public virtual bool ActionDone()
        {
            //Debug.Log(gameObject.name + (IsMoving ? ": \tis moving" : ": \tis not moving") + " (done: " + (skillsDone && !IsMoving) + ")");
            CheckMovement();
            return !isMoving;
        }

        /// <summary>
        /// Checks whether the member is still moving
        /// </summary>
        protected virtual void CheckMovement()
        {
            // Move the locations
            for (int i = 0; i < previousLocations.Length - 1; i++)
            {
                previousLocations[i] = previousLocations[i + 1];
            }
            // Set last location to the current location
            previousLocations[previousLocations.Length - 1] = transform.position;

            // If there are still vector3 zeroes in the array, that means that not all values have been filled
            if (previousLocations.Contains(Vector3.zero))
            {
                isMoving = true;
                return;
            }

            bool doesMove = true;
            // Check the distances between the points in your previous locations
            // If for the past several updates, there are no movements smaller than the threshold,
            // you can most likely assume that the object is not moving
            for (int i = 0; i < previousLocations.Length - 1; i++)
            {
                // If it is larger than the threshold, it is moving, else not
                if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
                {
                    doesMove = true;
                    break;
                }
                else
                {
                    doesMove = false;
                }
            }
            isMoving = doesMove;
        }

        /// <summary>
        /// Resets the movement points, so the member knows it needs to revalidate its movement
        /// </summary>
        protected void ResetPoints()
        {
            for (int i = 0; i < previousLocations.Length; i++)
            {
                previousLocations[i] = Vector3.zero;
            }
        }
    }
}
