using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialClass : MonoBehaviour
{
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
        public string color;
        public string Actor;
        public string AnimState;
        public string dotAnim;
        public string TextType;
        public string KorText;
        public string EngText;
        public string NextLineKey;
        public string Deathnote;
        public string AfterScript;
    }
}
