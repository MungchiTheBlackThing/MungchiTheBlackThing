using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DotAnimState
{
    anim_default = 0,
    anim_bed,
    anim_reading,
    anim_writing,
    anim_mold,
    anim_bounce,
    anim_laptop,
    anim_walking,
    anim_mold2,
    anim_happy,
    anim_spiderweb1,
    anim_spiderweb2,
    anim_eyesclosed,
    anim_eyescorner,
    anim_eyesdown,
    anim_eyesside,
    anim_eyesup,
    anim_sleepy_bed,
    anim_sleepy_spiderweb,
    anim_mud_ch1,
    anim_mud_ch2,
    anim_mud_ch3,
    anim_mud_ch4,
    anim_mud_ch5,
    anim_mud_ch6,
    anim_mud_ch7,
    anim_mud_ch8,
    anim_mud_ch9,
    anim_mud_ch10,
    anim_mud_ch11,
    anim_mud_ch12,
    anim_mud_ch13,
    anim_mud_ch14,
    anim_eyeswide, /*Idle*/
    anim_eyesblink,
    anim_eyesclosed_turn,
    anim_eyescorner_turn,
    anim_eyesdown_turn,
    anim_eyesside_turn,
    anim_eyesup_turn,
    anim_eyeswide_turn,
    anim_sub_wish,
    anim_sub_birdanddot,
    anim_sub_birdnest,
    anim_sub_caterpillar1,
    anim_sub_caterpillar2,
    anim_sub_deadbug1,
    anim_sub_deadbug2,
    anim_sub_dottoflamingo,
    anim_sub_flamingotodot,
    anim_sub_flamingo,
    anim_sub_dreamcatcher1,
    anim_sub_dreamcatcher2,
    anim_sub_roundthings1,
    anim_sub_roundthings2,
    anim_sub_spiderdot,
    anim_sub_raggedyann1,
    anim_sub_raggedyann2,
    anim_sub_raggedyann3,
    anim_sub_hands,
    anim_sub_heart,
    anim_sub_hurt1,
    anim_sub_hurt2,
    anim_sub_letter,
    anim_move,
    anim_idle, /*Sub*/
    anim_diary,
    anim_sleep,
    anim_watching,  /*Phase*/
    anim_trigger_play /*Trigger*/
}
//추상 클래스로 선언
//Enter 입장 시
// 실행 
//나가
public abstract class State
{
    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택

    public abstract void Init(DotAnimState state, List<float> pos); //해당 상태 초기화를 위해서 필요하다.
    public abstract void Enter(DotController dot);
    //상태를 업데이트할 때마다 매 프레임 호출 -> 있을 필요 없음.
    public abstract void Execute(DotController dot);
    //상태를 나갈 때 1회 호출 -> Position -1로 변경
    public abstract void Exit(DotController dot);

    //임시 데이터 읽기용
    public abstract void Read();
}
