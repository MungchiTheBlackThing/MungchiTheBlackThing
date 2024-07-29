using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;



public class Idle : State
{
    [SerializeField]
    Dictionary<DotAnimState, List<float>> IdlePos;

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public Idle()
    {
        IdlePos = new Dictionary<DotAnimState, List<float>>();
        StateReader reader = new StateReader();
        reader.ReadJson(this, Resources.Load<TextAsset>("FSM/IdleState"));
    }

    public override void Init(DotAnimState state, List<float> pos)
    {
        IdlePos.Add(state, pos);
    }
    public override void Enter(DotController dot)
    {
        //Idle이 갈 수 있는 Position에 대해 정의
    }
    //상태를 업데이트할 때마다 매 프레임 호출 -> 있을 필요 없음.
    public override void Execute(DotController dot)
    {

    }
    //상태를 나갈 때 1회 호출 -> Position -1로 변경
    public override void Exit(DotController dot)
    {

    }

    //임시 print용
    public override void Read()
    {
        foreach (var anim in IdlePos)
        {
            Debug.Log($"Animation: {anim.Key}, Positions: {string.Join(", ", anim.Value)}");
        }
    }
}
