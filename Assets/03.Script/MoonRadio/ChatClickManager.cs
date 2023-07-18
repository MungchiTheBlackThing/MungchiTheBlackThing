using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
/*
1. 현재 모든 자식을 불러온다.
2. 클릭한 자식의 인덱스를 확인한다.
3. 다음 자식을 생성한다. - 동적 생성
*/
public class ChatClickManager : MonoBehaviour
{

    /*
    클릭할 때 AreaScript에게 다음 GameObject 전달
    AreaScript는 누를시 전달받은 GameObject 생성, 자기자신 비활성화 
    */

    [SerializeField]
    protected GameObject[] dreamNPC;
    [SerializeField]
    GameObject exit;
    public int currIdx=0;
    //임시 대사 저장
    protected List<string> txt= new List<string>(){
        "~♫♮♭♩♫&?","숨겨 놓고 아껴 먹던 건데… 잃어버렸을 리가.","그것, 첼로 경이 뭉치에 꽂아 보냄.\n뭉치도 간식 필요하다 했음.",
        "!!♫♮♭#)&!!","비밀이라고 말한 적은 없지 않았음?\n 기억 안 남.","아, 그걸 왜 걔를 줘! 걔는 먹지도 못해!"
    };
    protected int [] idx=new int[6]{1,2,0,1,0,2};
    void Start(){
        currIdx=0;
    }
    public void RunScript(){
        if(currIdx>=idx.Length){
            if(!exit.activeSelf){
                exit.SetActive(true);
            }
            return;
        }
        Transform parentObject=this.gameObject.transform;
        if(parentObject.childCount>=3){
            Destroy(parentObject.GetChild(0).gameObject);
        }
        GameObject newObject=Instantiate(dreamNPC[idx[currIdx]]) as GameObject; //gameObject 생성
        newObject.name=newObject.name.Substring(0,newObject.name.IndexOf('('));
        newObject.transform.GetComponent<AreaScript>().CharacterText.text=txt[currIdx-1];
        newObject.transform.SetParent(parentObject,false); //현재 클릭된 오브젝트의 부모 위로 설정해주기.
    }
}
