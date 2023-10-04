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
    GameObject PolaroidUI;
    [SerializeField]
    GameObject Polaroid;

    public void onMenu(){
        if(!Icon.activeSelf){
            Icon.transform.parent.gameObject.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",false);
        }else{
            Icon.SetActive(false);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",true);
        }
    }

    public void offMenu(){
        if(Icon.activeSelf){
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

    public void PolaroidCamera(){
        MenuDefault.SetActive(false);
        Polaroid.SetActive(true);
    }

    public void onClickHelper(){
        Helper.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickMypage(){
        MyPageUI.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickPolar(){
        PolaroidUI.SetActive(true);
        MenuDefault.SetActive(false);
    }
}
