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

public class DialogueDataAsset : MonoBehaviour
{
    // Start is called before the first frame update

    static public List<string> skipDialogue = new List<string>(); //달나라 주인공 이름, 스킵 
    static public MungchiOutingInfos outingInfos;
    static public Poems poemsData;

    [SerializeField] List<DialogueEntry> DialogueEntries;
    [SerializeField] List<SubDialogueEntry> SubDialogueEntries;
    [SerializeField] public List<object> currentDialogueList = new List<object>();
    public int Day = 0;  // Current day
    public int Dial;

    [SerializeField] SkipController SkipController;
    [SerializeField] public LanguageType CurrentLanguage = LanguageType.Kor;
    [SerializeField] PlayerController PlayerController;

    void Awake()
    {
        currentDialogueList = new List<object>();
        //데이터 베이스를 호출한다.
        skipDialogue.Add("요즘 사람들은 더 이상 \"라디오\"라는 걸 사용하지 않는다는데, 어떻게 그래?");
        skipDialogue.Add("인간은 성장의 동물. 그들은 라디오에서 mp3로, 다시 핸드폰이라는 기계로 성장했음.");
        skipDialogue.Add("~♫♮♭♩♫&?");
        var loadedoutingInfoJson = Resources.Load<TextAsset>("Json/OutingInformationData");
    
        if(loadedoutingInfoJson)
        {
            outingInfos = JsonUtility.FromJson<MungchiOutingInfos>(loadedoutingInfoJson.ToString());
        }
        var poemDataloadedJson = Resources.Load<TextAsset>("Json/PoemsData");
    
        Debug.Log(poemDataloadedJson);
        if(poemDataloadedJson)
        {
             poemsData = JsonUtility.FromJson<Poems>(poemDataloadedJson.ToString());
        }
    }
    //private void Start()
    //{
    //    Dial = SkipController.GetTimeCurIdx;
    //    Day = PlayerController.GetChapter();
    //}

    //public void LoadMainDialogue(string[] lines)
    //{
    //    for (int i = 1; i < lines.Length; i++)
    //    {
    //        string line = lines[i];
    //        if (string.IsNullOrEmpty(line))
    //        {
    //            continue;
    //        }
    //        string[] parts = ParseCSVLine(line);
    //        Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

    //        if (parts.Length >= 15)
    //        {
    //            int main = int.Parse(parts[0]);
    //            if (main == Dial)
    //            {
    //                DialogueEntry entry = new DialogueEntry
    //                {
    //                    Main = main,
    //                    ScriptKey = int.Parse(parts[1]),
    //                    LineKey = int.Parse(parts[2]),
    //                    Background = parts[3],
    //                    Actor = parts[4],
    //                    AnimState = parts[5],
    //                    DotBody = parts[6],
    //                    DotExpression = parts[7],
    //                    TextType = parts[8],
    //                    KorText = ApplyLineBreaks(parts[9]),
    //                    EngText = ApplyLineBreaks(parts[10]),
    //                    NextLineKey = parts[11],
    //                    AnimScene = parts[12],
    //                    AfterScript = parts[13],
    //                    Deathnote = parts[14]
    //                };

    //                string displayedText = CurrentLanguage == LanguageType.Kor ? entry.KorText : entry.EngText;
    //                entry.KorText = displayedText;
    //                DialogueEntries.Add(entry);
    //                currentDialogueList.Add(entry);

    //                Debug.Log($"Added DialogueEntry: {displayedText}");
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError($"Line {i} does not have enough parts: {line}");
    //        }
    //    }
    //}

    //public void LoadSubDialogue(string[] lines)
    //{
    //    for (int i = 1; i < lines.Length; i++)
    //    {
    //        string line = lines[i];
    //        if (string.IsNullOrEmpty(line))
    //        {
    //            continue;
    //        }
    //        string[] parts = ParseCSVLine(line);
    //        Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

    //        if (parts.Length >= 12)
    //        {
    //            int scriptKey = int.Parse(parts[0]);
    //            if (scriptKey == Day)
    //            {
    //                SubDialogueEntry entry = new SubDialogueEntry
    //                {
    //                    ScriptKey = scriptKey,
    //                    LineKey = int.Parse(parts[1]),
    //                    Color = parts[2],
    //                    Actor = parts[3],
    //                    AnimState = parts[4],
    //                    DotAnim = parts[5],
    //                    TextType = parts[6],
    //                    KorText = ApplyLineBreaks(parts[7]),
    //                    EngText = ApplyLineBreaks(parts[8]),
    //                    NextLineKey = parts[9],
    //                    Deathnote = parts[10],
    //                    AfterScript = parts[11]
    //                };

    //                string displayedText = CurrentLanguage == LanguageType.Kor ? entry.KorText : entry.EngText;
    //                entry.KorText = displayedText;
    //                SubDialogueEntries.Add(entry);
    //                currentDialogueList.Add(entry);

    //                Debug.Log($"Added SubDialogueEntry: {displayedText}");
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError($"Line {i} does not have enough parts: {line}");
    //        }
    //    }
    //    Debug.Log("현재 인덱스 숫자: " + currentDialogueList.Count);
    //}

    //string[] ParseCSVLine(string line)
    //{
    //    List<string> result = new List<string>();
    //    bool inQuotes = false;
    //    string value = "";

    //    foreach (char c in line)
    //    {
    //        if (c == '"')
    //        {
    //            inQuotes = !inQuotes;
    //        }
    //        else if (c == ',' && !inQuotes)
    //        {
    //            result.Add(value.Trim());
    //            value = "";
    //        }
    //        else
    //        {
    //            value += c;
    //        }
    //    }

    //    if (!string.IsNullOrEmpty(value))
    //    {
    //        result.Add(value.Trim());
    //    }
    //    return result.ToArray();
    //}

    //string ApplyLineBreaks(string text)
    //{
    //    return text.Replace(@"\n", "\n");
    //}
}
