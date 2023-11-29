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
        //현재 x좌표를 가져온다. +1씩 옆으로 이동한다.
        float remainingDistance = (dis-backRect.anchoredPosition.x); //이동거리
        Debug.Log("현재 : "+backRect.anchoredPosition.x);
        while(remainingDistance>0)
        {  
            //오른쪽으로 이동할 예정, 0->-402가 되어야함.
            float m_x=Mathf.Abs(backRect.anchoredPosition.x)+(speed*Time.deltaTime); //음수가 아닌 양수로 만들어 이동할 위치를 계산하고, 즉 값이 증가할 예정
            float m_y=backRect.anchoredPosition.y;
            backRect.anchoredPosition=new Vector2(-m_x,m_y); //실제 값을 전달할 때, 음수로 전달하여 오른쪽으로 이동함을 전달함.
            //Debug.Log(Mathf.Abs(backRect.anchoredPosition.x));
            remainingDistance = dis-Mathf.Abs(backRect.anchoredPosition.x); //dis(402) - 양수(202) = 양수 음수거나 0일때 stop
        }
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
