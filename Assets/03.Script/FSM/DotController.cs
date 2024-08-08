using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

//뭉치가 할 수 있는 애니메이션의 열거형
public enum DotState
{
    Idle = 0,
    Sub = 1,
    Main = 2,
    Phase = 3,
    Tirgger = 4
};

public class DotController : MonoBehaviour
{
    //private State[] states; //모든 상태
    private State currentState; //현재 상태
    private Dictionary<DotState, State> states;
    private DotAnimState animKey;
    private int position;

    [SerializeField]
    private int chapter;

    [SerializeField]
    private Animator animatior;

    public Animator Animator
    { get { return animatior; } }

    public int Chapter
    { 
        get { return chapter; }
        set { chapter = value; }
    }

    public int Position
    {
        get { return position; }
        set { position = value; }
    }

    public DotAnimState AnimKey
    {
        get { return animKey; }
        set { animKey = value; }
    }

    void Start()
    {
        states = new Dictionary<DotState, State>();
        states.Clear();
        states.Add(DotState.Idle, new Idle());
        states.Add(DotState.Phase, new Phase());
        //states.Add(DotState.Sub, new Sub());

        animatior = GetComponent<Animator>();

        Position = -1;
        chapter = 1;
        ChangeState(DotState.Phase, DotAnimState.anim_diary);
    }

    public void ChangeState(DotState state = DotState.Idle, DotAnimState OutAnimKey = DotAnimState.anim_default, int OutPos = -1)
    {
        Debug.Log(1);

        if (states.ContainsKey(state) == null)
        {
            return;
        }

        Debug.Log(2);

        if (currentState != null)
        {
           currentState.Exit(this); //이전 값을 나가주면서, 값을 초기화 시킨다.
        }

        animatior.SetInteger("DotState", (int)state);
        Position = OutPos; //Update
        animKey = OutAnimKey; 
        //OutPos 가 있다면 해당 Position으로 바껴야함.
        currentState = states[state];
        currentState.Enter(this); //실행
    }
}
