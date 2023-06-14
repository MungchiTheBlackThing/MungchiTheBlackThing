using Assets.Script.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public SCRIPT_TYPE? scriptType = null;
    public TMPro.TextMeshProUGUI textMeshPro = null;

    private void Start()
    {
        textMeshPro = this.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetText()
    {
        textMeshPro.text =  SubDialogueManager.instance.GetCurrentScript(CSVOption.GetLanguage());
    }
}
