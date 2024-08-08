using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public enum DotAnimState
{
    anim_default = 0, //1
    anim_bed, //2
    anim_reading, //3
    anim_writing, //4
    anim_mold, //5
    anim_bounce, //6
    anim_laptop, //7
    anim_walking, //8
    anim_mold2, //9
    anim_happy, //10
    anim_spiderweb1, //11
    anim_spiderweb2, //12
    anim_eyesclosed,//13
    anim_eyescorner,//14
    anim_eyesdown,//15
    anim_eyesside,//16
    anim_eyesup,//17
    anim_sleepy_bed,//18
    anim_sleepy_spiderweb, //19
    anim_mud, //DotAnimState 20 - Chapter 
    anim_eyeswide, /*Idle 20*/ 

    anim_eyesblink, //21
    anim_eyesclosed_turn, //22
    anim_eyescorner_turn, //23
    anim_eyesdown_turn, //24
    anim_eyesside_turn, //25
    anim_eyesup_turn, //26
    anim_sub_wish, //27
    anim_sub_birdanddot, //28
    anim_sub_birdnest, //29
    anim_sub_caterpillar1, //30
    anim_sub_caterpillar2, //31
    anim_sub_deadbug1, //32
    anim_sub_deadbug2, //33
    anim_sub_dottoflamingo, //34
    anim_sub_flamingotodot, //35
    anim_sub_flamingo, //36
    anim_sub_dreamcatcher1, //37
    anim_sub_dreamcatcher2, //38
    anim_sub_roundthings1, //39
    anim_sub_roundthings2, //40
    anim_sub_spiderdot, //41
    anim_sub_raggedyann1, //42
    anim_sub_raggedyann2, //43
    anim_sub_raggedyann3, //44
    anim_sub_hands, //45
    anim_sub_heart, //46
    anim_sub_hurt1, //47
    anim_sub_hurt2, //48
    anim_sub_letter, //49
    anim_move, //50
    anim_idle, //51
    anim_sub_ch_heart, //52
    anim_sub_light, /*Sub 53*/

    anim_diary,
    anim_sleep,
    anim_watching,  /*Phase*/
    anim_trigger_play, /*Trigger*/
    body_default1, /*Main Body*/
    body_default2,
    body_default2_turn,
    body_bounce,
    body_move,
    body_rhythm,
    body_spikey,
    body_spikey_turn,
    body_hmm,
    body_hmm_turn,
    body_mud,
    body_mud_turn,
    body_trembling,
    body_ch2,
    body_ch2tremble,
    body_ch2drop,
    body_draw,
    body_draw2,
    body_cup,
    body_cu2,
    //몽퉁
}
//추상 클래스로 선언
//Enter 입장 시
// 실행 
//나가
[System.Serializable]
public class DotData
{
    public float dotPosition;
    public int X;
    public int Y;
}

[System.Serializable]
public class Coordinate
{
    public List<DotData> data;
}
public abstract class State
{
    static protected Dictionary<float, Vector2> position; //State 클래스 1개에 모두 공유할 수 있도록 함.
    static protected StateReader reader;
    public Vector2 GetCoordinate(float idx) { return position[idx]; }

    public State()
    {
        reader = new StateReader();
        position = new Dictionary<float, Vector2>();
        ReadJson();
    }

    void ReadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("FSM/DotPosition");
        Coordinate dotData = JsonUtility.FromJson<Coordinate>(jsonFile.text);

        // Example usage: Print all dot positions
        foreach (var Data in dotData.data)
        {
            Vector2 vector = new Vector2(Data.X, Data.Y);
            position.Add(Data.dotPosition, vector);
            Debug.Log($"Dot Position: {Data.dotPosition}, X: {Data.X}, Y: {Data.Y}");
        }
    }

    //상태를 시작할 때 1회 호출 -> Position 랜덤으로 선택
    public abstract void Init(DotAnimState state, List<float> pos); //해당 상태 초기화를 위해서 필요하다.
    public abstract void Enter(DotController dot);
    //상태를 나갈 때 1회 호출 -> Position -1로 변경
    public abstract void Exit(DotController dot);

    //임시 데이터 읽기용
    public abstract void Read();
}
