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

public class SKipSleepingParser : MonoBehaviour
{

    [SerializeField] List<SkipSleeping> Skips;
    [SerializeField] public List<object> currentDialogueList = new List<object>();
    [SerializeField] public LanguageType CurrentLanguage = LanguageType.Kor;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset dialogueData = Resources.Load<TextAsset>("Dial/moonradio");

        if (dialogueData == null)
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
            return;
        }
        Debug.Log("Dialogue file loaded successfully.");
        string[] lines = dialogueData.text.Split('\n');
        LoadSkipSleeping(lines);
    }

    public void LoadSkipSleeping(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] parts = ParseCSVLine(line);
            //Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

            if (parts.Length >= 4)
            {
                SkipSleeping entry = new SkipSleeping
                {
                    ID = int.Parse(parts[0]),
                    Actor = parts[1],
                    KorText = ApplyLineBreaks(parts[2]),
                    EngText = ApplyLineBreaks(parts[3]),
                };
                string displayedText = CurrentLanguage == LanguageType.Kor ? entry.KorText : entry.EngText;
                entry.KorText = displayedText;
                Skips.Add(entry);
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
}
