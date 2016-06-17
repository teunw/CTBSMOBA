using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject FirstMenu;
    public GameObject DataImport;

    public InputField NewCharNameField;
    public InputField TotalDistanceField;
    public InputField AverageSpeedField;
    public InputField NrOfSprintsField;
    public InputField AvgSprintDistanceField;

    public Dropdown CharacterDropdownList;

    public GameObject PopUpField;
    public Text PopUpText;

    public List<string> CharacterList = new List<string>();
    string[] CharacterDataArray; //Speed,stamina,name
    public List<string[]> CharacterDataList = new List<string[]>();

    int Speed;
    int Stamina;

    string FilePath;

    float TotalDistance;
    float AverageSpeed;
    float NrOfSprints;
    float AvgSprintDistance;
    string CharacterName;

    bool ModifyChar = false;

    // Use this for initialization
    void Start()
    {
        //Maybe change this to Application.persistentDataPath for mobile?        
        FilePath = Application.dataPath + "/Resources/Data.txt";
    }

    public void PlayGame()
    {
        //TODO: Load character selection 
        SceneManager.LoadScene("Character Selection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToInputScreen()
    {
        DataImport.SetActive(true);
        FirstMenu.SetActive(false);

        LoadAndReadFile(FilePath);
        AddToCharList();
    }

    public void ImportAndConvertToStats()
    {
        SetAllInputs();

        if (!CheckInputs())
        {
            return;
        }
        foreach (string[] s in CharacterDataList)
        {
            if (s[2].ToString() == CharacterName && !NewCharacter)
            {
                CharacterDataArray = s;
                ModifyChar = true;
            }
            else if (s[2].ToString() == CharacterName && NewCharacter)
            {
                StartCoroutine(ShowPopup("This character already exists", 2));
                return;
            }
        }

        if (NewCharacter)
        {
            ModifyChar = true;
        }

        CalculateStats();
        if (ModifyChar && !NewCharacter)
        {
            CharacterDataArray = new string[3];
            CharacterDataArray[0] = Speed.ToString();
            CharacterDataArray[1] = Stamina.ToString();
            Debug.Log(Speed);
        }
        else if (ModifyChar && NewCharacter)
        {
            MakeNewCharacter(CharacterName, Speed, Stamina);
        }

        Speed = 0;
        Stamina = 0;
        ModifyChar = false;

        AddToCharList();
        ResetInputFields();
        ExportCharacterData(FilePath);
    }

    public void GoBack()
    {
        FirstMenu.SetActive(true);
        DataImport.SetActive(false);
    }

    public void LoadCharacters()
    {
        CharacterDropdownList.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        Dropdown.OptionData NewCharacter = new Dropdown.OptionData();
        NewCharacter.text = "New Character";
        options.Add(NewCharacter);

        foreach (string characterName in CharacterList)
        {
            Dropdown.OptionData temp = new Dropdown.OptionData();
            temp.text = characterName;
            options.Add(temp);
        }
        CharacterDropdownList.AddOptions(options);
    }

    public void LoadAndReadFile(string filePath)
    {
        StreamReader File = new StreamReader(filePath);

        while (!File.EndOfStream)
        {
            string s = File.ReadLine();
            string[] ss = s.Split('-');

            CharacterDataArray = new string[3];
            CharacterDataArray[0] = ss[0];
            CharacterDataArray[1] = ss[1];
            CharacterDataArray[2] = ss[2];
            CharacterDataList.Add(CharacterDataArray);
        }

        File.Close();
    }

    public bool NewCharacter
    {
        get
        {
            if (CharacterDropdownList.value == 0)
            {
                return true;
            }
            return false;
        }
    }

    public void ChangeNewCharacterInput()
    {
        NewCharNameField.gameObject.SetActive(NewCharacter);
    }

    public void MakeNewCharacter(string charName, int speed, int stamina)
    {
        CharacterDataArray = new string[3];
        CharacterDataArray[0] = speed.ToString();
        CharacterDataArray[1] = stamina.ToString();
        CharacterDataArray[2] = charName;
        CharacterDataList.Add(CharacterDataArray);
    }

    public void ResetInputFields()
    {
        NewCharNameField.text = "";
        TotalDistanceField.text = "0";
        AverageSpeedField.text = "0";
        NrOfSprintsField.text = "0";
        AvgSprintDistanceField.text = "0";
        CharacterDropdownList.value = 0;
    }

    public void ExportCharacterData(string filePath)
    {
        StreamWriter File = new StreamWriter(filePath);

        foreach (string[] s in CharacterDataList)
        {
            File.WriteLine(s[0] + "-" + s[1] + "-" + s[2]);
        }
        File.Flush();
        File.Close();
    }

    public void AddToCharList()
    {
        CharacterList.Clear();
        foreach (string[] s in CharacterDataList)
        {
            CharacterList.Add(s[2]);
        }
        LoadCharacters();
    }

    public void SetAllInputs()
    {
        TotalDistance = float.Parse(TotalDistanceField.text);
        AverageSpeed = float.Parse(AverageSpeedField.text);
        NrOfSprints = float.Parse(NrOfSprintsField.text);
        AvgSprintDistance = float.Parse(AverageSpeedField.text);
        if (NewCharacter)
        {
            CharacterName = NewCharNameField.text;
        }
        else
        {
            Dropdown.OptionData[] ods = CharacterDropdownList.options.ToArray();
            CharacterName = ods[CharacterDropdownList.value].text;
        }
    }

    IEnumerator ShowPopup(string message, float delay)
    {
        PopUpText.text = message;
        PopUpField.SetActive(true);
        yield return new WaitForSeconds(delay);
        PopUpField.SetActive(false);
    }

    public bool CheckInputs()
    {
        if (NewCharNameField.text.Length < 3 && NewCharacter)
        {
            StartCoroutine(ShowPopup("Character name must be longer than 3 characters", 2));
            return false;
        }
        return true;
    }

    public void CalculateStats()
    {
        Speed = (int)(AverageSpeed * 2.9f * (1 +( Mathf.Log10(TotalDistance/1000) / 10)));
        Stamina = (int)(AvgSprintDistance * (1.9f + (Mathf.Log10(NrOfSprints) / 10)));

        if (Speed < 0)
        {
            Speed = 0;
        }
        if (Stamina < 0)
        {
            Stamina = 0;
        }
        if(Speed > 100)
        {
            Speed = 100;
        }
        if (Stamina > 100)
        {
            Stamina = 100;
        }
    }
}
