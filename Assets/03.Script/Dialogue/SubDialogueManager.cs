using Assets.Script.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDialogueManager : MonoBehaviour
{
    public static SubDialogueManager instance;

    public string eventID;
    public List<Dictionary<string, string>> scriptData;

    public TMPro.TMP_InputField inputEventID;
    public TMPro.TextMeshProUGUI scriptText;
    public TMPro.TextMeshProUGUI type;
    private int currentTextIndex = 0;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        if (inputEventID == null || scriptText == null || type == null) Debug.LogError("오브젝트가 매핑되지 않았습니다.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateScriptUI();
        }
    }

    public void SetEventID(string eventID)
    {
        this.eventID = eventID;
        scriptData = CSVParser.instance.ParseCSVFile_EventID(inputEventID.text);
        currentTextIndex = 0;
    }
    public string GetCurrentScript(LANGUAGE _language)
    {
        string scriptType = scriptData[currentTextIndex]["Type"];
        if(scriptType == Enum.GetName(typeof(SCRIPT_TYPE), SCRIPT_TYPE.TEXT))
        {
            string columnName = CSVOption.languageMappings[CSVOption.GetLanguage()][SCRIPT_TYPE.TEXT];
            return scriptData[currentTextIndex][columnName];
        }
        else if(scriptType == Enum.GetName(typeof(SCRIPT_TYPE), SCRIPT_TYPE.CHOICE))
        {
            string columnName = CSVOption.languageMappings[CSVOption.GetLanguage()][SCRIPT_TYPE.CHOICE];
            return scriptData[currentTextIndex][columnName];
        }
        else Debug.Log("Wrong script type");
        return null;
    }
    public void UpdateScriptUI()
    {
        var objects = FindObjectsOfType<TextScript>();
        foreach (var obj in objects)
        {
            obj.GetComponent<TextScript>().SetText();
        }
    }
    public void GoNextScript()
    {
        currentTextIndex++;
        UpdateScriptUI();
    }
    // --- TEST --- 

    public void TEST_ChangeLanguage()
    {
        if(CSVOption.GetLanguage() == LANGUAGE.ENGLISH)
            CSVOption.SetLanguage(LANGUAGE.KOREAN);
        else if (CSVOption.GetLanguage() == LANGUAGE.KOREAN)
            CSVOption.SetLanguage(LANGUAGE.ENGLISH);
        UpdateScriptUI();
    }

    public void OnSetEventID()
    {
        scriptData = CSVParser.instance.ParseCSVFile_EventID(inputEventID.text);
        currentTextIndex = 0;
        OnCurrentText();
    }

    public void OnCurrentText()
    {
        scriptText.text = scriptData[currentTextIndex]["Kor Text"];
        type.text = scriptData[currentTextIndex]["Type"];
        Debug.Log(scriptText.text);
    }

    public void OnNextText()
    {
        currentTextIndex++;
        scriptText.text = scriptData[currentTextIndex]["Kor Text"];
        type.text = scriptData[currentTextIndex]["Type"];
    }
}
