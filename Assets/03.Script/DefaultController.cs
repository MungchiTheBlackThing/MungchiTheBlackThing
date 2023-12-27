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
    GameObject Diary;
    GameObject canvas;

    PlayerController _player;
    GameObject[] uiList;

    public void Start()
    {
        uiList=GameObject.FindGameObjectsWithTag("UI");
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        this.gameObject.name = this.gameObject.name.Substring(0,this.gameObject.name.IndexOf('('));
        isClose=false;
        scrollRect = this.transform.parent.gameObject.GetComponent<ScrollRect>();
        DefaultPos = this.transform.position;
        canvas=GameObject.Find("Canvas");
    }

    public void Update()
    {
        
        if (NoteClick.CanScroll == false)
        {
            scrollRect.horizontal = false;
        }
    }
    public void SetLightDiary()
    {
        GameObject parent=GameObject.Find("phase_diary");

        for(int i=0;i<parent.transform.childCount;i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(true);
        }
        scrollRect.horizontal = true;
        GameObject.Find("TimeManager").GetComponent<SkipController>().SetSleepCheckList();
    }
    public void SetDiary()
    {
        
        GameObject selected=EventSystem.current.currentSelectedGameObject;
        if(selected.transform.GetChild(0).gameObject.activeSelf == false)
        {
            GameObject alter = Resources.Load<GameObject>(this.gameObject.name+"/alert_diary");
            StartCoroutine(CloseAlter(Instantiate(alter,selected.transform.parent)));
            //alter생성
            return;
        }
        //만약에 있으면 diary를 만든다
        
        for(int i=0;i<uiList.Length;i++)
            uiList[i].SetActive(false);
        Instantiate(Diary,this.transform.parent.transform.parent);
    }
    public void CloseMenu()
    {
        for(int i=0;i<uiList.Length;i++)
            uiList[i].SetActive(false);
    }
    public void OpenMenu()
    {
        for(int i=0;i<uiList.Length;i++)
            uiList[i].SetActive(true);
    }
    public void InstMoonSystem(){
        
        GameObject alter = Resources.Load<GameObject>(this.gameObject.name+"/alert_moonradio");

        if(alter==null){
            Instantiate(moon_main,this.transform.parent.transform.parent);   
            for(int i=0;i<uiList.Length;i++)
                uiList[i].SetActive(false);
        }
        else
        {
            StartCoroutine(CloseAlter(Instantiate(alter,this.transform.parent)));
        }
    }

    IEnumerator CloseAlter(GameObject alter){
        yield return new WaitForSeconds(2f);
        Destroy(alter);
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
            Instantiate(Resources.Load("Bino_"+_player.GetChapter().ToString())); //Bino이름 동적으로 바꿔야함.
            //canvas내 모든 transform 전부 setActive(false);
            for(int i=0;i<canvas.transform.childCount;i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }  
            parent_Bino.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OpenDoor(){
        GameObject selected=EventSystem.current.currentSelectedGameObject;
        selected.transform.parent.GetComponent<Animator>().SetBool("isClose",isClose);
        isClose=!isClose;
    }

}
