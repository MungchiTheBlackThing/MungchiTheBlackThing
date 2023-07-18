using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuBut;
    [SerializeField]
    GameObject Icon;

    public void onMenu(){
        if(!Icon.activeSelf){
            Icon.transform.parent.gameObject.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",true);
        }else{
            Icon.SetActive(false);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",false);
        }
    }

    public void offMenu(){
        if(Icon.activeSelf){
            this.gameObject.GetComponent<Animator>().SetBool("isDowning",false);
            Icon.SetActive(false);
        }
    }

    public void MenuoffExit(){
        Icon.transform.parent.gameObject.SetActive(false);
    }
    public void MenuAniExit(){
        Icon.SetActive(true);
    }
}
