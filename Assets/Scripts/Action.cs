using Assets.Scripts;
using UnityEngine;

public abstract class Action
{

    public virtual void Start() { }

    public virtual void Update() { }

    public abstract void Perform(Member member);
}