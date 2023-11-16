using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DefaultController : MonoBehaviour
{

    [SerializeField]
    GameObject moon_main;
    ScrollRect scrollRect;
    Vector2 DefaultPos;
    bool isClose;

    [SerializeField]
    GameObject moon_diary;
    Canvas canvas;

    public void Start()
    {
        isClose=false;
        scrollRect = this.transform.parent.gameObject.GetComponent<ScrollRect>();
        DefaultPos = this.transform.position;
        canvas=this.GetComponent<Canvas>();
    }

    public void Update()
    {
        if (NoteClick.CanScroll == false)
        {
            this.transform.transform.position = DefaultPos;
            scrollRect.horizontal = false;
        }
        else
        {
            scrollRect.horizontal = true;
        }
    }

    public void InstMoonSystem(){
        //if(player.GetCurrTime()!="night")
        //    return ;
        Instantiate(moon_main,this.transform.parent.transform.parent);
    }

    //Animating 
    public void isClick(){
        GameObject selected=EventSystem.current.currentSelectedGameObject;
        Animator ani=selected.GetComponent<Animator>();
        if(ani!=null)
            ani.SetTrigger("isTouch");
    }

    public void setBinocular(){
        GameObject parent_Bino=EventSystem.current.currentSelectedGameObject;
        if(parent_Bino.transform.GetChild(0).gameObject.activeSelf){ 
            //Instantiate(Resources.Load("Bino_"+player.GetCurrDay().ToString())); //Bino이름 동적으로 바꿔야함.
            this.transform.parent.gameObject.SetActive(false);
            parent_Bino.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OpenDoor(){
        GameObject selected=EventSystem.current.currentSelectedGameObject;
        selected.transform.parent.GetComponent<Animator>().SetBool("isClose",isClose);
        isClose=!isClose;
    }

    public void OpenDiary(){
        //if(player.GetCurrTime()!="night")
        //    return ;
        Instantiate(moon_diary,this.transform.parent.transform.parent);
    }
}
