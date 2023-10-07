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
    PlayerInfo player; //이거 플레이어 말고 GameManager부분으로 이동해야할거같음.
    Canvas canvas;
    GraphicRaycaster raycaster;
    PointerEventData point;
    public void Start()
    {
        isClose=false;
        scrollRect = this.gameObject.GetComponent<ScrollRect>();
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        DefaultPos = this.transform.position;
        canvas=this.GetComponent<Canvas>();
        raycaster=canvas.GetComponent<GraphicRaycaster>();
        point = new PointerEventData(null);
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

        /*
        그래픽에 레이캐스트를 쏜다.
         레이어 fixAnimator에 해당하는 오브젝트가 있는가를 확인한다.
         있으면? isClick을 실행한다.
         */

        GameObject selectedObj=checkFirstRaycasterObj();
        if(selectedObj){
            switch(selectedObj.name){
                case "fix_door_open": case "fix_door_close" :
                OpenDoor(selectedObj);
                break;
                case "fix_binoocular":
                setBinocular(selectedObj);
                break;
                case "fix_moonradio":
                //밤이라면 문라디오 실행
                if(player.GetCurrTime()=="night"){
                    InstMoonSystem(selectedObj);
                    this.gameObject.SetActive(false);
                    //라디오를 누르면 잠시 꺼놓는다.
                    //라디오 종료시 다시 켜놓는다.
                }
                break;
                case "phase_letter":
                    selectedObj.GetComponent<NoteClick>().setNote();
                break;
                default:
                    isClick(selectedObj);
                    break;
            }
        }
    }
    public void InstMoonSystem(GameObject selectedObj){
        Instantiate(moon_main,this.transform.parent);
    }
    GameObject checkFirstRaycasterObj(){
        point.position=Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        if(Input.GetMouseButtonDown(0)){
            raycaster.Raycast(point,results);
        }

        if(results.Count==0)
            return null;

        return results[0].gameObject;
    }
    //Animating 
    public void isClick(GameObject selected){
        Animator ani=selected.GetComponent<Animator>();
        if(ani!=null)
            ani.SetTrigger("isTouch");
    }

    public void setBinocular(GameObject parent_Bino){
        //GameObject parent_Bino=EventSystem.current.currentSelectedGameObject;
        if(parent_Bino.transform.GetChild(0).gameObject.activeSelf){ 
            Instantiate(Resources.Load("Bino_"+player.GetCurrDay().ToString())); //Bino이름 동적으로 바꿔야함.
            this.transform.parent.gameObject.SetActive(false);
            parent_Bino.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OpenDoor(GameObject child){
        child.transform.parent.GetComponent<Animator>().SetBool("isClose",isClose);
        isClose=!isClose;
    }

}
