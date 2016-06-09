﻿using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    /// <summary>
    /// The first team.
    /// </summary>
    public Team team1;

    /// <summary>
    /// The second team.
    /// </summary>
    public Team team2;

    /// <summary>
    /// The team which has the current turn.
    /// </summary>
    private Team currentTeam;

    /// <summary>
    /// The score limit of the game.
    /// If a team reached this limit,
    /// they should win the game.
    /// </summary>
    public int scoreLimt;

    /// <summary>
    /// The status of the team.
    /// This can be:
    /// Planning OR Execute
    /// Depending on the value, the game checks
    /// different things in the update method.
    /// </summary>
    public TeamStatus teamStatus;

    /// <summary>
    /// A boolean to check if the 
    /// actions of team 1 are done.
    /// This is standard set to false.
    /// </summary>
    private bool team1ActionsDone;

    /// <summary>
    /// A boolean to check if the 
    /// actions of team 2 are done.
    /// This is standard set to false.
    /// </summary>
    private bool team2ActionsDone;

    //UI ELEMENTS
    public Text textCurrentTurn;
    public Text textScoreTeam1;
    public Text textScoreTeam2;

    /// <summary>
    /// This gets called to initialize this class.
    /// This method will setup the current teamStatus
    /// and set the currentTeam to team 1.
    /// Also sets the team1ActionsDone 
    /// and team2ActionsDone to false.
    /// </summary>
    public void Start()
    {
        SwitchTurn();
        teamStatus = TeamStatus.Planning;

        team1ActionsDone = false;
        team2ActionsDone = false;
    }

    /// <summary>
    /// Call this method when you want to switch turns.
    /// This will also change the teamStatus of the game.
    /// So if both players planned their moves and this gets called,
    /// it will change the teamStatus and wait for the players to be done
    /// with executing their actions.
    /// </summary>
    public void SwitchTurn()
    {
        if (currentTeam == null)
        {
            currentTeam = team1;
            textCurrentTurn.text = "Turn: Team 1";
            team1.ChangeTurn(true);
            team2.ChangeTurn(false);
            return;
        }

        if (currentTeam == team1)
        {
            currentTeam = team2;
            textCurrentTurn.text = "Turn: Team 2";
            team1.ChangeTurn(false);
            team2.ChangeTurn(true);
            return;
        }

        else if (currentTeam == team2)
        {
            currentTeam = team1;
            textCurrentTurn.text = "Turn: Team 1";
            team1.ChangeTurn(true);
            team2.ChangeTurn(false);
            teamStatus = TeamStatus.Executing;
            StartActions();
            return;
        }
    }

    /// <summary>
    /// Calls the PerformActions in the teams.
    /// The team instances will then execute the rest.
    /// teamStatus is set to EXECUTING.
    /// It also cancels all the input and the GUI components.
    /// </summary>
    public void StartActions()
    {
        teamStatus = TeamStatus.Executing;
        team1.PerformActions();
        team2.PerformActions();

        //TODO: DISABLE GUI COMPONENTS AND INPUT
    }

    /// <summary>
    /// Checks if the score limit is reached.
    /// If it is reached, this returns true otherwise false.
    /// </summary>
    public bool CheckEndGame(int teamScore)
    {
        if (teamScore >= scoreLimt)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// First checks if the teamStatus is executing.
    /// If it is executing then it waits for the 
    /// teamActions to be done. If they are, they
    /// set a boolean to try. 
    /// If both booleans are set to true,
    /// it will set the teamStatus to planning.
    /// It will also enable all the GUI components back.
    /// </summary>
    public void Update()
    {
        if (teamStatus == TeamStatus.Executing) 
        {
            if (team1.CheckActionsDone())
            {
                team1ActionsDone = true;
            }

            if (team2.CheckActionsDone())
            {
                team2ActionsDone = true;
            }

            if (team1ActionsDone && team2ActionsDone)
            {
                team1ActionsDone = false;
                team2ActionsDone = false;
                teamStatus = TeamStatus.Planning;

                //TODO: ENABLE GUI COMPONENTS AND INPUT AGAIN.
            }
        }
    }
}
