using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{

    GameObject binocular;
    Transform DialogueUI;

    void Awake()
    {
        binocular=GameObject.FindWithTag("Binocular").gameObject;
    }
    void Start()
    {
        DialogueUI=GameObject.Find("Canvas").transform;
    }
    public void exit(){
        for(int i=0;i<DialogueUI.childCount;i++)
        {
            if(DialogueUI.GetChild(i).gameObject.name.Contains("skip")==false)
                DialogueUI.GetChild(i).gameObject.SetActive(true);
        }
        Destroy(binocular);//삭제하는데, Click_Alarm을 꺼줘야함
        Destroy(this.gameObject);
    }
}
