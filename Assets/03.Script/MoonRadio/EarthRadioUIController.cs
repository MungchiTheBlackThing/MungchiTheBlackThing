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
    GameObject alter_wordsCnt; //500자 이내만
    GameObject main;

    GameObject answerTextBox;
    GameObject answerGroup;
    bool line_within500=true;
    int textline_cnt=0;
    void Start(){
        answerTextBox=GameObject.Find("textbox").gameObject;

        for(int i=0;i<this.transform.parent.childCount;i++){
            if(this.transform.parent.GetChild(i).name.Contains("moonRadio_main"))
                main=this.transform.parent.GetChild(i).gameObject;
        }

        Debug.Log(main);
    }
    void OnEnable()
    {
        textline_cnt=0;
    }
    //TextField 처리공간
    public void write2Moon(TMP_Text text){
        //누르면, 박스는 사라진다.

        if(text.text.Length>=1){
            answerTextBox.SetActive(false);
        }else{ 
            answerTextBox.SetActive(true);
        }
        textline_cnt=text.text.Length;
        Debug.Log(textline_cnt);
        if(textline_cnt>500) line_within500 = false;
        else line_within500=true;
        //한글자라도 있으면 없애고, 한글자 존재하면 생김.
        //글씨 처리..
        //text.text는 moonbut누를시 전달될 string
    }

    public void send2MoonBut(){
        //textfield가 사라진다.
        //현재 누른 오브젝트 실행 후 애니메이션 끝나면 함수 실행
        //Debug.Log(inputText);
        if(line_within500==false) //500글자 이내
        {
            alter_wordsCnt.SetActive(true);
            //전송 불가능함.
            return;
        }
        alter_wordsCnt.SetActive(false);
        GameObject currObj=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        currObj.GetComponent<Animator>().SetBool("isGoing",true);
        send_earth.GetComponent<Animator>().SetBool("isGoing",true);
    }

    public void send2MoonButEventExit(){

        send_earth.SetActive(false);
        send_alert.SetActive(true);
        Invoke("waitAlert",.5f);
    }

    //channel exit but 누른다.
    public void exitChannelBut(){
        //close_Alter이 뜬다.
        close_alert.SetActive(true);
    }

    //채널 종료
    public void yesBut(){
        //yes를 누르면 send_Alert 뜸.. 화면 클릭시 메인 화면으로 이동
        main.SetActive(true);
        Destroy(this.gameObject);
    }

    //채널 종료 안함
    public void noBut(){
        //no일시... 물어봐야할듯 뭔데..? 
        close_alert.SetActive(false);
    }
    void reset()
    {
        //돌아오는 애니메이터 
        //animator.ResetTrigger("YourTrigger");
        answerTextBox.SetActive(true); //다시 쓸수 있기 때문에 게임오브젝트를 켜준다s
    }
    public void waitAlert()
    {
        StartCoroutine("waitForTransmission");
    }
    //waitForTransmission
    public IEnumerator waitForTransmission(){

        yield return new WaitForSeconds(2.0f);
        send_alert.SetActive(false);
        send_earth.SetActive(true);
        reset();
        yield return null;
        
        //main.SetActive(true);
        //Destroy(this.gameObject);
    }

}
