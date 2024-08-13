using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    GameObject dotEyes;
    Animator dotEyesAnim; // 눈 애니메이터도 가지고 있는다.

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public Main()
    {
        MainPos = new Dictionary<DotAnimState, List<float>>();
        reader.ReadJson(this, Resources.Load<TextAsset>("FSM/MainState"));

        //1. 꺼져있는 자식 중 Eyes를 찾아서 dotEyes에 대입해 놓는다.
        dotEyes = GameObject.Find("Dot").transform.Find("DotEyes").gameObject;
        dotEyesAnim = dotEyes.GetComponent<Animator>();
    }

    public override void Init(DotAnimState state, List<float> pos)
    {
        MainPos.Add(state, pos);
    }
    public override void Enter(DotController dot)
    {
        //2. eyes를 킨다.
        dotEyes.SetActive(true);

        DotEyes eyes;
       
        if (Enum.TryParse(dot.DotExpression, true, out eyes))
        {
            dotEyesAnim.SetInteger("FaceKey", (int)eyes);
        }

        DotAnimState anim;
        if (Enum.TryParse(dot.AnimKey, true, out anim))
        {
            dot.Animator.SetInteger("DotAnimState", (int)anim); //애니메이션 업데이트
        }

        //dot.transform.localPosition = GetCoordinate(dot.Position); //위치 업데이트

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
