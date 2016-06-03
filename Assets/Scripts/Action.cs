using Assets.Scripts;
using UnityEngine;

public abstract class Action
{
    public Member Member
    { get; protected set; }

    public Action(Member member)
    {
        this.Member = member;
    }

    public abstract bool Update();
}