using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuBut;
    [SerializeField]
    GameObject Icon;

    [SerializeField]
    GameObject DayProgressUI;
    [SerializeField]
    GameObject MenuDefault;
    [SerializeField]
    GameObject Helper;
    [SerializeField]
    GameObject MyPageUI;
    [SerializeField]
    GameObject TimeUI;
    [SerializeField]
    GameObject checkList;


    public void onMenu(){
        if(!Icon.activeSelf){
            TimeUI.SetActive(false);
            Icon.transform.parent.gameObject.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",false);
        }else{
            TimeUI.SetActive(true);
            Icon.SetActive(false);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",true);
        }
    }

    public void offMenu(){
        if(Icon.activeSelf){
            TimeUI.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",true);
            Icon.SetActive(false);
        }
    }

    public void MenuoffExit(){
        Icon.transform.parent.gameObject.SetActive(false);
    }
    public void MenuAniExit(){
        Icon.SetActive(true);
    }
    public void onDayProgressUI(){
        //DayProgressUI on,.,
        DayProgressUI.SetActive(true);
        MenuDefault.SetActive(false);

    }

    public void onClickHelper(){
        Helper.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickMypage(){
        MyPageUI.SetActive(true);
        MenuDefault.SetActive(false);
    }


    public void onClickCheckListIcon()
    {
        if(checkList.activeSelf==false)
        {

            checkList.SetActive(true);
        }
        else
            checkList.SetActive(false);

    }

}
