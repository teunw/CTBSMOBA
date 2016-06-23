using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
    /// <summary>
    /// The game object.
    /// </summary>
    public GameScript game;

    /// <summary>
    /// A list of members in this team.
    /// </summary>
    public List<Member> members;

    /// <summary>
    /// The flag of this team.
    /// </summary>
    public Flag flag;

    /// <summary>
    /// The base of this team.
    /// </summary>
    public Base base_;

    /// <summary>
    /// The score of this team.
    /// </summary>
    public int score;

    /// <summary>
    /// The number of this team.
    /// </summary>
    public int team;
    
    /// <summary>
    /// The text element which indicates the score.
    /// </summary>
    public Text textScore;

    /// <summary>
    /// The soundmanager which is responsible for all the sounds.
    /// </summary>
    public Sound soundManager;

    /// <summary>
    /// Team's color.
    /// </summary>
    public Color color;

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
    /// Raises the score of this team by 1,
    /// Checks if the team needs to win.
    /// </summary>
    public void RaiseScore()
    {
        score += 1;
        textScore.text = score.ToString();
        if (game.CheckEndGame(score))
        {
            soundManager.playWinSound();
            game.Win(this);
            return;
        }

        soundManager.playScoreSound();
    }

    /// <summary>
    /// Return true if all the members of this team have finished their action
    /// </summary>
    /// <returns></returns>
    public bool CheckActionsDone()
    {
        foreach (Member member in this.members)
        {
            if (!member.ActionDone())
            {
                return false;
            }
        }

        if (!this.flag.ActionDone())
        {
            return false;
        }
        return true;
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
