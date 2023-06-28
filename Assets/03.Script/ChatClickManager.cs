using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
1. 현재 모든 자식을 불러온다.
2. 클릭한 자식의 인덱스를 확인한다.
3. 다음 자식을 생성한다. - 동적 생성
*/
public class ChatClickManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] dreamNPC;

    public void OnClick(){
        //클릭된 오브젝트의 클릭 비활성화
        GameObject clickObject =EventSystem.current.currentSelectedGameObject;
        Transform parentObject=clickObject.transform.parent;

        if(parentObject.childCount>=3){
            Destroy(parentObject.GetChild(0).gameObject);
            //클릭되었을때 부모의 자식 개수가 3개를 넘어서면 가장 최상위 자식 삭제
            //Destroy(clickObject.transform.parent.GetChild(1));
        }
        clickObject.GetComponent<Button>().interactable=false;
        //현재 다이어로그를 불러오지 않은 상태이기에, 랜덤으로 불러오도록 완성한다.

        //랜덤 호출 부분 -> dreamNPC의 순서를 저장하고 호출하는 방법으로 변경할 예정.
        int randIdx=Random.Range(0,dreamNPC.Length);
        GameObject obj=Instantiate(dreamNPC[randIdx]) as GameObject; //gameObject 생성
        obj.name=obj.name.Substring(0,obj.name.IndexOf('('));
        obj.transform.SetParent(parentObject,false); //현재 클릭된 오브젝트의 부모 위로 설정해주기.


    }
}
