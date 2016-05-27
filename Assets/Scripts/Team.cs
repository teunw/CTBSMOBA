using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {
    private ArrayList<Member> members;
    private Flag flag;
    private Base base_; //TODO: Better name
    private int score;

    public Team (Flag flag, Base base_, ArrayList<Member> members)
    {
        this.flag = flag;
        this.base_ = base_;
        this.members = members;
        this.score = 0;
    }

    /// <summary>
    /// Makes each member of this team perform its action
    /// </summary>
    public void PerformActions()
    {
        foreach(Member member in this.members)
        {
            member.PerformAction();
        }
    }

    /// <summary>
    /// Raises the score of this team by 1
    /// </summary>
    public void RaiseScore()
    {
        this.score++;
        if(this.score == 3)
        {
            this.Win();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Win()
    {
        //TODO
    }

    /// <summary>
    /// 
    /// </summary>
    public void Lose()
    {
        //TODO
    }

    /// <summary>
    /// Return true if all the members of this team have finished their action
    /// </summary>
    /// <returns></returns>
    public bool CheckActionsDone()
    {
        bool done = true;
        foreach(Member member in this.members)
        {
            if (!member.isDone()) done = false;
        }
        return done;
    }
}
