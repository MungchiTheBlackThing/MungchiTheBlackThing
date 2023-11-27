using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Script.TimeEnum;
public class SkipController : MonoBehaviour
{
    // Start is called before the first frame update
    //SkipController가 시간을 보관한다.

    float[] _timeStamp={3600f,7200f,1800f};

    [SerializeField]
    GameObject alter;
    [SerializeField]
    TMP_Text _timeText;
    [SerializeField]
    GameObject eventPlay;
    int curIdx=0;
    float time=0;
    PlayerController _player;
    const int HOUR=3600;
    const int MIN=60;

    ObjectManager _objManager;
    void Start()
    {
        _objManager=GameObject.FindWithTag("ObjectManager").GetComponent<ObjectManager>();
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        time=_timeStamp[(curIdx)%3];
    }

    void Update()
    {
        //초 단위 3600을 나누면 h나옴 
        //1분 60
        //1시간 3600초 
        //((현재 시간)%3600)/60
        int hour=(int)time/HOUR; //3600초 나눔
        int min =((int)time%HOUR)/MIN;
        _timeText.text=(hour).ToString()+"h "+(min).ToString()+"m";
        time-=Time.deltaTime;

        if(time<=0)
        {
            if(curIdx==0)
                _objManager.Close();
            curIdx++;
            //ObjectManager에게 전달.
        }
    }

    public void ClickSkipBut()
    {
        if(alter!=null)
        {
            alter.SetActive(true);
        }
    }

    public void YesBut()
    { 
        //player 시간을 빠르게 만든다.
        alter.SetActive(false);

        switch(curIdx)
        {
            case (int)TimeStamp.TS_WATCHING:
                _objManager.Close();
                ++curIdx;
                time=_timeStamp[curIdx];
            break;
            case (int)TimeStamp.TS_THINKING:
                ++curIdx;
                time=_timeStamp[curIdx];
            break;
            case (int)TimeStamp.TS_WRITING:
            //시잃기 시작.
            Instantiate(eventPlay,_objManager.gameObject.transform);

            //애니메이션 생성
            DateTime today=DateTime.Now; //현재 지금 시간
            string format=string.Format("{0} 13:00:00",DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
            DateTime tomorrow=Convert.ToDateTime(format); //format 변환
            TimeSpan disTime=tomorrow-today; //두 차이를
            time=(float)disTime.TotalSeconds; //초로 변환함
            curIdx++;
            break;
            case (int)TimeStamp.TS_SLEEPING:
            _player.SetChapter();
            _objManager.transChapter(_player.GetChapter());

            //현재 시간을 가져와서, 다음날 한시까지 계산을 한다.
            curIdx=0;
            time=_timeStamp[curIdx];
            break;
        }
        //ObjectManager에게 전달.
    }

    public void NoBut()
    {
        alter.SetActive(false);
    }
}
