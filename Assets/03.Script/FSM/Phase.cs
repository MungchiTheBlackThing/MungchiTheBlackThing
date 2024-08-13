using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : State
{
    [SerializeField]
    Dictionary<DotAnimState, List<float>> PhasePos;

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public Phase()
    {
        PhasePos = new Dictionary<DotAnimState, List<float>>();
        reader.ReadJson(this, Resources.Load<TextAsset>("FSM/PhaseState"));
    }

    public override void Init(DotAnimState state, List<float> pos)
    {
        PhasePos.Add(state, pos);
    }
    public override void Enter(DotController dot)
    {
        Debug.Log(2);

        Debug.Log(dot.AnimKey);

        DotAnimState anim;
        if (Enum.TryParse(dot.AnimKey, true, out anim))
        {
            dot.Animator.SetInteger("DotAnimState", (int)anim); //애니메이션 업데이트
        }
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
