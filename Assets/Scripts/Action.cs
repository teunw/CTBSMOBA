#region

using Assets.Scripts;

#endregion

public abstract class Action
{
    public Action(Member member)
    {
        Member = member;
    }

    public Member Member { get; protected set; }

    /// <summary>
    ///     Updates to the member, implementation dependent on member
    /// </summary>
    /// <returns>Return true when action is done, false otherwise</returns>
    public abstract bool Update();
    public abstract bool isDone();
}