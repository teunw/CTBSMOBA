using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Skills;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SkillIndicator : MonoBehaviour
    {
        public GameObject KickIndicator;
        public GameObject RehtegotDietIndicator;

        private Vector3 LinePosition;
        private Vector3 LinePosition2;
        private Type Skill;

        private GameObject GetSkillGameObject(Type skill)
        {
            if (skill != typeof(KickAction) && skill != typeof(TiedTogetherAction)) throw new ArgumentException("Invalid skill type");
            return (skill == typeof(KickAction)) ? KickIndicator : RehtegotDietIndicator;
        }
        public void SetActive(Vector3 pos, Vector3 pos2, Type skill)
        {
            this.LinePosition = pos;
            this.LinePosition2 = pos2;
            this.Skill = skill;
        }

        public void SetInactive()
        {
            Skill = null;
            LinePosition = Vector3.zero;
            LinePosition2 = Vector3.zero;
        }

        void Update()
        {
            if (Skill == null)
            {
                KickIndicator.SetActive(false);
                RehtegotDietIndicator.SetActive(false);
            }
            else
            {
                KickIndicator.SetActive(Skill == typeof(KickAction));
                RehtegotDietIndicator.SetActive(Skill == typeof(TiedTogetherAction));
                if (Skill == typeof(TiedTogetherAction))
                {
                    //Check if the TTG action is not in fase 2 or higher, then don't show
                }

                GameObject gm = GetSkillGameObject(Skill);
                gm.transform.position = LinePosition;
                gm.transform.LookAt(LinePosition2);
            }
        }
    }
}
