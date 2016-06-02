using UnityEngine;

public abstract class Action : MonoBehaviour
{

    public virtual void Start() { }

    public virtual void Update() { }

    public abstract void Perform();
}