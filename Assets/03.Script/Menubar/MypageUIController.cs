using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MypageUIController : MonoBehaviour
{

    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject community;
    [SerializeField]
    GameObject credits;
    [SerializeField]
    GameObject defaultSetting;
    [SerializeField]
    GameObject editPopup;
    [SerializeField]
    GameObject alterPopup;
    [SerializeField]
    GameObject pushPopup;
    [SerializeField]
    GameObject nameSettings;

    [SerializeField]
    Slider SESlider;
    [SerializeField]
    Slider MusicSlider;
    /*이 user id,name은 수정되어야한다... player가 생기면 수정하기 popup도*/
    string UserID="";
    string UserName="";
    bool allowPopup=true;

    GameObject CopyAlter;
    GameObject inputPopup;
    GameObject[] op;

    PlayerController player;

    void Start(){
        op=GameObject.FindGameObjectsWithTag("SelectedPush");
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        /*이름과 bgm/음향 효과를 50 초기세팅*/
        Init();
    }

    void Init()
    {
        UserName=player.GetNickName();
        nameSettings.GetComponent<TMP_Text>().text=UserName;
        SESlider.value=player.GetAcousticVolume();
        MusicSlider.value=player.GetMusicVolume();
    }
    public void Exit(){
        this.gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void goCommunity(){
        if(credits.activeSelf){
            credits.SetActive(false);
        }else{
            defaultSetting.SetActive(false);
        }
        community.SetActive(true);
    }

    public void goCredit(){
        community.SetActive(false);
        credits.SetActive(true);
    }

    public void goSetting(){
        
        community.SetActive(false);
        defaultSetting.SetActive(true);
    }

    public void copyID(){

        UserID="";
        //눌렀을 때 게임오브젝트를 가져오고, 그 내부에 RealID.text를 저장한다
        GameObject copyBut=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        for(int i=0;i<copyBut.transform.childCount;i++){
            GameObject child=copyBut.transform.GetChild(i).gameObject;
            if(child.name=="RealID"){
                UserID=copyBut.transform.GetChild(i).gameObject.GetComponent<TMP_Text>().text;
            }
            if(child.name=="alter"){
                CopyAlter=child;
                CopyAlter.SetActive(true);
            }
        }

        Debug.Log(UserID);
        Invoke("closeIDAlter",1.5f);
    }
    void closeIDAlter(){
        CopyAlter.SetActive(false);
    }

    public void editName(){
        if(editPopup.activeSelf){
            for(int i=0;i<inputPopup.transform.childCount;i++){
                GameObject child=inputPopup.transform.GetChild(i).gameObject;
                if(child.name=="inputName"){
                    child.GetComponent<TMP_InputField>().text="";
                }
            }
            editPopup.gameObject.SetActive(false);
            return;
        }
        editPopup.gameObject.SetActive(true);
        if(inputPopup==null)
            inputPopup=GameObject.Find("inputPopup");
    }

    public void storeName(){
        UserName="";
        //InputField로부터 text를 가져온다.
        //alter팝업을 띄운다
        //이름을 바꾼다
        
        if(inputPopup!=null)
            for(int i=0;i<inputPopup.transform.childCount;i++){
                GameObject child=inputPopup.transform.GetChild(i).gameObject;
                if(child.name=="inputName"){
                    UserName=child.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text;
                    child.GetComponent<TMP_InputField>().text="";
                }
            }

        Debug.Log(UserName);
        alterPopup.SetActive(true);

        TMP_Text alterText=alterPopup.transform.GetChild(0).GetComponent<TMP_Text>();
        string edit=alterText.text;
        edit=edit.Remove(1,2).Insert(1,UserName);
        alterText.text=edit;
        player.SetNickName(UserName);
        inputPopup.SetActive(false);
        nameSettings.GetComponent<TMP_Text>().text=UserName;
        Invoke("closeAlter",1.5f);
    }

    void closeAlter(){
        editPopup.SetActive(false);
        alterPopup.SetActive(false);
        inputPopup.SetActive(true);
    }

    public void alarmPopup(){
        if(pushPopup.activeSelf&&allowPopup){
            GameObject select=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            Debug.Log(select.name);
            //알람 종류에 따라서 어둡게만든다.
            if(select.name.Contains("no")){
                //yes면 팝업 색을 175 175 175로 변경한다.
    
                for(int i=0;i<op.Length;i++){
                    if(op[i].name.Contains("button_on")){
                        op[i].GetComponent<TMP_Text>().color=new Color32(175, 175, 175, 255);
                    }

                    if(op[i].name.Contains("button_off"))
                        op[i].GetComponent<TMP_Text>().color=new Color32(240, 240, 240, 255);
                        allowPopup=false;
                }
            }
            pushPopup.SetActive(false);
            return;
        }
        pushPopup.SetActive(true);
    }

    public void allowButPopup(){

        if(!allowPopup){
            for(int i=0;i<op.Length;i++){
                if(op[i].name.Contains("button_off")){
                    op[i].GetComponent<TMP_Text>().color=new Color32(175, 175, 175, 255);
                }
                if(op[i].name.Contains("button_on"))
                    op[i].GetComponent<TMP_Text>().color=new Color32(240, 240, 240, 255);
                
                allowPopup=true;
            }
        }
    }
}
