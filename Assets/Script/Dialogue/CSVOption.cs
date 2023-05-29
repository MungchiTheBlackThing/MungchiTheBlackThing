using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Dialogue;

public static class CSVOption
{
    private static Language currentLanguage = Language.Korean;
    public static Language GetLanguage() { return currentLanguage; }
    public static void SetLanguage(Language language) { currentLanguage = language; }
}
