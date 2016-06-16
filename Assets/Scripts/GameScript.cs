using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

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

    //SKILL INDICATORS
    public Sprite circleIndicator;
    public Sprite diamondIndicator;
    public Sprite hexagonIndicator;
    public Sprite polygonIndicator;
    public Sprite squareIndicator;
    public Sprite triangleIndicator;
    public GameObject character1; //TODO: not hardcoded
    public GameObject indicatorRotationPoint;

    private Skill selectedSkill = null;
    private GameObject skillIndicator;
    private bool casting = false;

    public List<GameObject> fieldObjects;

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
        
        foreach(Member member in team1.members)
        {
            fieldObjects.Add(member.gameObject);
        }
        fieldObjects.Add(team1.flag.gameObject);
        foreach(Member member in team2.members)
        {
            fieldObjects.Add(member.gameObject);
        }
        fieldObjects.Add(team2.flag.gameObject);
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
        endTurn.gameObject.SetActive(false);
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
        playAgain.gameObject.SetActive(false);
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

                currentTeam = team1;
                team1.ChangeTurn(true);
                team2.ChangeTurn(false);
                endTurn.gameObject.SetActive(true);
                Debug.Log("BOTH TEAMS ARE DONE");
            }
        }
        if (selectedSkill != null)
        {
            skillIndicator.transform.parent = indicatorRotationPoint.transform;
            indicatorRotationPoint.transform.position = new Vector3(character1.transform.position.x, character1.transform.position.y, 0.05f);
            if (selectedSkill.IsSkillshot())
            {
                Debug.Log("Is skillshot");
                skillIndicator.transform.localPosition = new Vector3(selectedSkill.GetRange() / 2, 0, 0);
            } 
            else
            {
                skillIndicator.transform.localPosition = Vector3.zero;
            }

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - indicatorRotationPoint.transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            indicatorRotationPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if(Input.GetMouseButtonDown(0))
            {
                SkillAction sa = new SkillAction(skillIndicator.GetComponent<SpriteRenderer>(), character1.GetComponent<Member>(), SkillType.TiedTogether);
                sa.TieTogetherAll();
                GameObject.Destroy(indicatorRotationPoint.transform.GetChild(0).gameObject);
                indicatorRotationPoint.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                selectedSkill = null;
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

    public void Skill1Casting()
    {
        selectedSkill = new Skill("Tied Together", circleIndicator, 5f, 0.0f, 0.0f);
        skillIndicator = selectedSkill.CreateIndicator();
    }
}
