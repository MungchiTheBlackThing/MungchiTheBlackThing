using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{
    //isFour 변수에 따라 선택지 갯수가 달라진다.
    [SerializeField]
    bool isFour;
    //선택지는 나중에 저장한다.
    public void Choose(){
        GameObject option=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if(option.transform.GetChild(0).gameObject.activeSelf){
            option.transform.GetChild(0).gameObject.SetActive(false);
        }else{
            option.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
