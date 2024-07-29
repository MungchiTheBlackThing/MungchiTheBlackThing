using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Chapter
{
    public string character;
    public string speech;
}

[System.Serializable]
public class MoonRadioScript
{
    public List<Chapter> chapter1;
    public List<Chapter> chapter2;
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
        }
    }
}
