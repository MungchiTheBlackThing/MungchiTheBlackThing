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
    Phase = 3
};

public class DotController : MonoBehaviour
{
    //private State[] states; //모든 상태
    private State currentState; //현재 상태
    private Dictionary<DotState, State> states;

    DotAnimState animKey;
    int Position;

    void Start()
    {
        states = new Dictionary<DotState, State>();
        states.Clear();
        states.Add(DotState.Idle, new Idle());

        currentState = states[DotState.Idle];
        Position = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(DotState state)
    {
        
    }

    void UpdateState()
    {
        //상태에 따른 동작을 수행
        //델리게이트로 못만드나?
    }
}
