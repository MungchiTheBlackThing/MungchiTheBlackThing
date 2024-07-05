using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.IO;

[System.Serializable]
public class DialogueEntry
{
    public string Character;
    public string Type;
    public string Dialogue;
}
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DotTextUI;
    public TextMeshProUGUI PlayTextUI;
    public GameObject temp;
    public GameObject DotPanel;
    public GameObject PlayPanel;
    public GameObject selectionPanel;
    public Button nextButton;
    public GameObject MainDial;

    public List<DialogueEntry> dialogueEntries;
    public int dialogueIndex = 0;

    public void Start()
    {
        DotPanel = Instantiate(Resources.Load("DotBalloon") as GameObject, this.transform);
        temp = DotPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        DotTextUI = temp.GetComponent<TextMeshProUGUI>();
        DotPanel.SetActive(false);
        PlayPanel = Instantiate(Resources.Load("PlayerOneLineBallum") as GameObject, this.transform);
        temp = PlayPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        PlayTextUI = temp.GetComponent<TextMeshProUGUI>();
        PlayPanel.SetActive(false);
        selectionPanel = Instantiate(Resources.Load("TwoSelectionBallum") as GameObject, this.transform);
        selectionPanel.SetActive(false);
    }
    public void st()
    {
        LoadDialogue("Dialogue");
        //nextButton.onClick.AddListener(OnNextButtonClicked);
        ShowNextDialogue();
    }

    void LoadDialogue(string fileName)
    {
        dialogueEntries = new List<DialogueEntry>();

        TextAsset dialogueData = Resources.Load<TextAsset>(fileName);

        if (dialogueData != null)
        {
            Debug.Log("Dialogue file loaded successfully.");
            Debug.Log("Dialogue file content: " + dialogueData.text);
            string[] lines = dialogueData.text.Split('\n');
            Debug.Log(lines.Length);
            for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더이므로 건너뜁니다.
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 3)
                {
                    DialogueEntry entry = new DialogueEntry
                    {
                        Character = parts[0],
                        Type = parts[1],
                        Dialogue = parts[2]
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
    /// <summary>
    /// 아오 준현아 이거 Text 부분도 다 Selection처럼 버튼 누르게 넘어가도록 바꾸자
    /// </summary>
    void ShowNextDialogue()
    {
        Debug.Log(dialogueIndex);
        DotPanel.SetActive(false);
        PlayPanel.SetActive(false);
        selectionPanel.SetActive(false);
        if (dialogueIndex < dialogueEntries.Count)
        {
            DialogueEntry entry = dialogueEntries[dialogueIndex];

            
            if (entry.Type == "text")
            {
                if ($"{entry.Character}" == "Dot")
                {
                    Debug.Log("dot");
                    DotPanel.SetActive(entry.Type == "text");
                    //DotTextUI = DotPanel.transform.Find("Script").GetComponent<TextMeshPro>();
                    Debug.Log(DotTextUI);
                    DotTextUI.text = $"{entry.Character}: {entry.Dialogue}";
                    ShowDottext();
                }
                else if ($"{entry.Character}" == "Player")
                {
                    Debug.Log("player");
                    PlayPanel.SetActive(entry.Type == "text");
                    //PlayTextUI = PlayPanel.transform.Find("Script").GetComponent<TextMeshPro>();
                    PlayTextUI.text = $"{entry.Character}: {entry.Dialogue}";
                    ShowPlayertext();
                }
            }
            if (entry.Type == "selection")
            {
                selectionPanel.SetActive(entry.Type == "selection");
                ShowSelection(entry.Dialogue);
            }
        }
    }

    void ShowDottext()
    {
        Button button = DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        Debug.Log(button);
        button.onClick.AddListener(() => nextbut());
    }
    void ShowPlayertext()
    {
        Button button = PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        Debug.Log(button);
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
            int index = i; // 로컬 변수로 캡처
            button.onClick.AddListener(() => OnSelectionClicked(index));
        }
    }

    void nextbut()
    {
        Debug.Log("NEXT");
        dialogueIndex++;
        ShowNextDialogue();
    }

    void OnSelectionClicked(int index)
    {
        // 선택지에 따른 대화 처리
        selectionPanel.SetActive(false);
        dialogueIndex++;
        ShowNextDialogue();
    }


    /// <summary>
    /// 이거 무한으로 넘어가버림
    /// </summary>
}
