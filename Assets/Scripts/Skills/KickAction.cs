using System;
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

        void Update()
        {
            if (!shouldExecuteSkill) return;
            GetComponent<Rigidbody2D>().AddExplosionForce(50f, transform.position, 5f);
            EndKick();
        }

        void OnMemberWalkDone()
        {
            Debug.Log("Kick action");
            shouldExecuteSkill = true;
        }

        public void EndKick()
        {
            SendMessage(ActionConstants.OnSkillExecuted);
        }
    }
}