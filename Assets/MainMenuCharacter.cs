using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class MainMenuCharacter : MonoBehaviour
{


    public Text NameText;
    public Text StaminaText;
    public Text SpeedText;

    public void SetCharacter(MemberData memberData)
    {
        NameText.gameObject.SetActive(true);
        StaminaText.gameObject.SetActive(true);
        SpeedText.gameObject.SetActive(true);

        NameText.text = memberData.Name;
        StaminaText.text = "Stamina: " + memberData.Stamina.ToString();
        SpeedText.text = "Speed: " + memberData.Speed.ToString();
    }

}
