using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class TiedTogetherAction : MonoBehaviour, Action
    {
        public string Name
        {
            get { return "Tied Together"; }
        }
        
        private int doBind = 0; // 0: Do nothing | 1: Bind | 2: Wait | 3: Unbind
        private List<LineRenderer> lines = new List<LineRenderer>();
        private List<GameObject> influencedObjects = new List<GameObject>();
        public float TieRange = 1.75f;
        public float TieDistance = 2f;

        void Update()
        {
            switch(doBind)
            {
                case 0:
                    return;
                case 1:
                    foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, this.TieRange))
                    {
                        if (coll.GetComponent<Member>() != null || coll.GetComponent<Flag>() != null)
                        {
                            if (!this.gameObject.Equals(coll.gameObject))
                            {
                                SpringJoint2D springJoint = this.gameObject.AddComponent<SpringJoint2D>();
                                springJoint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                                springJoint.distance = TieDistance;
                                springJoint.autoConfigureDistance = false;

                                LineRenderer line = coll.gameObject.AddComponent<LineRenderer>();
                                line.SetWidth(0.08f, 0.08f);
                                line.SetVertexCount(2);

                                lines.Add(line);
                                influencedObjects.Add(coll.gameObject);
                            }
                        }
                    }

                    EndTiedTogetherBind();
                    break;
                case 2:
                    for (int i = 0; i < lines.Count; i++)
                    {
                        lines[i].SetPosition(0, this.gameObject.transform.position);
                        lines[i].SetPosition(1, influencedObjects[i].transform.position);
                    }
                    break;
                case 3:
                    foreach(SpringJoint2D springJoint in this.gameObject.GetComponents<SpringJoint2D>())
                    {
                        Destroy(springJoint);
                    }
                    foreach(GameObject influencedObject in this.influencedObjects)
                    {
                        Destroy(influencedObject.GetComponent<LineRenderer>());
                    }
                    EndTiedTogetherUnbind();
                    break;
            }
        }

        void OnMemberWalkDone()
        {
            Debug.Log("Tied Together Beste Game");
            doBind++;
        }

        public void EndTiedTogetherBind()
        {
            //Destroy(this);
            doBind++;
            Debug.Log("Done binding");
            SendMessage(ActionConstants.OnSkillExecuted);
        }

        public void EndTiedTogetherUnbind()
        {
            Debug.Log("Done unbinding");
            SendMessage(ActionConstants.OnSkillExecuted);
            Destroy(this);
        }
    }
}