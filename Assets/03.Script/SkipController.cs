using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipController : MonoBehaviour
{
    // Start is called before the first frame update
    //SkipController가 시간을 보관한다.

    float[] _timeStamp={3600f,7200f,1800f};

    [SerializeField]
    GameObject alter;
    [SerializeField]
    TMP_Text _timeText;

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
        if(curIdx==0)
            _objManager.Close();
        if((curIdx+1)>=3)
        {
            _player.SetChapter();
            _objManager.transChapter(_player.GetChapter());
        }
        curIdx=(curIdx+1)%3;
        time=_timeStamp[curIdx];
        //ObjectManager에게 전달.
    }

    public void NoBut()
    {
        alter.SetActive(false);
    }
}
