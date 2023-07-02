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
    private void OnEnable() {
        manager=GameObject.Find("Content").GetComponent<ChatClickManager>();
        if(manager)
            manager.currIdx+=1;    
    }
    /*눌렀어. 누르면 ChatClickManager을 호출해서 다음 스크립트 호출*/
    public void OnPointerDown(PointerEventData eventData){
        if(!isFirst){
            manager.RunScript();
            isFirst=true;
        }
    }
}
