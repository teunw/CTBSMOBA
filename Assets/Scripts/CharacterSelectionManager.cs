#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#endregion
using System.IO;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        /// <summary>
        /// The gameobject which we use to put it
        /// into the game.
        /// </summary>
        public GameObject originalGameObject;

        /// <summary>
        /// The empty parent object to put the members in.
        /// This is just to keep things clean at runtime.
        /// </summary>
        public GameObject emptyParentObject;

        /// <summary>
        /// List of members which need to be put into the game.
        /// </summary>
        private List<Member> members;

        /// <summary>
        /// The end position of the camera's journey.
        /// </summary>
        private Vector3 endPosition;

        /// <summary>
        /// A bool which indicates if the camera should move on 
        /// the next update.
        /// </summary>
        private bool toMove;

        /// <summary>
        /// A button which is used to move camera left.
        /// </summary>
        public Button buttonLeft;

        /// <summary>
        /// A button which is used to move camera right.
        /// </summary>
        public Button buttonRight;

        /// <summary>
        /// The members who are currently in your team.
        /// </summary>
        private List<Member> currentTeam;

        /// <summary>
        /// The text for current member 1.
        /// </summary>
        public Text textMember1;

        /// <summary>
        /// The text for current member 2.
        /// </summary>
        public Text textMember2;

        /// <summary>
        /// The text for current member 3.
        /// </summary>
        public Text textMember3;

        /// <summary>
        /// Button to go to the next scene.
        /// </summary>
        public Button buttonNext;

        /// <summary>
        /// Team 1's member.
        /// </summary>
        private List<Member> team1;

        /// <summary>
        /// Team 2's member.
        /// </summary>
        private List<Member> team2;

        /// <summary>
        /// The currentmember
        /// </summary>
        private int currentMember;


        /// <summary>
        /// Start function.
        /// This method will start the procedure of this class.
        /// </summary>
        void Start()
        {
            this.members = new List<Member>();
            this.currentTeam = new List<Member>();
            this.buttonNext.gameObject.SetActive(false);
            this.ReadFromFile();
            this.currentMember = 1;
            this.buttonLeft.gameObject.SetActive(false);
        }

        /// <summary>
        /// Moves camera if necessary.
        /// Makes sure the camera
        /// can't go out of bounds.
        /// </summary>
        void Update()
        {
            if (toMove)
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, endPosition, 25f * Time.deltaTime);

                if (Camera.main.transform.position == endPosition)
                {
                    toMove = false;
                    buttonLeft.enabled = true;
                    buttonRight.enabled = true;
                }
            }

            this.buttonLeft.gameObject.SetActive(currentMember != 1);
            this.buttonRight.gameObject.SetActive(currentMember != this.members.Count);
        }

        /// <summary>
        /// Read the members from the file
        /// and put them in the list.
        /// </summary>
        private void ReadFromFile()
        {
            string path = Application.dataPath + "/Resources/Data.txt";
            StreamReader reader = new StreamReader(path);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] lines = line.Split('-');
                int stamina = Convert.ToInt32(lines[0]);
                int speed = Convert.ToInt32(lines[1]);
                string name = lines[2];

                Member m = new Member();
                m.SetFieldsFromFile(name, stamina, speed);
                members.Add(m);
            }

            reader.Close();
            PutMembersInGame();
        }

        /// <summary>
        /// Put the members in the game which
        /// are in the list.
        /// </summary>
        private void PutMembersInGame()
        {
            Vector3 positionToPlaceMember;
            int currentCharacterNumber = 0;

            foreach (Member m in this.members)
            {
                positionToPlaceMember = new Vector3(currentCharacterNumber * 10, 0, 0);
                Quaternion q = Quaternion.Euler(0, 90, 0);
                GameObject go = (GameObject)GameObject.Instantiate(this.originalGameObject, positionToPlaceMember, q);
                go.name = m.PlayerName;
                go.transform.SetParent(emptyParentObject.transform);

                Member mScript = go.GetComponent<Member>();
                mScript.SetFieldsFromFile(m.PlayerName, m.Stamina, m.Speed);


                SimpleMember simpleMember = go.AddComponent<SimpleMember>();
                simpleMember.csm = this;
                simpleMember.member = mScript;
                simpleMember.SetTextUnderCharacter();

                currentCharacterNumber++;
            }
        }

        /// <summary>
        /// Move the camera to a certain direction.
        /// Sets the currently selected member.
        /// </summary>
        /// <param name="right">Decides if the camera should move to the right or the left</param>
        public void moveCamera(bool right)
        {
            int value = 10;
            currentMember += 1;

            if (!right)
            {
                value *= -1;
                currentMember -= 2;
            }

            Debug.Log("Current member: " + currentMember);

            endPosition = new Vector3(Camera.main.transform.position.x + value, -0.44f, -5);

            toMove = true;
            buttonLeft.enabled = false;
            buttonRight.enabled = false;
        }

        /// <summary>
        /// Add the selectedmember to your team.
        /// </summary>
        public void AddMember(Member memberToAdd)
        {
            Debug.Log("Gonna try to ADD: " + memberToAdd.PlayerName);

            if (!currentTeam.Contains(memberToAdd) && currentTeam.Count < 3)
            {
                this.currentTeam.Add(memberToAdd);
                CheckThreeMembers();
                UpdateTeamLayout();
            }
        }

        /// <summary>
        /// Update the team layout UI elements
        /// in the top-left corner. 
        /// </summary>
        public void UpdateTeamLayout()
        {
            int counter = 1;

            foreach (Member m in this.currentTeam)
            {
                string text = m.PlayerName + " - " + m.Speed.ToString() + " - " + m.Stamina.ToString();

                switch (counter)
                {
                    case 1:
                        textMember1.text = text;
                        break;

                    case 2:
                        textMember2.text = text;
                        break;

                    case 3:
                        textMember3.text = text;
                        break;
                }

                counter++;
            }
        }

        /// <summary>
        /// Reset the layout of the scene.
        /// </summary>
        public void ResetTeamLayout()
        {
            textMember1.text = "Member 1 not assigned";
            textMember2.text = "Member 2 not assigned";
            textMember3.text = "Member 3 not assigned";
            buttonNext.gameObject.SetActive(false);
        }

        /// <summary>
        /// Check if there are 3 members in the game.
        /// If so, enable the next button.
        /// </summary>
        public void CheckThreeMembers()
        {
            if (this.currentTeam.Count == 3)
            {
                this.buttonNext.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Go to the next team.
        /// If both teams are filled,
        /// go to the next scene with 
        /// parameters/file shit.
        /// </summary>
        public void NextTeam()
        {
            if (team1 != null)
            {
                if (currentTeam.Count == 3)
                {
                    team2 = new List<Member>(currentTeam);
                    TeamCollector.SetTeam(team2, 2);
                    Debug.Log("Team 2 has been set with: " + team2.Count + " players");
                    currentTeam.Clear();
                    SceneManager.LoadScene(2);
                }
            }

            else
            {
                team1 = new List<Member>(currentTeam);
                Debug.Log("Team 1 has been set with: " + team1.Count + " players");
                TeamCollector.SetTeam(team1, 1);
                currentTeam.Clear();
                ResetTeamLayout();
            }
        }
    }
}