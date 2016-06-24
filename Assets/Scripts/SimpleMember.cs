using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

/// <summary>
/// Member which is used for character selection.
/// </summary>
public class SimpleMember : MonoBehaviour
{
    /// <summary>
    /// The member which corresponds with this SimpleMember.
    /// </summary>
    public Member member;

    /// <summary>
    /// A boolean which says if the update method should
    /// change position of the UI elements.
    /// </summary>
    private bool updateText;

    /// <summary>
    /// The character selection manager.
    /// This is used to add member to the team.
    /// </summary>
    public CharacterSelectionManager csm;

    //UI ELEMENTS
    private Text textName;
    private Text textSpeed;
    private Text textStamina;
    private Button button;

    /// <summary>
    /// The update method.
    /// This gets called every frame.
    /// It's responsibility in this class is to update
    /// the position of all the UI elements which are
    /// bound to a player.
    /// </summary>
    void Update()
    {
        if (updateText)
        {
            textName.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) - new Vector3(-30, 100, 0);
            textSpeed.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) - new Vector3(-18, 120, 0);
            textStamina.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) - new Vector3(-12, 140, 0);
            button.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) - new Vector3(0, 150, 0);
        }
    }

    /// <summary>
    /// Set the text under the character.
    /// </summary>
    public void SetTextUnderCharacter()
    {
        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        GameObject textParentName = new GameObject();
        textParentName.transform.SetParent(canvas.transform);
        textParentName.name = this.member.PlayerName + " Name";

        this.textName = textParentName.AddComponent<Text>();
        textName.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        textName.name = "Text Name from: " + this.member.PlayerName;
        textName.color = Color.black;
        textName.text = this.member.PlayerName;
        textName.transform.position = new Vector3(-32, 100, 0); // Camera.main.WorldToScreenPoint(this.member.transform.position) - new Vector3(-32, 100, 0);


        GameObject textParentSpeed = new GameObject();
        textParentSpeed.transform.SetParent(canvas.transform);
        textParentSpeed.name = this.member.PlayerName + " Speed";


        this.textSpeed = textParentSpeed.AddComponent<Text>();
        textSpeed.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        textSpeed.name = "Text Speed from: " + this.member.PlayerName;
        textSpeed.text = "Speed: " + this.member.Speed.ToString();
        textSpeed.transform.position = new Vector3(-18, 120, 0); // Camera.main.WorldToScreenPoint(this.member.transform.position) - new Vector3(-18, 120, 0);
        textSpeed.color = Color.black;

        GameObject textParentStamina = new GameObject();
        textParentStamina.transform.SetParent(canvas.transform);
        textParentStamina.name = this.member.PlayerName + " Stamina";


        this.textStamina = textParentStamina.AddComponent<Text>();
        textStamina.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        textStamina.name = "Text Stamina from: " + this.member.PlayerName;
        textStamina.text = "Stamina: " + this.member.Stamina.ToString();
        textStamina.transform.position = new Vector3(-12, 140, 0);  //Camera.main.WorldToScreenPoint(this.member.transform.position) - new Vector3(-12, 140, 0);
        textStamina.color = Color.black;


        GameObject buttonParentSelect = new GameObject();
        buttonParentSelect.transform.SetParent(canvas.transform);
        buttonParentSelect.name = this.member.PlayerName + " Select";

        GameObject btn;
        btn = Instantiate(Resources.Load("Button") as GameObject);
        Button button = btn.GetComponent<Button>();
        button.onClick.AddListener(() => csm.AddMember(this.member.GetData()));
        button.transform.SetParent(canvas.transform);
        button.name = "Select button from: " + this.member.PlayerName;
        button.transform.position = new Vector3(0, 150, 0); //Camera.main.WorldToScreenPoint(this.member.transform.position) - new Vector3(0, 150, 0);

        this.button = button;
        
        updateText = true;
    }
}
