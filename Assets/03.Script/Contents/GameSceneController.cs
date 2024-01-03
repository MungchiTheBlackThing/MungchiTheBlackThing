using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{

    Transform DialogueUI;

    void Start(){
        DialogueUI=GameObject.Find("Canvas").transform;
    }
    public void exit(){
        for(int i=0;i<DialogueUI.childCount;i++)
        {
            DialogueUI.GetChild(i).gameObject.SetActive(true);
        }
        Destroy(this.gameObject);
        //현재 뭉치 GameObject를 삭제해야함.
        Destroy(GameObject.FindWithTag("Binocular").gameObject);//삭제하는데, Click_Alarm을 꺼줘야함.
    }
}
