using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterSpeech
{
    public string character;
    public string speech;
}

[Serializable]
public class MoonChapter
{
    public int chapter_number;
    public CharacterSpeech[] script_1;
    public CharacterSpeech[] script_2;
}

[Serializable]
public class MoonRadioScript
{
    public MoonChapter[] chapters;
}


public class MoonRadioCallJson : MonoBehaviour
{
    // Start is called before the first frame update
    static public MoonRadioScript radioScript;
    void Start()
    {
        var moonRadioJson = Resources.Load<TextAsset>("Json/moon_radio_dummy");
    
        if(moonRadioJson)
        {
            radioScript = JsonUtility.FromJson<MoonRadioScript>(moonRadioJson.ToString());

            Debug.Log(radioScript.chapters.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
