using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class DialogueEntry
{
    public int LineKey;
    public string Color;
    public string DotAnim;
    public string Character;
    public string Type;
    public string Dialogue;
    public string EngText;
    public string NextLineKey;
    public string DeathNote;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DotTextUI;
    public TextMeshProUGUI PlayTextUI;
    public TextMeshProUGUI InputTextUI;
    public GameObject DotPanel;
    public GameObject PlayPanel;
    public GameObject InputPanel;
    public GameObject selectionPanel;
    public GameObject checkbox3Panel;
    public GameObject checkbox4Panel;
    public Button nextButton;

    public List<DialogueEntry> dialogueEntries;
    public int dialogueIndex = 0;

    void Start()
    {
        InitializePanels();
    }

    string ApplyLineBreaks(string text)
    {
        return text.Replace(@"\n", "\n");
    }

    void InitializePanels()
    {
        DotPanel = Instantiate(Resources.Load("DotBalloon") as GameObject, this.transform);
        DotTextUI = DotPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        DotPanel.SetActive(false);

        PlayPanel = Instantiate(Resources.Load("PlayerOneLineBallum") as GameObject, this.transform);
        PlayTextUI = PlayPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        PlayPanel.SetActive(false);

        InputPanel = Instantiate(Resources.Load("InputPlayerOpinion") as GameObject, this.transform);
        InputTextUI = InputPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        InputPanel.SetActive(false);

        checkbox3Panel = Instantiate(Resources.Load("CheckBox3Selection") as GameObject, this.transform);
        checkbox3Panel.SetActive(false);

        checkbox4Panel = Instantiate(Resources.Load("CheckBox4Selection") as GameObject, this.transform);
        checkbox4Panel.SetActive(false);

        selectionPanel = Instantiate(Resources.Load("TwoSelectionBallum") as GameObject, this.transform);
        selectionPanel.SetActive(false);
    }

    public void StartDialogue(string fileName)
    {
        LoadDialogue(fileName);
        ShowNextDialogue();
    }

    void LoadDialogue(string fileName)
    {
        dialogueEntries = new List<DialogueEntry>();
        TextAsset dialogueData = Resources.Load<TextAsset>(fileName);

        if (dialogueData != null)
        {
            Debug.Log("Dialogue file loaded successfully.");
            string[] lines = dialogueData.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = ParseCSVLine(line);
                if (parts.Length >= 9)
                {
                    DialogueEntry entry = new DialogueEntry
                    {
                        LineKey = int.Parse(parts[0]),
                        Color = parts[1],
                        Character = parts[2],
                        DotAnim = parts[3],
                        Type = parts[4],
                        Dialogue = ApplyLineBreaks(parts[5]),
                        EngText = ApplyLineBreaks(parts[6]),
                        NextLineKey = parts[7],
                        DeathNote = parts[8]
                    };
                    dialogueEntries.Add(entry);
                }
            }
        }
        else
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
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
                result.Add(value);
                value = "";
            }
            else
            {
                value += c;
            }
        }

        if (!string.IsNullOrEmpty(value))
        {
            result.Add(value);
        }
        return result.ToArray();
    }
    void ShowNextDialogue()
    {
        DotPanel.SetActive(false);
        PlayPanel.SetActive(false);
        selectionPanel.SetActive(false);
        InputPanel.SetActive(false);
        checkbox3Panel.SetActive(false);
        checkbox4Panel.SetActive(false);

        if (dialogueIndex < dialogueEntries.Count)
        {
            DialogueEntry entry = dialogueEntries[dialogueIndex];

            switch (entry.Type)
            {
                case "text":
                    if (entry.Character == "Dot")
                    {
                        DotPanel.SetActive(true);
                        DotTextUI.text = $"{entry.Character}: {entry.Dialogue}";
                        RegisterNextButton(DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                    }
                    else if (entry.Character == "Player")
                    {
                        PlayPanel.SetActive(true);
                        PlayTextUI.text = $"{entry.Character}: {entry.Dialogue}";
                        RegisterNextButton(PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                    }
                    break;
                case "selection":
                    selectionPanel.SetActive(true);
                    ShowSelection(entry.Dialogue);
                    break;
                case "textbox":
                    InputPanel.SetActive(true);
                    InputTextUI.text = entry.Dialogue;
                    RegisterNextButton(InputPanel.transform.GetChild(1).GetComponent<Button>());
                    break;
                case "checkbox3":
                    checkbox3Panel.SetActive(true);
                    ShowCheckboxOptions(checkbox3Panel, entry.Dialogue);
                    RegisterNextButton(checkbox3Panel.transform.GetChild(1).GetComponent<Button>());
                    break;
                case "checkbox4":
                    checkbox4Panel.SetActive(true);
                    ShowCheckboxOptions(checkbox4Panel, entry.Dialogue);
                    RegisterNextButton(checkbox4Panel.transform.GetChild(1).GetComponent<Button>());
                    break;
            }
        }
    }

    void RegisterNextButton(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => nextbut());
    }

    void ShowSelection(string options)
    {
        string[] selections = options.Split('|');
        for (int i = 0; i < selections.Length; i++)
        {
            Button button = selectionPanel.transform.GetChild(i).GetComponent<Button>();
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

    void nextbut()
    {
        dialogueIndex++;
        ShowNextDialogue();
    }

    void OnSelectionClicked(int index)
    {
        selectionPanel.SetActive(false);
        dialogueIndex++;
        ShowNextDialogue();
    }
}
