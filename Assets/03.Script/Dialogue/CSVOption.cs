using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Dialogue;

public static class CSVOption
{
    private static LANGUAGE currentLanguage = LANGUAGE.KOREAN;
    public static LANGUAGE GetLanguage() { return currentLanguage; }
    public static void SetLanguage(LANGUAGE language) { currentLanguage = language; }

    public static Dictionary<LANGUAGE, Dictionary<SCRIPT_TYPE, string>> languageMappings = new Dictionary<LANGUAGE, Dictionary<SCRIPT_TYPE, string>>()
    {
        {
            LANGUAGE.KOREAN, new Dictionary<SCRIPT_TYPE, string>()
            {
                { SCRIPT_TYPE.TEXT, "Kor Text" },
                { SCRIPT_TYPE.CHOICE, "Kor Choice" },
            }
        },
        {
            LANGUAGE.ENGLISH, new Dictionary<SCRIPT_TYPE, string>()
            {
                { SCRIPT_TYPE.TEXT, "Eng Text" },
                { SCRIPT_TYPE.CHOICE, "Eng Choice" },
            }
        }
    };
}
