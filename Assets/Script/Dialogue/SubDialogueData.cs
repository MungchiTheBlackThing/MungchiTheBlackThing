using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDialogueData : MonoBehaviour
{
    public string eventId;
    public List<Dictionary<string, string>> scriptData;

    public TMPro.TMP_InputField inputEventID;
    public TMPro.TextMeshProUGUI korText;
    public TMPro.TextMeshProUGUI type;

    private int currentTextIndex = 0;

    private void Start()
    {
        scriptData = CSVParser.instance.ParseCSVFile_EventID("");
        OnCurrentText();
    }

    public void OnSetEventID()
    {
        scriptData = CSVParser.instance.ParseCSVFile_EventID(inputEventID.text);
        currentTextIndex = 0;
        OnCurrentText();
    }

    public void OnCurrentText()
    {
        korText.text = scriptData[currentTextIndex]["Kor Text"];
        type.text = scriptData[currentTextIndex]["Type"];
        Debug.Log(korText.text);
    }

    public void OnNextText()
    {
        currentTextIndex++;
        korText.text = scriptData[currentTextIndex]["Kor Text"];
        type.text = scriptData[currentTextIndex]["Type"];
    }
}
