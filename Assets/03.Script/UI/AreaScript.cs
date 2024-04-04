using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AreaScript : MonoBehaviour,IPointerDownHandler
{

    [SerializeField]
    RectTransform AreaRect, TextRect, CharacterRect;
    [SerializeField]
    public TMP_Text CharacterText;
    ChatClickManager manager;
    bool isFirst=false;

    void Start()
    {
        manager=GameObject.Find("Content").GetComponent<ChatClickManager>();
    }
    
    public void SettingText(string text)
    {
        CharacterText.text=text;
    }

    public void OnPointerDown(PointerEventData eventData){
        if(!isFirst){
            if(manager)
            {
                manager.RunScript();
                isFirst=true;
                return;
            }
           
        }
    }
}
