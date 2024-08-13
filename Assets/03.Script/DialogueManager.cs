using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Assets.Script.DialClass;
using Assets.Script.DialLanguage;
using Assets.Script.TimeEnum;
using UnityEngine.XR;
using Unity.VisualScripting;
using System;

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
    [SerializeField] GameObject Selection3Panel;
    [SerializeField] GameObject Selection4Panel;
    [SerializeField] Button NextButton;
    [SerializeField] SkipController SkipController;

    // Reference to the player controller
    [SerializeField] PlayerController PlayerController;

    // Lists to store dialogue entries
    [SerializeField] List<DialogueEntry> DialogueEntries;
    [SerializeField] List<SubDialogueEntry> SubDialogueEntries;

    public List<object> currentDialogueList;  // Current dialogue list
    public int dialogueIndex = 0;  // Current dialogue index
    public int Day = 0;  // Current day

    public int Dial;

    // Language setting
    [SerializeField]
    public LanguageType CurrentLanguage = LanguageType.Kor;


    /*뭉치 애니메이션 동작 확인을 위해서 잠시, 송수영이 추가함.*/
    [SerializeField]
    DotController dot;

    /**/

    void Start()
    {
        InitializePanels();
        Debug.Log("패널 초기화");
    }

    void InitializePanels()
    {
        DotPanel = Instantiate(Resources.Load("DialBalloon/DotBalloon") as GameObject, transform);
        DotTextUI = DotPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        DotPanel.SetActive(false);
        DotPanel.AddComponent<CanvasGroup>();

        PlayPanel = Instantiate(Resources.Load("DialBalloon/PlayerOneLineBallum") as GameObject, transform);
        PlayTextUI = PlayPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        PlayPanel.SetActive(false);
        PlayPanel.AddComponent<CanvasGroup>();

        InputPanel = Instantiate(Resources.Load("DialBalloon/InputPlayerOpinion") as GameObject, transform);
        InputTextUI = InputPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        InputPanel.SetActive(false);
        InputPanel.AddComponent<CanvasGroup>();

        Checkbox3Panel = Instantiate(Resources.Load("DialBalloon/CheckBox3Selection") as GameObject, transform);
        Checkbox3Panel.SetActive(false);
        Checkbox3Panel.AddComponent<CanvasGroup>();

        Checkbox4Panel = Instantiate(Resources.Load("DialBalloon/CheckBox4Selection") as GameObject, transform);
        Checkbox4Panel.SetActive(false);
        Checkbox4Panel.AddComponent <CanvasGroup>();

        SelectionPanel = Instantiate(Resources.Load("DialBalloon/TwoSelectionBallum") as GameObject, transform);
        SelectionPanel.SetActive(false);
        SelectionPanel.AddComponent<CanvasGroup>();

        Selection3Panel = Instantiate(Resources.Load("DialBalloon/Selection3Selection") as GameObject, transform);
        Selection3Panel.SetActive(false);
        Selection3Panel.AddComponent<CanvasGroup>();

        Selection4Panel = Instantiate(Resources.Load("DialBalloon/Selection4Selection") as GameObject, transform);
        Selection4Panel.SetActive(false);
        Selection4Panel.AddComponent<CanvasGroup>();
    }

    public void StartDialogue(string fileName)
    {
        Dial = SkipController.GetTimeCurIdx;
        Day = PlayerController.GetChapter();
        Debug.Log("대화 날짜: " + Day);
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
        TextAsset dialogueData = Resources.Load<TextAsset>("MainDial/" + fileName);

        if (dialogueData == null)
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
            return;
        }
        Debug.Log(fileName);
        Debug.Log("Dialogue file loaded successfully.");
        string[] lines = dialogueData.text.Split('\n');

        // Clear previous dialogue entries
        DialogueEntries.Clear();
        SubDialogueEntries.Clear();
        currentDialogueList = new List<object>();

        if (fileName.Contains("main"))
        {
            Debug.Log("Loading main dialogue");
            LoadMainDialogue(lines);
        }
        else if (fileName.Contains("sub"))
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

            if (parts.Length >= 15)
            {
                int main = int.Parse(parts[0]);
                if (main == Dial)
                {
                    DialogueEntry entry = new DialogueEntry
                    {
                        Main = main,
                        ScriptKey = int.Parse(parts[1]),
                        LineKey = int.Parse(parts[2]),
                        Background = parts[3],
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
        Debug.Log("현재 인덱스 숫자: "+ currentDialogueList.Count);
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
        string state = GetState(entry);
        /*송수영이 추가한 부분 테스트*/
        DotState stateEnum;
        if (Enum.TryParse(state, true, out stateEnum))
        {
            string body = "";
            string eyes = "";
            if (stateEnum == DotState.Main)
            {
                body = GetBody(entry);
                eyes = GetExpression(entry);
            }

            Debug.Log($"현재 body {body} eyes {eyes}");

            dot.ChangeState(stateEnum, body, -1, eyes);
        }
        /**/


        switch (textType)
        {
            case "text":
                if (actor == "Dot")
                {
                    DotTextUI.text = $"{korText}";
                    DotPanel.SetActive(true);
                    StartCoroutine(FadeIn(DotPanel.GetComponent<CanvasGroup>(), 0.5f, DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>()));
                    RegisterNextButton(DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                }
                else if (actor == "Player")
                {
                    PlayTextUI.text = $"{korText}";
                    PlayPanel.SetActive(true);
                    StartCoroutine(FadeIn(PlayPanel.GetComponent<CanvasGroup>(), 0.5f, PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>()));
                    RegisterNextButton(PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>());
                }
                break;
            case "selection":
                SelectionPanel.SetActive(true);
                StartCoroutine(FadeIn(SelectionPanel.GetComponent<CanvasGroup>(), 0.5f, SelectionPanel.transform.GetComponentInChildren<Button>()));
                ShowSelection(korText);
                break;
            case "textbox":
                InputPanel.SetActive(true);
                InputTextUI.text = korText;
                StartCoroutine(FadeIn(InputPanel.GetComponent<CanvasGroup>(), 0.5f, InputPanel.transform.GetChild(1).GetComponent<Button>()));
                RegisterNextButton(InputPanel.transform.GetChild(1).GetComponent<Button>());
                break;
            case "checkbox3":
                Checkbox3Panel.SetActive(true);
                ShowCheckboxOptions(Checkbox3Panel, korText);
                StartCoroutine(FadeIn(Checkbox3Panel.GetComponent<CanvasGroup>(), 0.5f, Checkbox3Panel.transform.GetChild(1).GetComponent<Button>()));
                RegisterNextButton(Checkbox3Panel.transform.GetChild(1).GetComponent<Button>());
                break;
            case "checkbox4":
                Checkbox4Panel.SetActive(true);
                ShowCheckboxOptions(Checkbox4Panel, korText);
                StartCoroutine(FadeIn(Checkbox4Panel.GetComponent<CanvasGroup>(), 0.5f, Checkbox4Panel.transform.GetChild(1).GetComponent<Button>()));
                RegisterNextButton(Checkbox4Panel.transform.GetChild(1).GetComponent<Button>());
                break;

            case "selection3":
                Selection3Panel.SetActive(true);
                ShowSelectionOptions(Selection3Panel, korText);
                StartCoroutine(FadeIn(Selection3Panel.GetComponent<CanvasGroup>(), 0.5f, Selection3Panel.transform.GetChild(1).GetComponent<Button>()));
                break;

            case "selection4":
                Selection4Panel.SetActive(true);
                ShowSelectionOptions(Selection4Panel, korText);
                StartCoroutine(FadeIn(Selection4Panel.GetComponent<CanvasGroup>(), 0.5f, Selection4Panel.transform.GetChild(1).GetComponent<Button>()));
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

    void ShowSelectionOptions(GameObject checkboxPanel, string options)
    {
        string[] selections = options.Split('|');
        for (int i = 0; i < selections.Length; i++)
        {
            TextMeshProUGUI text = checkboxPanel.transform.GetChild(2).GetChild(0).GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            text.text = selections[i];
        }
    }

    public void OnSelectionClicked(int index)
    {
        var currentEntry = currentDialogueList[dialogueIndex] as DialogueEntry;
        if (currentEntry != null)
        {
            Debug.Log($"Current LineKey: {currentEntry.LineKey}");
            string[] nextKeys = currentEntry.NextLineKey.Split('|');

            if (index < nextKeys.Length && int.TryParse(nextKeys[index], out int nextLineKey))
            {
                Debug.Log($"Next LineKey: {nextLineKey}");
                int nextIndex = currentDialogueList.FindIndex(entry => (entry as DialogueEntry)?.LineKey == nextLineKey);

                if (nextIndex != -1)
                {
                    dialogueIndex = nextIndex;
                }
                else
                {
                    Debug.Log("Next LineKey not found in dialogue list. Ending dialogue.");
                    DialEnd();
                    return;
                }
            }
            else
            {
                Debug.Log("Invalid NextLineKey index or parse failure. Ending dialogue.");
                DialEnd();
                return;
            }
        }
        else
        {
            Debug.Log("Current entry is null. Ending dialogue.");
            DialEnd();
            return;
        }

        SelectionPanel.SetActive(false);
        Selection3Panel.SetActive(false);
        Selection4Panel.SetActive(false);
        ShowNextDialogue();
    }

    void NextDialogue()
    {
        var currentEntry = currentDialogueList[dialogueIndex] as DialogueEntry;
        if (currentEntry != null)
        {
            if (int.TryParse(currentEntry.NextLineKey, out int nextLineKey))
            {
                int nextIndex = currentDialogueList.FindIndex(entry => (entry as DialogueEntry)?.LineKey == nextLineKey);

                if (nextIndex != -1)
                {
                    dialogueIndex = nextIndex;
                }
                else
                {
                    DialEnd();
                    return;
                }
            }
            else
            {
                Debug.Log("NextLineKey is not a valid integer. Moving to the next entry by index.");
                dialogueIndex++;
            }
        }
        else
        {
            Debug.Log("Current entry is null. Ending dialogue.");
            DialEnd();
            return;
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
        Selection3Panel.SetActive(false);
        Selection4Panel.SetActive(false);
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
    string GetState(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).AnimState;
        if (entry is SubDialogueEntry)
            return (entry as SubDialogueEntry).AnimState;
        return "";
    }

    string GetBody(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).DotBody;
        return "";
    }
    string GetExpression(object entry)
    {
        if (entry is DialogueEntry)
            return (entry as DialogueEntry).DotExpression;
        return "";
    }

    void RegisterNextButton(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(NextDialogue);
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration, Button button)
    {
        float counter = 0f;
        button.interactable = false;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
        button.interactable = true;
    }
}
