using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI List")]
    public List<GameObject> uiList;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    private void Start()
    {
        var objects = FindObjectsOfType<TextScript>();
        foreach(var obj in objects){
            uiList.Add(obj.gameObject);
        }
    }

    public void ChangeUILanguage()
    {

    }
}
