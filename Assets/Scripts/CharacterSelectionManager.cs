#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#endregion
using System.IO;
using System.Net;
using SimpleJSON;
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
        public List<MemberData> members;

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
        private List<MemberData> currentTeam;

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
        ///  The images for members 1 to 3
        /// </summary>
        public Image imageMember1;
        public Image imageMember2;
        public Image imageMember3;
        
        /// <summary>
        /// Sprite used for characters
        /// </summary>
        public Sprite characterImage;

        /// <summary>
        /// Team number of team currently choosing characters
        /// </summary>
        private int teamNumber;

        /// <summary>
        /// Text for team currently choosing
        /// </summary>
        public Text teamName;

        /// <summary>
        /// Panel for team color
        /// </summary>
        public GameObject teamPanel;

        /// <summary>
        /// Button to go to the next scene.
        /// </summary>
        public Button buttonNext;

        /// <summary>
        /// Team 1's member.
        /// </summary>
        private List<MemberData> team1;

        /// <summary>
        /// Team 2's member.
        /// </summary>
        private List<MemberData> team2;

        /// <summary>
        /// The currentmember
        /// </summary>
        private int currentMember;

        private WWW www;

        /// <summary>
        /// Start function.
        /// This method will start the procedure of this class.
        /// </summary>
        void Start()
        {
            this.members = new List<MemberData>();
            this.currentTeam = new List<MemberData>();
            this.buttonNext.gameObject.SetActive(false);
            this.currentMember = 1;
            this.buttonLeft.gameObject.SetActive(false);
            this.teamNumber = 1;
            this.SetPickingTeam();

            HttpWebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create("https://dashcap.teunwillems.nl/data");
                response = (HttpWebResponse) request.GetResponse();
            }
            catch (Exception e)
            {
                AddMandatoryCharacters();
                PutMembersInGame();
                return;
            }

            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();


            JSONNode node = JSON.Parse(responseFromServer);
            JSONArray array = node["characters"].AsArray;
            IEnumerator enumerator = array.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                JSONClass currentNode = JSON.Parse(current.ToString()).AsObject;

                MemberData md = new MemberData()
                {
                    Name = currentNode["name"].Value,
                    Speed = (int) Convert.ToDouble(currentNode["speed"].Value),
                    Stamina = (int) Convert.ToDouble(currentNode["stamina"].Value)
                };

                members.Add(md);
                Debug.Log(md);
            }
            PutMembersInGame();
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

            if (Camera.main.transform.position.x % 10 == 0)
            {
               
                if (Input.GetKeyDown(KeyCode.RightArrow) && currentMember != this.members.Count())
                {
                    moveCamera(true);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) && currentMember != 1)
                {
                    moveCamera(false);
                }
            }
        }

        /// <summary>
        /// Read the members from the file
        /// and put them in the list.
        /// </summary>
        private void ReadFromFile()
        {
            string path = Application.dataPath + "/Resources/Data2.txt";

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] lines = line.Split('-');
                    int stamina = Convert.ToInt32(lines[0]);
                    int speed = Convert.ToInt32(lines[1]);
                    string name = lines[2];

                    MemberData m = new MemberData()
                    {
                        Stamina = stamina,
                        Speed = speed,
                        Name = name
                    };
                    members.Add(m);
                }

                reader.Close();
            }
            catch
            {
            }
            AddMandatoryCharacters();
            PutMembersInGame();
        }

        /// <summary>
        /// Adds characters until at least 3 members are present
        /// </summary>
        public void AddMandatoryCharacters()
        {
            while (members.Count < 3)
            {
                members.Add(new MemberData()
                {
                    Stamina = 25 + members.Count,
                    Speed = 200 - members.Count * 10,
                    Name = "GC_" + members.Count
                });
            }
        }

        /// <summary>
        /// Put the members in the game which
        /// are in the list.
        /// </summary>
        private void PutMembersInGame()
        {
            Vector3 positionToPlaceMember;
            int currentCharacterNumber = 0;

            foreach (MemberData m in this.members)
            {
                positionToPlaceMember = new Vector3(currentCharacterNumber * 10, 0, 0);
                Quaternion q = Quaternion.Euler(0, 90, 0);
                GameObject go = (GameObject)GameObject.Instantiate(this.originalGameObject, positionToPlaceMember, q);
                go.name = m.Name;
                go.transform.SetParent(emptyParentObject.transform);

                Member mScript = go.GetComponent<Member>();
                mScript.SetFieldsFromFile(m);


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

            endPosition = new Vector3(Camera.main.transform.position.x + value, -0.44f, -5);

            toMove = true;
            buttonLeft.enabled = false;
            buttonRight.enabled = false;
        }

        /// <summary>
        /// Add the selectedmember to your team.
        /// </summary>
        public void AddMember(MemberData memberToAdd)
        {
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

            foreach (MemberData m in this.currentTeam)
            {
                string text = m.Name + " - " + m.Speed.ToString() + " - " + m.Stamina.ToString();
                Color teamColor = teamNumber == 1 ? Color.red : Color.blue;

                switch (counter)
                {
                    case 1:
                        imageMember1.sprite = characterImage;
                        imageMember1.color = teamColor;
                        textMember1.text = text;
                        imageMember1.gameObject.SetActive(true);
                        imageMember2.gameObject.SetActive(false);
                        imageMember3.gameObject.SetActive(false);
                        break;

                    case 2:
                        imageMember2.sprite = characterImage;
                        imageMember2.color = teamColor;
                        textMember2.text = text;
                        imageMember2.gameObject.SetActive(true);
                        imageMember3.gameObject.SetActive(false);
                        break;

                    case 3:
                        imageMember3.sprite = characterImage;
                        imageMember3.color = teamColor;
                        textMember3.text = text;
                        imageMember3.gameObject.SetActive(true);
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
            imageMember1.gameObject.SetActive(false);
            imageMember2.gameObject.SetActive(false);
            imageMember3.gameObject.SetActive(false);
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
                    team2 = new List<MemberData>(currentTeam);
                    TeamCollector.FillTeam(team2, 2);
                    Debug.Log("Team 2 has been set with: " + team2.Count + " players");
                    currentTeam.Clear();
                    SceneManager.LoadScene(2);
                }
            }

            else
            {
                team1 = new List<MemberData>(currentTeam);
                Debug.Log("Team 1 has been set with: " + team1.Count + " players");
                TeamCollector.FillTeam(team1, 1);
                currentTeam.Clear();
                ResetTeamLayout();
                teamNumber++;
                SetPickingTeam();
            }
        }

        public void RemoveMemberFromTeam(int teamMemberNr)
        {
            teamMemberNr -= 1;
            currentTeam.Remove(currentTeam[teamMemberNr]);
            UpdateTeamLayout();

            if (currentTeam.Count == 0) imageMember1.gameObject.SetActive(false);
        }

        public void SetPickingTeam()
        {
            if (teamNumber == 1)
            {
                teamPanel.GetComponent<Image>().color = Color.red;
                teamName.text = "Team\nRED\npicks";
            }
            else if(teamNumber == 2)
            {
                teamPanel.GetComponent<Image>().color = Color.blue;
                teamName.text = "Team\nBLUE\npicks";
            }
        }
    }

    public struct MemberData
    {
        private int stamina;
        private int speed;
        private string name;
        private string skill1, skill2;

        public int Stamina { get { return stamina; } set { stamina = value; } }
        public int Speed { get { return speed; } set { speed = value; } }
        public string Name { get { return name; } set { name = value; } }

        public override string ToString()
        {
            return String.Format("{0}-{1}-{2}", stamina, speed, name);
        }
    }
}
