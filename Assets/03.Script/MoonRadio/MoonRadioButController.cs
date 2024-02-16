using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class MoonRadioButController : MonoBehaviour
{

    [SerializeField]
    GameObject popup;
    [SerializeField]
    GameObject []chat;

    GameObject main;

    [SerializeField]
    GameObject []exitPopup;

    Button ExitBtn;
    int cnt=0;
    void Start(){
        for(int i=0;i<this.transform.parent.childCount;i++){
            if(this.transform.parent.GetChild(i).name.Contains("moonRadio_main"))
                main=this.transform.parent.GetChild(i).gameObject;
        }
        cnt=main.GetComponent<MainMoonRadioUIController>().getMoonCnt();
    }

    public void ExitMoonChannel(){
        //exitPopup setActive on
        //exit but interactable off
        popup.SetActive(true);
        ExitBtn=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ExitBtn.interactable=false;
        if(exitPopup.Length!=0)
            exitPopup[(cnt-1)%2].SetActive(true);
    }

    //달나라 송신기 대화부분 변경하기 (2번째 이야기 ) -> 콘텐츠 적 내용 변경 및.. 미리 생성해서 교체하는 방식을 사용하는게 나을라나?
    public void ResetTalk()
    {
        ExitBtn.interactable=true;
        ExitBtn.gameObject.SetActive(false);
        popup.SetActive(false);
        if(exitPopup.Length!=0)
            exitPopup[(cnt-1)%2].SetActive(false);
        main.GetComponent<MainMoonRadioUIController>().resetRadioMoon();
        //대화도 새로 바뀔 예정 -> 원래버전으로 바꾼다 
    }

    public void ExitPopup(){
        //popup off -> move a main channel 
        //close moonRadio_moon channel
        popup.SetActive(false);
        if(exitPopup.Length!=0)
            exitPopup[(cnt-1)%2].SetActive(false);
        main.SetActive(true);
        Debug.Log(1);

    }
}
