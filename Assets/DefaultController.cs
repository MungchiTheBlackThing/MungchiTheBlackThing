using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DefaultController : MonoBehaviour
{
    ScrollRect scrollRect;
    Vector2 DefaultPos;
    bool isBinoFirst;
    bool isClose;
    public void Start()
    {
        isClose=false;
        isBinoFirst=false;
        scrollRect = this.gameObject.GetComponent<ScrollRect>();
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        DefaultPos = this.transform.position;
    }
    public void Update()
    {
        if (NoteClick.CanScroll == false)
        {
            this.transform.transform.position = DefaultPos;
            scrollRect.enabled = false;
        }
        else
        {
            scrollRect.enabled = true;
        }
    }
    //Animating 
    public void isClick(){
        Animator ani=EventSystem.current.currentSelectedGameObject.GetComponent<Animator>();
        Debug.Log(ani);
        ani.SetTrigger("isTouch");
    }

    public void setBinocular(){
        GameObject parent_Bino=EventSystem.current.currentSelectedGameObject;
        if(!isBinoFirst&&parent_Bino.transform.GetChild(0).gameObject.activeSelf){ 
            Instantiate(Resources.Load("Bino_2"));
            this.transform.parent.gameObject.SetActive(false);
            isBinoFirst=!isBinoFirst;
        }
    }

    public void OpenDoor(){
        GameObject child=EventSystem.current.currentSelectedGameObject;
        child.transform.parent.GetComponent<Animator>().SetBool("isClose",isClose);
        isClose=!isClose;
    }

}
