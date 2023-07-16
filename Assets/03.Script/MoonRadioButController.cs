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
    GameObject chat;
    GameObject main;

    void Start(){
        for(int i=0;i<this.transform.parent.childCount;i++){
            if(this.transform.parent.GetChild(i).name.Contains("moonRadio_main"))
                main=this.transform.parent.GetChild(i).gameObject;
        }

        popup.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text="("+main.GetComponent<MainMoonRadioUIController>().getMoonCnt().ToString()+"/2)";

        //실행시 5초 뒤에 깜빡 깜빡 등장.
        Invoke("AppearOnScreen",1.5f);
    }

    void AppearOnScreen(){
        chat.SetActive(true);
    }
    public void ExitMoonChannel(){
        //exitPopup setActive on
        //exit but interactable off
        Button currObj=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        currObj.interactable=false;
        popup.SetActive(true);
    }

    public void ExitPopup(){
        //popup off -> move a main channel 
        //close moonRadio_moon channel
        popup.SetActive(false);
        main.SetActive(true);
        Destroy(this.gameObject);
    }
}
