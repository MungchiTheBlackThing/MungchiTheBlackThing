using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FSM_Button : MonoBehaviour
{
    private DotController dotController;

    public void Start()
    {
        dotController = GameObject.Find("Dot").GetComponent<DotController>();
    }
    public void OnClick(string state)
    {
        DotAnimState dotState;

        DotState dot = DotState.Idle;

        if(dotController)
        {
            dotController.ChangeState(dot, state);
        }

        //DotController을 가져와서
        //ChangeState(를 변경한다.
    }
}
