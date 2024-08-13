using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FSM_Button : MonoBehaviour
{
    private DotController dotController;

    [SerializeField]
    string body;
    [SerializeField]
    string eyes;

    public void Start()
    {
        dotController = GameObject.Find("Dot").GetComponent<DotController>();
    }
    public void OnClick()
    {
        DotState dot = DotState.Main;

        if(dotController)
        {
            dotController.ChangeState(dot, body, -1, eyes);
        }

        //DotController을 가져와서
        //ChangeState(를 변경한다.
    }
}
