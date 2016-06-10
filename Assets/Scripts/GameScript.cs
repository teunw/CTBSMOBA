using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;

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
    /// This automatically changes round after 2 calls (of team 1 and team 2)
    /// This will also change the teamStatus of the game.
    /// So if both players planned their moves and this gets called,
    /// it will change the teamStatus and wait for the players to be done
    /// with executing their actions.
    /// </summary>
    public void SwitchTurn()
    {
        // If current team is not null (is null once team 2 has ended their game), so 1 or 2
        // Then the turn for the current team is ended
        // The drawn lines for the team (1 or 2) are hidden
        if (currentTeam != null)
        {
            // Set the current team to no longer able to change their actions
            currentTeam.ChangeTurn(false);
            // Remove visibility of the lines of this team
            foreach (Member m in currentTeam.members)
            {
                m.RemoveLines();
            }

            // In case it was team 2's turn
            // Reset the current team 'selection'
            // Start the actions
            if (currentTeam == 2)
            {
                currentTeam = null;
                StartActions();
                return;
            }
        }

        // If there is no current team, that means that the new round has just started
        // Set the new team to team 1
        currentTeam = currentTeam == null ? team1 : team2;

        // Set the new current team to be able to change their actions
        currentTeam.ChangeTurn(true);

        // Show indication of the new, active team
        textCurrentTurn.text = "Turn: Team " + currentTeam.team;
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
        endTurn.gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the score limit is reached.
    /// If it is reached, this returns true otherwise false.
    /// </summary>
    public bool CheckEndGame(int teamScore)
    {
        return teamScore >= scoreLimt;
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
                Debug.Log("Status: Planning");

                SwitchTurn();
                endTurn.gameObject.SetActive(true);
                Debug.Log("BOTH TEAMS ARE DONE");
            }
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
