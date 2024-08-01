using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using Assets.Script.DialClass;
using Assets.Script.DialLanguage;

public class DialogueManager : MonoBehaviour
{
    // UI elements
    [SerializeField] TextMeshProUGUI DotTextUI;
    [SerializeField] TextMeshProUGUI PlayTextUI;
    [SerializeField] TextMeshProUGUI InputTextUI;
    [SerializeField] GameObject DotPanel;
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject InputPanel;
    [SerializeField] GameObject SelectionPanel;
    [SerializeField] GameObject Checkbox3Panel;
    [SerializeField] GameObject Checkbox4Panel;
    [SerializeField] Button NextButton;

    // Reference to the player controller
    [SerializeField] PlayerController PlayerController;

    // Lists to store dialogue entries
    [SerializeField] List<DialogueEntry> DialogueEntries;
    [SerializeField] List<SubDialogueEntry> SubDialogueEntries;

    public List<object> currentDialogueList;  // Current dialogue list
    public int dialogueIndex = 0;  // Current dialogue index
    public int Day = 0;  // Current day

    // Language setting
    [SerializeField]
    public LanguageType CurrentLanguage = LanguageType.Kor;

    void Start()
    {
        Day = PlayerController.GetChapter();
        Debug.Log("대화 날짜: " + Day);
        InitializePanels();
        Debug.Log("패널 초기화");
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
        if (DialogueEntries != null)
        {
            DialogueEntries.Clear();
        }
        if (SubDialogueEntries != null)
        {
            SubDialogueEntries.Clear();
        }
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

        if (fileName == "main_test")
        {
            Debug.Log("Loading main dialogue");
            LoadMainDialogue(lines);
        }
        else if (fileName == "sub_test")
        {
            Debug.Log("Loading sub dialogue");
            LoadSubDialogue(lines);
        }
        ShowNextDialogue();
    }

    void LoadMainDialogue(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] parts = ParseCSVLine(line);
            Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

            if (parts.Length >= 14)
            {
                int scriptKey = int.Parse(parts[0]);
                if (scriptKey == Day)
                {
                    DialogueEntry entry = new DialogueEntry
                    {
                        ScriptKey = scriptKey,
                        LineKey = int.Parse(parts[1]),
                        Background = parts[2],
                        Actor = parts[3],
                        AnimState = parts[4],
                        DotBody = parts[5],
                        DotExpression = parts[6],
                        TextType = parts[7],
                        KorText = ApplyLineBreaks(parts[8]),
                        EngText = ApplyLineBreaks(parts[9]),
                        NextLineKey = parts[10],
                        AnimScene = parts[11],
                        AfterScript = parts[12],
                        Deathnote = parts[13]
                    };

                    string displayedText = CurrentLanguage == LanguageType.Kor ? entry.KorText : entry.EngText;
                    entry.KorText = displayedText;

                    DialogueEntries.Add(entry);
                    currentDialogueList.Add(entry);

                    Debug.Log($"Added DialogueEntry: {displayedText}");
                }
            }
            else
            {
                Debug.LogError($"Line {i} does not have enough parts: {line}");
            }
        }
    }

    void LoadSubDialogue(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] parts = ParseCSVLine(line);
            Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

            if (parts.Length >= 12)
            {
                int scriptKey = int.Parse(parts[0]);
                if (scriptKey == Day)
                {
                    SubDialogueEntry entry = new SubDialogueEntry
                    {
                        ScriptKey = scriptKey,
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
                    };

                    string displayedText = CurrentLanguage == LanguageType.Kor ? entry.KorText : entry.EngText;
                    entry.KorText = displayedText;

                    SubDialogueEntries.Add(entry);
                    currentDialogueList.Add(entry);

                    Debug.Log($"Added SubDialogueEntry: {displayedText}");
                }
            }
            else
            {
                Debug.LogError($"Line {i} does not have enough parts: {line}");
            }
        }
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
        PanelOff();

        if (dialogueIndex >= currentDialogueList.Count)
        {
            DialEnd();
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

    void OnSelectionClicked(int index)
    {
        var currentEntry = currentDialogueList[dialogueIndex] as DialogueEntry;
        if (currentEntry != null)
        {
            Debug.Log($"Current LineKey: {currentEntry.LineKey}");
            string[] nextKeys = currentEntry.NextLineKey.Split('|');
            int nextLineKey = int.Parse(nextKeys[index]);
            Debug.Log($"Next LineKey: {nextLineKey}");
            dialogueIndex = currentDialogueList.FindIndex(entry => (entry as DialogueEntry)?.LineKey == nextLineKey);
        }

        SelectionPanel.SetActive(false);
        ShowNextDialogue();
    }

    void NextDialogue()
    {
        var currentEntry = currentDialogueList[dialogueIndex] as DialogueEntry;
        if (currentEntry != null)
        {
            if (currentEntry.TextType == "selection")
            {
                Debug.Log($"Current LineKey: {currentEntry.LineKey}");
                return;
            }

            if (int.TryParse(currentEntry.NextLineKey, out int nextLineKey))
            {
                Debug.Log($"Current LineKey: {currentEntry.LineKey}");
                Debug.Log($"Next LineKey: {nextLineKey}");
                dialogueIndex = currentDialogueList.FindIndex(entry => (entry as DialogueEntry)?.LineKey == nextLineKey);
            }
            else
            {
                dialogueIndex++;
            }
        }

        ShowNextDialogue();
    }

    public void DialEnd()
    {
        Debug.Log("Dialogue ended.");
        PanelOff();
        currentDialogueList.Clear();
        dialogueIndex = 0;
    }

    void PanelOff()
    {
        DotPanel.SetActive(false);
        PlayPanel.SetActive(false);
        SelectionPanel.SetActive(false);
        InputPanel.SetActive(false);
        Checkbox3Panel.SetActive(false);
        Checkbox4Panel.SetActive(false);
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
        button.onClick.AddListener(NextDialogue);
    }
}
