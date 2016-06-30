using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utils;
using UnityEngine;
namespace Assets.Scripts.Skills
{
    public class GrowAction : MonoBehaviour, Action
    {
        Vector3 scaleBefore;
        Vector3 scaleAfter;
        float lurpings;
        bool grow;
        bool shrink;
        public string Name
        {
            get { return "Grow"; }
        }


        public Color actionColor
        {
            get
            {
                return Color.green;
            }
        }

        void Start()
        {
            lurpings = 0f;
            grow = false;
            shrink = false;
            GetComponentInChildren<Member>().SetColor(actionColor);
            scaleBefore = new Vector3(1,1,1);
            scaleAfter = new Vector3(1, 2, 2);
            SendMessage(ActionConstants.OnSkillAdded, this, SendMessageOptions.DontRequireReceiver);

        }
        void Update() {
            if (grow)
            {
                gameObject.transform.localScale = Vector3.Lerp(scaleBefore, scaleAfter, lurpings);
                lurpings += 0.1f;

                if (lurpings >= 1)
                {
                    lurpings = 1f;
                    grow = false;
                }
            }
            else 
                if (shrink)
                {
                    gameObject.transform.localScale = Vector3.Lerp(scaleBefore, scaleAfter, lurpings);
                    lurpings -= 0.1f;
                    if (lurpings <= 0)
                    {
                        lurpings = 0f;
                        shrink = false;
                        gameObject.transform.localScale = Vector3.Lerp(scaleBefore, scaleAfter, lurpings);
                        SendMessage(ActionConstants.OnSkillExecuted);
                        Destroy(this);
                    }
                }
            
            
        }

        void OnExcecutingStart()
        {
            grow = true;
        }

        void OnExcecutingDone()
        {
            shrink = true;
            
        }
        void OnDestroy()
        {
            GetComponentInChildren<Member>().SetColor();
            SendMessage(ActionConstants.OnSkillRemoved, this, SendMessageOptions.DontRequireReceiver);

        }

    }
}
