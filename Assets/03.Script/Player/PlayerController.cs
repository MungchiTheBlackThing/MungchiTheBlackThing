using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    PlayerInfo _player;
    //player 접속 경과 시간
    float _elapsedTime; 

    //임시 저장을 위한 serialize..
    [SerializeField]
    string nickname;

    const float _passTime=1800f; //30분을 기준으로 한다.
    // Start is called before the first frame update
    private void Awake()
    {
        //앞으로 player을 동적으로 생성해서 관리할 예정.. 아직은 미리 초기화해서 사용한다.
        _player = new PlayerInfo(0,nickname,1);
    }

    // Update is called once per frame
    //1시간이 되었는지 체크하기 위해서 저정용도
    void Update()
    {
        _elapsedTime+=Time.deltaTime; 
    }

    public float GetTime()
    {
        return _elapsedTime;
    }

    public void SetChapter()
    {
        _player.CurrentChapter+=1;
    }
    //시간 설정 : (현재 시간 - watching이 진행된 시간)+60분
    public void PassWathingTime() 
    {
        //현재 진행시간에 60분을 더한다.
        //Time.deltaTime => 1초 
        //1분 => 60초
        //60분 => 60*60 => 3600초
        //30분 => 60*30 => 1800초
        //120분 => 60*120 => 7200초
        _elapsedTime+=(_passTime*2); //1시간 Update
    }
    public void PassWriting()
    {
        _elapsedTime+=(_passTime);
    }
    public void PassThinkingTime()
    {
        _elapsedTime+=(_passTime*4); //2시간 1800*4 => 7200
    }
    public void EntryGame(DateTime dateTime)
    {
        if(_player!=null){
            _player.Datetime=dateTime;
        }
    }

    public int GetChapter()
    {
        return _player.CurrentChapter;
    }

    public string GetNickName()
    {
        return _player.Nickname;
    }

    public float GetAcousticVolume()
    {
        return _player.AcousticVolume;
    }
    public float GetMusicVolume()
    {
        return _player.AcousticVolume;
    }
}
