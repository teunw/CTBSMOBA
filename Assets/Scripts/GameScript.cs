using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{

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
    public Text textWin;

    public Button playAgain;
    public Button endTurn;
    public Button kickButton;
    public Button besteGameButton;
    public ProgressBarBehaviour ProgressBar;

    public static GameScript instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Not allowed to instantiate multiple GameScripts!");
            Destroy(this);
        }
    }

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

        textWin.enabled = false;
        playAgain.gameObject.SetActive(false);
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

            foreach (Member m in team1.members)
            {
                m.RemoveLines();
            }

            team2.ChangeTurn(true);
            return;
        }

        else if (currentTeam == team2)
        {
            currentTeam = team1;
            textCurrentTurn.text = "Turn: Team 1";

            foreach (Member m in team2.members)
            {
                m.RemoveLines();
            }

            team1.ChangeTurn(false);
            team2.ChangeTurn(false);
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
        if (teamStatus == TeamStatus.Executing)
        {
            return;
        }
        teamStatus = TeamStatus.Executing;
        Debug.Log("Status: Executing");
        team1.PerformActions();
        team2.PerformActions();
        kickButton.interactable = false;
        besteGameButton.interactable = false;
        endTurn.interactable = false;
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

    public void Win(Team team)
    {
        textWin.text = "Team " + team.team + " won!";
        textWin.enabled = true;
        playAgain.gameObject.SetActive(true);
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
        kickButton.interactable = teamStatus == TeamStatus.Planning;
        besteGameButton.interactable = teamStatus == TeamStatus.Planning;
        if (teamStatus == TeamStatus.Executing)
        {
            if (!team1.CheckActionsDone())
            {
                return;
            }

            if (!team2.CheckActionsDone())
            {
                return;
            }
            team1ActionsDone = false;
            team2ActionsDone = false;
            teamStatus = TeamStatus.Planning;
            Debug.Log("Status: Planning");

            currentTeam = team1;
            team1.ChangeTurn(true);
            team2.ChangeTurn(false);
            endTurn.interactable = true;
            kickButton.interactable = true;
            besteGameButton.interactable = true;
            Debug.Log("BOTH TEAMS ARE DONE");
        }
    }

    /// <summary>
    /// Restart the scene.
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
