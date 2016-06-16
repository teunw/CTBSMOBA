using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;
using System.Collections.Generic;

public class SkillAction : Action
{
    private Collider2D collider;
    private SkillType skillType;
    private Member member;

    private GameScript gameScript;
    private List<GameObject> influencedObjects;

    public SkillAction(Collider2D collider, Member member, SkillType skillType) : base(member)
    {
        this.collider = collider;
        this.member = member;
        this.skillType = skillType;
        this.influencedObjects = new List<GameObject>();

        this.gameScript = GameObject.Find("GameManager").GetComponent<GameScript>();
    }

    public override bool isDone()
    {
        throw new NotImplementedException();
    }

    public override bool Update()
    {
        throw new NotImplementedException();
    }

    public void TieTogetherAll()
    {
        foreach(GameObject fieldObject in gameScript.fieldObjects)
        {
            if (!fieldObject.name.Equals("Flag"))
            {
                if(this.collider.OverlapPoint(fieldObject.GetComponent<Member>().GetPosition()))
                {
                    Debug.Log(fieldObject.name);
                    TieTogether(fieldObject);
                    influencedObjects.Add(fieldObject);
                }
            }
        }
    }

    public void TieTogether(GameObject other)
    {
        if (!member.gameObject.Equals(other))
        {
            Debug.Log("Found other player to bind");
            Debug.Log(Vector3.Distance(this.member.transform.position, other.transform.position));
            SpringJoint2D springJoint = this.member.gameObject.AddComponent<SpringJoint2D>();
            springJoint.autoConfigureDistance = false;
            springJoint.distance = 2;

            springJoint.connectedBody = other.GetComponent<Rigidbody2D>();
        }
    }
}
