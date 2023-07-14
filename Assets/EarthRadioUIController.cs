using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EarthRadioUIController : MonoBehaviour
{

    [SerializeField]
    GameObject send_earth;
    [SerializeField]
    GameObject close_alert;
    [SerializeField]
    GameObject send_alert;
    [SerializeField]
    GameObject main;

    GameObject answerTextBox;
    GameObject answerGroup;
    void Start(){
        answerTextBox=GameObject.Find("textbox").gameObject;
    }
    
    //TextField 처리공간
    public void write2Moon(TMP_Text text){
        //누르면, 박스는 사라진다.

        if(text.text.Length>=1){
            answerTextBox.SetActive(false);
        }else{ 
            answerTextBox.SetActive(true);
        }
        Debug.Log(text.text);
        //한글자라도 있으면 없애고, 한글자 존재하면 생김.
        //글씨 처리..
        //text.text는 moonbut누를시 전달될 string
    }

    public void send2MoonBut(){
        //textfield가 사라진다.
        GameObject moonAnswer=send_earth.transform.FindChild("MoonAnswer").gameObject;

        if(!moonAnswer.activeSelf){
            GameObject.Find("answerGroup").gameObject.SetActive(false);
            moonAnswer.SetActive(true);
        }
        //button을 누른다.
        //text가 뜬다
    }

    //channel exit but 누른다.
    public void exitChannelBut(){
        //close_Alter이 뜬다.
        close_alert.SetActive(true);
    }

    //채널 종료
    public void yesBut(){
        //yes를 누르면 send_Alert 뜸.. 화면 클릭시 메인 화면으로 이동
        close_alert.SetActive(false);
        send_earth.SetActive(false);
        send_alert.SetActive(true);
    }

    //채널 종료 안함
    public void noBut(){
        //no일시... 물어봐야할듯 뭔데..? 
        close_alert.SetActive(false);
    }

    public void goMoonmain(){
        this.gameObject.SetActive(false);
        main.SetActive(true);
    }

}
