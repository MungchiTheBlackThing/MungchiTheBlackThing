using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMungchiController : MonoBehaviour
{
    [SerializeField]
    GameObject MenuBar;

    public void onClick(){
        MenuBar.SetActive(false);

        for(int i=0;i<this.gameObject.transform.childCount;i++){
            GameObject child =this.gameObject.transform.GetChild(i).gameObject;
            Debug.Log(child.name);
            if(child.name.Contains("mainDialogue")){
                child.SetActive(true);
            }
        }
        GameObject select=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        select.gameObject.GetComponent<MungchiPos>().onDialogue();
        
    }

    //그냥 버튼이랑 스크립트를 붙여서 instantiate하자 ^^,,,
}
