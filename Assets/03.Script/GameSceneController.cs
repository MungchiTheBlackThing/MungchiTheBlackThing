using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{

    Transform DialogueUI;

    void Start(){
        Transform parent=this.gameObject.transform.parent;

        for(int i=0;i<parent.childCount;i++){
            if(parent.GetChild(i).name.Contains("Dialogue")){
                DialogueUI=parent.GetChild(i);
            }
        }
    }
    public void exit(){
        DialogueUI.gameObject.SetActive(true);
        Destroy(this.gameObject);
        //현재 뭉치 GameObject를 삭제해야함.
        Destroy(GameObject.FindWithTag("Binocular").gameObject);//삭제하는데, Click_Alarm을 꺼줘야함.
    }
}
