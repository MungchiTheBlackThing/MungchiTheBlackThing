using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class DialogueEntry
{
    public int ScriptKey;
    public int LineKey;
    public string Background;
    public string DotAnim;
    public string Actor;
    public string AnimState;
    public string DotBody;
    public string DotExpression;
    public string TextType;
    public string KorText;
    public string EngText;
    public string NextLineKey;
    public string AnimScene;
    public string AfterScript;
    public string Deathnote;
}

public class SubDialogueEntry
{
    public int ScriptKey;
    public int LineKey;
    public string Color;
    public string Actor;
    public string AnimState;
    public string DotAnim;
    public string TextType;
    public string KorText;
    public string EngText;
    public string NextLineKey;
    public string Deathnote;
    public string AfterScript;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DotTextUI;
    public TextMeshProUGUI PlayTextUI;
    public TextMeshProUGUI InputTextUI;
    public GameObject DotPanel;
    public GameObject PlayPanel;
    public GameObject InputPanel;
    public GameObject SelectionPanel;
    public GameObject Checkbox3Panel;
    public GameObject Checkbox4Panel;
    public Button NextButton;

    public List<DialogueEntry> DialogueEntries;
    public List<SubDialogueEntry> SubDialogueEntries;
    private List<object> currentDialogueList;
    private int dialogueIndex = 0;

    void Start()
    {
        InitializePanels();
    }

    void InitializePanels()
    {
        DotPanel = Instantiate(Resources.Load("DotBalloon") as GameObject, transform);
        DotTextUI = DotPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        DotPanel.SetActive(false);

        PlayPanel = Instantiate(Resources.Load("PlayerOneLineBallum") as GameObject, transform);
        PlayTextUI = PlayPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        PlayPanel.SetActive(false);

        InputPanel = Instantiate(Resources.Load("InputPlayerOpinion") as GameObject, transform);
        InputTextUI = InputPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        InputPanel.SetActive(false);

        Checkbox3Panel = Instantiate(Resources.Load("CheckBox3Selection") as GameObject, transform);
        Checkbox3Panel.SetActive(false);

        Checkbox4Panel = Instantiate(Resources.Load("CheckBox4Selection") as GameObject, transform);
        Checkbox4Panel.SetActive(false);

        SelectionPanel = Instantiate(Resources.Load("TwoSelectionBallum") as GameObject, transform);
        SelectionPanel.SetActive(false);
    }

    public void StartDialogue(string fileName)
    {
        LoadDialogue(fileName);
    }

    void LoadDialogue(string fileName)
    {
        TextAsset dialogueData = Resources.Load<TextAsset>(fileName);

        if (dialogueData == null)
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
            return;
        }

        Debug.Log("Dialogue file loaded successfully.");
        string[] lines = dialogueData.text.Split('\n');

        // Clear previous dialogue entries
        DialogueEntries.Clear();
        SubDialogueEntries.Clear();
        currentDialogueList = new List<object>();

        if (fileName == "main")
        {
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = ParseCSVLine(line);
                if (parts.Length >= 14)
                {
                    DialogueEntries.Add(new DialogueEntry
                    {
                        ScriptKey = int.Parse(parts[0]),
                        LineKey = int.Parse(parts[1]),
                        Background = parts[2],
                        DotAnim = parts[3],
                        Actor = parts[4],
                        AnimState = parts[5],
                        DotBody = parts[6],
                        DotExpression = parts[7],
                        TextType = parts[8],
                        KorText = ApplyLineBreaks(parts[9]),
                        EngText = ApplyLineBreaks(parts[10]),
                        NextLineKey = parts[11],
                        AnimScene = parts[12],
                        AfterScript = parts[13],
                        Deathnote = parts[14]
                    });
                    currentDialogueList.Add(DialogueEntries[DialogueEntries.Count - 1]);
                }
            }
        }
        else if (fileName == "sub")
        {
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = ParseCSVLine(line);
                if (parts.Length >= 12)
                {
                    SubDialogueEntries.Add(new SubDialogueEntry
                    {
                        ScriptKey = int.Parse(parts[0]),
                        LineKey = int.Parse(parts[1]),
                        Color = parts[2],
                        Actor = parts[3],
                        AnimState = parts[4],
                        DotAnim = parts[5],
                        TextType = parts[6],
                        KorText = ApplyLineBreaks(parts[7]),
                        EngText = ApplyLineBreaks(parts[8]),
                        NextLineKey = parts[9],
                        Deathnote = parts[10],
                        AfterScript = parts[11]
                    });
                    currentDialogueList.Add(SubDialogueEntries[SubDialogueEntries.Count - 1]);
                }
            }
        }
        ShowNextDialogue();
    }

    string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string value = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value.Trim());
                value = "";
            }
            else
            {
                value += c;
            }
        }

        if (!string.IsNullOrEmpty(value))
        {
            result.Add(value.Trim());
        }
        return result.ToArray();
    }

    string ApplyLineBreaks(string text)
    {
        return text.Replace(@"\n", "\n");
    }

    void ShowNextDialogue()
    {
        DotPanel.SetActive(false);
        PlayPanel.SetActive(false);
        SelectionPanel.SetActive(false);
        InputPanel.SetActive(false);
        Checkbox3Panel.SetActive(false);
        Checkbox4Panel.SetActive(false);

        if (dialogueIndex >= currentDialogueList.Count)
        {
            Debug.Log("Dialogue ended.");
            currentDialogueList.Clear();  // Clear the list for the next dialogue session
            dialogueIndex = 0;  // Reset index for the next session
            return;
        }

        var entry = currentDialogueList[dialogueIndex];
        string textType = GetTextType(entry);
        string actor = GetActor(entry);
        string korText = GetKorText(entry);

        switch (textType)
        {
            case "text":
                if (actor == "Dot")
                {
                    DotPanel.SetActive(true);
                    DotTextUI.text = $"{actor}: {korText}";
                    RegisterNextButton(DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                }
                else if (actor == "Player")
                {
                    PlayPanel.SetActive(true);
                    PlayTextUI.text = $"{actor}: {korText}";
                    RegisterNextButton(PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                }
                break;
            case "selection":
                SelectionPanel.SetActive(true);
                ShowSelection(korText);
                break;
            case "textbox":
                InputPanel.SetActive(true);
                InputTextUI.text = korText;
                RegisterNextButton(InputPanel.transform.GetChild(1).GetComponent<Button>());
                break;
            case "checkbox3":
                Checkbox3Panel.SetActive(true);
                ShowCheckboxOptions(Checkbox3Panel, korText);
                RegisterNextButton(Checkbox3Panel.transform.GetChild(1).GetComponent<Button>());
                break;
            case "checkbox4":
                Checkbox4Panel.SetActive(true);
                ShowCheckboxOptions(Checkbox4Panel, korText);
                RegisterNextButton(Checkbox4Panel.transform.GetChild(1).GetComponent<Button>());
                break;
        }
    }

    string GetTextType(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).TextType;
        if (entry is SubDialogueEntry)
            return (entry as SubDialogueEntry).TextType;
        return "";
    }

    string GetKorText(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).KorText;
        if (entry is SubDialogueEntry)
            return (entry as SubDialogueEntry).KorText;
        return "";
    }

    string GetActor(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).Actor;
        if (entry is SubDialogueEntry)
            return (entry as SubDialogueEntry).Actor;
        return "";
    }

    void RegisterNextButton(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => NextDialogue());
    }

    void ShowSelection(string options)
    {
        string[] selections = options.Split('|');
        for (int i = 0; i < selections.Length; i++)
        {
            Button button = SelectionPanel.transform.GetChild(i).GetComponent<Button>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = selections[i];
            int index = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnSelectionClicked(index));
        }
    }

    void ShowCheckboxOptions(GameObject checkboxPanel, string options)
    {
        string[] selections = options.Split('|');
        for (int i = 0; i < selections.Length; i++)
        {
            TextMeshProUGUI text = checkboxPanel.transform.GetChild(2).GetChild(0).GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            text.text = selections[i];
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;
        ShowNextDialogue();
    }

    void OnSelectionClicked(int index)
    {
        SelectionPanel.SetActive(false);
        dialogueIndex++;
        ShowNextDialogue();
    }
}
