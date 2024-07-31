using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub : State
{
    [SerializeField]
    Dictionary<DotAnimState, List<float>> SubPos;

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public Sub()
    {
        SubPos = new Dictionary<DotAnimState, List<float>>();
        reader.ReadJson(this, Resources.Load<TextAsset>("FSM/SubState"));
    }

    public override void Init(DotAnimState state, List<float> pos)
    {
        SubPos.Add(state, pos);
    }
    public override void Enter(DotController dot)
    {

    }

    //상태를 나갈 때 1회 호출 -> Position -1로 변경
    public override void Exit(DotController dot)
    {
    }

    //임시 print용
    public override void Read()
    {
    }
}
