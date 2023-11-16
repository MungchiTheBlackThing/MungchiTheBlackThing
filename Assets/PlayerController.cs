using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInfo player;
    //player 접속 경과 시간
    float _elapsedTime; 

    //임시 저장을 위한 serialize..
    [SerializeField]
    string nickname;

    // Start is called before the first frame update
    void Start()
    {
        //앞으로 player을 동적으로 생성해서 관리할 예정.. 아직은 미리 초기화해서 사용한다.
        player = new PlayerInfo(0,nickname,0);
    }

    // Update is called once per frame
    //1시간이 되었는지 체크하기 위해서 저정용도
    void Update()
    {
        _elapsedTime+=Time.deltaTime; 
    }

    public void EntryGame(DateTime dateTime)
    {
        if(player!=null){
            player.Datetime=dateTime;
        }
    
    }
}
