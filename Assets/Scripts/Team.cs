﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class Team : MonoBehaviour
{
    public List<Member> members;
    public Flag flag;
    public Base base_;
    public int score;
    public int team;

    private GameScript game;

    private void Start()
    {
        this.score = 0;
    }

    /// <summary>
    /// Makes each member of this team perform its action
    /// </summary>
    public void PerformActions()
    {
        ChangeTurn(false);

        foreach (Member member in this.members)
        {
            member.PerformActions();
        }
    }

    /// <summary>
    /// Raises the score of this team by 1
    /// </summary>
    public void RaiseScore()
    {
        Debug.Log("RAISE SCORE CALLED FROM TEAM");
        //if (game.CheckEndGame(++this.score))
        //{
        //    game.Win(this);
        //}
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
        foreach (Member member in this.members)
        {
            if (!member.ActionDone())
            {
                done = false;
            }
        }

        if (!this.flag.ActionDone())
        {
            done = false;
        }

        return done;
    }

    /// <summary>
    /// Checks if the team number is the same as the given integer
    /// </summary>
    /// <param name="t">the team to check</param>
    /// <param name="i">the team number to validate</param>
    /// <returns>whether the team number is the same as the integer</returns>
    public static bool operator ==(Team t, int i)
    {
        return t.team == i;
    }

    /// <summary>
    /// Checks if the team number is the same as the given integer
    /// </summary>
    /// <param name="t">the team to check</param>
    /// <param name="i">the team number to validate</param>
    /// <returns>whether the team number is not the same as the integer</returns>
    public static bool operator !=(Team t, int i)
    {
        return t.team != i;
    }

    /// <summary>
    /// Sets the turn of the player
    /// </summary>
    /// <param name="yourTurn">The bool which says if it's this member's turn</param>
    public void ChangeTurn(bool yourTurn)
    {
        foreach (Member m in this.members)
        {
            m.ChangeTurn(yourTurn);
        }
    }

}
