﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class KickAction : MonoBehaviour, Action
    {
        public string Name
        {
            get { return "Kick"; }
        }

        private bool shouldExecuteSkill;
        public float KickForce = 15f;

        void Update()
        {
            if (!shouldExecuteSkill) return;
           
            Vector2 forwardPos = transform.position + (1f*transform.right);
            Vector2 pointB = transform.position + (1f*-transform.right) + (3f*transform.forward);
            Collider2D[] colliders = Physics2D.OverlapAreaAll(forwardPos, pointB);
            foreach (Collider2D collider in colliders)
            {
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                if (rb == null || gameObject == collider.gameObject) continue;
                
                Vector3 targetPosition = collider.gameObject.transform.position;
                Vector3 position = transform.position;

                Vector3 heading = targetPosition - position;
                float distance = heading.magnitude;
                Vector3 direction = heading/distance;

                rb.AddForce(direction * KickForce, ForceMode2D.Impulse);
            }
            EndKick();
        }

        void OnMemberWalkDone()
        {
            Debug.Log("Kick action");
            shouldExecuteSkill = true;
        }

        public void EndKick()
        {
            Destroy(this);
            SendMessage(ActionConstants.OnSkillExecuted);
        }
    }
}