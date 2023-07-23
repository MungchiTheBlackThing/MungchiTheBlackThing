using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
