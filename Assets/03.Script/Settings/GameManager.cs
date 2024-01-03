using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Assets.Script.TimeEnum;
public class GameManager : MonoBehaviour
{

    static MemoryPool _memory=new MemoryPool();
    bool isChapterUpdate=true;
    ObjectManager _objManager;
    PlayerController _player; //현재 플레이어의 컨트롤러 
    const int setWidth=2796;
    const int setHeight=1920;

    [SerializeField]
    GameObject _background;
    //현재 시간을 보여주기 위한 UI (Test용)
    GameObject _currTimeBackground; //현재 배경화면에 대한 데이터 정보 
    //현재 해당하는 시간 저장하는 list

    void Start(){
        SetPlayer(); //캐릭터 생성
        InitBackground(); //현재 시간에 따른 배경 생성
    }
    
    void SetPlayer()
    {
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //현재 시간을 체크한다. -> player controller에게 현재 시간 전달 
        if(_player!=null)
            _player.EntryGame(DateTime.Now);
    }
    void InitBackground()
    {
        //현재 시만 가져온다. 
        Int32 hh=Int32.Parse(DateTime.Now.ToString(("HH"))); //현재 시간을 가져온다
        GameObject[] backgrounds=Resources.LoadAll<GameObject>("Background/");
        if(_objManager==null)
            _objManager=_background.GetComponent<ObjectManager>();

        for(int i=0;i<backgrounds.Length;i++)
        {
            if(hh>=(int)STime.T_DAWN&&hh<(int)STime.T_MORNING)
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_DAWN],_background.transform);
                break;
            }
            if(hh<(int)STime.T_EVENING)
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_MORNING],_background.transform);
                break;
            }
            else if(hh<(int)STime.T_NIGHT) 
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_EVENING],_background.transform);
                break;
            }else
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_NIGHT],_background.transform);
                break;
            }
        }
        _objManager.transChapter(_player.GetChapter()); //object manager에게 플레이어의 지금 챕터 전달하여, 배경화면 변경.
    }
    void SetResolution(){
        Camera.main.transform.position=new Vector3(0,0,-10f);
        //현 기기의 너비와 높이
        int deviceWidth=Screen.width;
        int deviceHeight=Screen.height;
        //기기의 해상도 비율을 비로 조절

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기
        if ((float)setWidth / setHeight >= (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        Camera.main.aspect=Screen.width/Screen.height;
        Camera.main.ResetAspect();
    }

    public void SetChapter()
    {
        if(_player!=null)
        {
            _player.SetChapter();
            _objManager.transChapter(_player.GetChapter());
        }
    }
    //플레이어가 초기에 들어올 때 현재 day에 따라 배경화면을 리셋한다.
    void Update()
    {
        SetResolution();
    }
}
