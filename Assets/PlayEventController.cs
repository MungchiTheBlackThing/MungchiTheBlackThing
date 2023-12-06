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
    float speed =10.0f;
    void Start()
    {
        //( width - canvas.width )/2 -> +왼쪽, -오른쪽 이동가능
        background=this.transform.parent.gameObject;
        StartCoroutine("MoveBackground");
        //시간에 따라 메시지 게임 오브젝트생성  -> 현재 밤 버전밖에 없음,...
        Instantiate(Resources.Load<GameObject>("Night/SelectedOption"),this.transform.GetChild(0).transform.GetChild(0));
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

    public void OnClick()
    {

        
        GameObject selected=EventSystem.current.currentSelectedGameObject;
        selected.transform.GetChild(0).gameObject.SetActive(false);
        selected.transform.GetChild(1).gameObject.SetActive(true);
    }
}
