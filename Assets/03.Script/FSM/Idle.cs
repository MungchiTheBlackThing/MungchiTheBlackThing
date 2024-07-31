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
        //dot의 position이 지정되어있는가 확인한다. -1은 지정되지 않음, n은 지정
        //지정된 경우, IdlePos[animKey][position]을 동작한다(애니메이션 상태전환).
        if(dot.Position == -1)
        {
            int maxIdx = IdlePos[dot.AnimKey].Count;
           
            dot.Position = Random.Range(0, maxIdx);
        }

        //dot의 animKey를 가져온다.
        //animKey의 저장된 List<float> Length 값 중 Random.Range 함수를 사용해서 뽑는다.
        //IdlePos[animKey][position]을 동작한다(애니메이션 상태전환).
        Debug.Log($"Animation: {dot.AnimKey}, Positions: {string.Join(", ", dot.Position)}");

        dot.transform.localPosition = GetCoordinate(dot.Position); //위치 업데이트
        dot.Animator.SetInteger("DotAnimState", (int)dot.AnimKey); //애니메이션 업데이트

        if (dot.AnimKey == DotAnimState.anim_mud)
        {
            //챕터를 파악해서, mold를 변경시킬 때 사용.
            dot.Animator.SetInteger("Chapter", dot.Chapter);
        }
    }

    //상태를 업데이트할 때마다 매 프레임 호출 -> 있을 필요 없음.
    public override void Execute(DotController dot)
    {

    }

    //상태를 나갈 때 1회 호출 -> Position -1로 변경
    public override void Exit(DotController dot)
    {
        //나갈 때 위치를 -1로 바꾼다.
        dot.Position = -1;
    }

    //임시 print용
    public override void Read()
    {
        /*foreach (var anim in IdlePos)
        {
            //Debug.Log($"Animation: {anim.Key}, Positions: {string.Join(", ", anim.Value)}");
        }*/
    }
}

