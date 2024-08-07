using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DotEyes
{
    face_null = 0,
    face_blink = 1,
    face_noblink = 2,
    face_squeeze = 3,
    face_close_turn,
    face_close,
    face_open,
    face_eyeroll,
    face_eyeroll2,
    face_eyeroll3,
    face_mad,
    face_staryeyes,
    face_thinking,
    face_omg,
    face_sad,
    face_draw,
    face_eyeroll3_stay
};

public class Main : State
{
    [SerializeField]
    Dictionary<DotAnimState, List<float>> MainPos;

    DotEyes dotEyes;

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public Main()
    {
        MainPos = new Dictionary<DotAnimState, List<float>>();
        reader.ReadJson(this, Resources.Load<TextAsset>("FSM/MainState"));
    }

    public override void Init(DotAnimState state, List<float> pos)
    {
        MainPos.Add(state, pos);
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
