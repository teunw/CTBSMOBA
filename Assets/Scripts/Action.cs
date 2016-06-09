#region

using Assets.Scripts;

#endregion

public abstract class Action
{
    /// <summary>
    /// The constructor of the abstract class Action.
    /// Initializes the member variable.
    /// </summary>
    /// <param name="member">The member to initialize</param>
    public Action(Member member)
    {
        Member = member;
    }

    /// <summary>
    /// Getter/Setter for member.
    /// </summary>
    public Member Member { get; protected set; }

    /// <summary>
    /// Updates to the member, implementation dependent on member
    /// </summary>
    /// <returns>Return true when action is done, false otherwise</returns>
    public abstract bool Update();
    public abstract bool isDone();
}