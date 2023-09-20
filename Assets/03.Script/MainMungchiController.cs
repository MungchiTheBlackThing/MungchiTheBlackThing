using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMungchiController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuBar;
    [SerializeField]
    GameObject mainDialogue;
    string currTime;
   
    
    void Start()
    {
        for (int i=0;i<this.gameObject.transform.childCount;i++){
            GameObject childs=this.gameObject.transform.GetChild(i).gameObject;
            if(childs.activeSelf){
                currTime=childs.gameObject.name;
                break;
            }
        }
    }

    public void onClick(){
        MenuBar.SetActive(false);
        mainDialogue.SetActive(true);
        if(currTime!=null){
            GameObject select=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            GameObject child=mainDialogue.transform.Find(currTime).gameObject;
            select.gameObject.GetComponent<MungchiPos>().onDialogue(child);
            child.SetActive(true);
        }
        
    }


    //그냥 버튼이랑 스크립트를 붙여서 instantiate하자 ^^,,,
}
