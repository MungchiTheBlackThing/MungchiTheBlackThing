using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{

    Transform DialogueUI;

    void Start(){
        Transform parent=GameObject.Find("Dialogue").transform;
        DialogueUI=GameObject.Find("Dialogue").transform.GetChild(0);
    }
    public void exit(){
        DialogueUI.gameObject.SetActive(true);
        Destroy(this.gameObject);
        //현재 뭉치 GameObject를 삭제해야함.
        Destroy(GameObject.FindWithTag("Binocular").gameObject);//삭제하는데, Click_Alarm을 꺼줘야함.
    }
}
