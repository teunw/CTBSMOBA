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
        private Material color
        {
            get
            {
                return (Material)Resources.Load("Materials/Rope", typeof(Material));
            }
        }

        public string Name
        {
            get { return "Tied Together"; }
        }
        
        public bool FirstPhase
        {
            get
            {
                return doBind < 1;
            }
        }

        private int doBind = 0; // 0: Do nothing | 1: Bind | 2: Wait | 3: Unbind
        private List<LineRenderer> lines = new List<LineRenderer>();
        private List<GameObject> influencedObjects = new List<GameObject>();
        public float TieRange = 1.75f;
        public float TieDistance = 2f;

        public Color actionColor
        {
            get
            {
                return Color.green;
            }
        }

        void Start()
        {
            GetComponentInChildren<Member>().SetColor(actionColor);
            SendMessage(ActionConstants.OnSkillAdded, this, SendMessageOptions.DontRequireReceiver);
        }

        void OnDestroy()
        {
            GetComponentInChildren<Member>().SetColor();
            for (int i = 0; i < lines.Count; i++)
            {
                try
                {
                    lines[i].SetPosition(0, gameObject.transform.position);
                    lines[i].SetPosition(1, influencedObjects[i].transform.position);
                }
                catch(Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
            foreach (SpringJoint2D springJoint in this.gameObject.GetComponents<SpringJoint2D>())
            {
                Destroy(springJoint);
            }
            foreach (GameObject influencedObject in this.influencedObjects)
            {
                Destroy(influencedObject.GetComponent<LineRenderer>());
            }
            EndTiedTogetherUnbind();
            SendMessage(ActionConstants.OnSkillRemoved, this, SendMessageOptions.DontRequireReceiver);
        }

        void Update()
        {
            switch (doBind)
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
                                SpringJoint2D springJoint = this.gameObject.GetComponent<SpringJoint2D>();
                                LineRenderer line = this.gameObject.GetComponent<LineRenderer>();
                                // If this character is not influenced by TTG
                                if (springJoint == null && line == null)
                                {
                                    // If the to-influence character is not influenced by TTG
                                    springJoint = coll.gameObject.GetComponent<SpringJoint2D>();
                                    line = coll.gameObject.GetComponent<LineRenderer>();
                                    if (springJoint == null && line == null)
                                    {
                                        // Influence this and the other character
                                        springJoint = this.gameObject.AddComponent<SpringJoint2D>();
                                        springJoint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                                        springJoint.distance = TieDistance;
                                        springJoint.autoConfigureDistance = false;

                                        // Visualise the springjoints
                                        if (line == null)
                                        {
                                            line = coll.gameObject.AddComponent<LineRenderer>();
                                            line.SetWidth(0.08f, 0.08f);
                                            line.SetVertexCount(2);
                                            line.material = color;
                                            lines.Add(line);
                                            influencedObjects.Add(coll.gameObject);
                                        }
                                    }
                                }
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