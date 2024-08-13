using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
   
    private int position;
    private string dotExpression; //CSV에 의해서 string 들어옴
    private string animKey; //CSV에 의해서 string으로 들어옴 파싱 해줘야한다.

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

    public string AnimKey
    {
        get { return animKey; }
        set { animKey = value; }
    }

    public string DotExpression
    {
        get { return dotExpression; }
        set { dotExpression = value; }
    }

    void Start()
    {
        states = new Dictionary<DotState, State>();
        states.Clear();
        states.Add(DotState.Idle, new Idle());
        //states.Add(DotState.Phase, new Phase());
        states.Add(DotState.Main, new Main());

        animatior = GetComponent<Animator>();

        Position = -1;
        dotExpression = "";
        chapter = 1;
        ChangeState(DotState.Main, "body_bounce");
    }

    public void ChangeState(DotState state = DotState.Idle, string OutAnimKey = "", int OutPos = -1, string OutExpression = "")
    {

        if (states.ContainsKey(state) == null)
        {
            return;
        }

        if (currentState != null)
        {
           currentState.Exit(this); //이전 값을 나가주면서, 값을 초기화 시킨다.
        }

        animatior.SetInteger("DotState", (int)state); //현재 상태를 변경해준다.
        position = OutPos; //이전 위치를 초기화함, 그렇게 하면 모든 상태로 입장했을 때 -1이 아니여서 랜덤으로 뽑지않는다.
        dotExpression = OutExpression; //Update, Main에서만 사용하기 때문에 다른 곳에서는 사용하지 않음.
        animKey = OutAnimKey; 

        //OutPos 가 있다면 해당 Position으로 바껴야함.
        currentState = states[state];
        currentState.Enter(this); //실행
    }
}
