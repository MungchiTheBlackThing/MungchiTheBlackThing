using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUIController : MonoBehaviour
{

    int days = 0; //days 변경시만...
    //1Day로 바꿀 예정
    [SerializeField]
    bool isInstant;
    [SerializeField]
    GameObject dragIcon;
    [SerializeField]
    GameObject dragScroller;
    // Update is called once per frame

    [SerializeField]
    GameObject alter;
    [SerializeField]
    GameObject detailed_popup;
    [SerializeField]
    GameObject menuUI;

    void Update()
    {
        
        if(isInstant){
            //이름 부여해야함.
            Instantiate(dragIcon,dragScroller.transform.GetChild(0));
            isInstant=false;
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
        }

    }

    public void onClickdragIcon(){

        GameObject day=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if(day.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.childCount!=0){
            alter.SetActive(true);
        }else{
            dragScroller.transform.parent.gameObject.SetActive(false);
            detailed_popup.SetActive(true);
        }
    }

    public void canceled(){
        alter.SetActive(false);
    }
    
    public void exit(){
        //현재 게임 오브젝트가 DayProgress_Default이면, DayProgressUI SetActive한다.

        if(detailed_popup.activeSelf){
            detailed_popup.SetActive(false);
            dragScroller.transform.parent.gameObject.SetActive(true);
        }else{
            this.gameObject.SetActive(false);
            menuUI.gameObject.SetActive(true);
        }
        
    }

}
