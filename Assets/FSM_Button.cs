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
        if(Enum.TryParse(state, true, out dotState))
        {
            DotState dot = DotState.Idle;

            if(dotController)
            {
                dotController.ChangeState(dot, dotState);
            }

        }

        //DotController을 가져와서
        //ChangeState(를 변경한다.
    }
}
