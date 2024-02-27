using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayEventController : MonoBehaviour
{
    [SerializeField]
    GameObject background;

    [SerializeField]
    GameObject selectedOptionObj;

    [SerializeField]
    GameObject Sleep;
    [SerializeField]
    GameObject Play;
    [SerializeField]
    GameObject sleepDots;
    [SerializeField]
    GameObject playBallumn;

    GameObject defaultUI;
    [SerializeField]
    GameObject iconBut;
    [SerializeField]
    MenuController menuControll;
    public static bool EventOn; //스킵을 꺼야하는 상황(이벤트가 켜저있음)
    private void OnEnable() 
    {
        EventOn = true;
        defaultUI= GameObject.Find("DefaultUI");
        menuControll = GameObject.Find("Menu").GetComponent<MenuController>();
        Sleep.SetActive(true);
        Play.GetComponent<Animator>().SetBool("isSleeping",true);
        Play.SetActive(false);
        sleepDots.SetActive(false);
        menuControll.skipoff();
    }
    void Start()
    {
        //( width - canvas.width )/2 -> +왼쪽, -오른쪽 이동가능
        background=this.transform.parent.gameObject;
        StartCoroutine("MoveBackground");
        //시간에 따라 메시지 게임 오브젝트생성  -> 현재 밤 버전밖에 없음,...
    }

    IEnumerator MoveBackground(){
        //제한 범위를 가져온다.
        //(canvas 크기 - 시스템 기기의 너비)=> 왼-오른쪽으로 이동할 수 있는 범위
        // N(위 공식)/2를 했을때 왼쪽 오른쪽으로 절반씩 이동제한이 있다는것을 알 수 있다.
        RectTransform backRect=background.GetComponent<RectTransform>();
        float dis=(backRect.rect.width-Screen.width)/2; 
        backRect.anchoredPosition=new Vector2(-dis,backRect.anchoredPosition.y);
        background.GetComponent<ScrollRect>().horizontal = false;

        return null;
    }
    public void SetUI()
    {
        defaultUI.SetActive(true);
    }
    public void SetSelectedOption()
    {
        iconBut.SetActive(false);
        defaultUI.SetActive(false);
        selectedOptionObj.SetActive(true);
        playBallumn.SetActive(true);
    }

    private void OnDisable() {
        EventOn = false;
        menuControll.skipon();
        selectedOptionObj.SetActive(false);
        playBallumn.SetActive(false);
    }
    public void MenuOn()
    {
        menuControll.skipon();
    }
    // public void OnClick()
    // {
    //     GameObject selected=EventSystem.current.currentSelectedGameObject;
    //     selected.transform.GetChild(0).gameObject.SetActive(false);
    //     selected.transform.GetChild(1).gameObject.SetActive(true);
    // }

}
