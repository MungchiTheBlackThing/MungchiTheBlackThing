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

    PlayerController _player;
    void Start()
    {
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //초 단위 3600을 나누면 h나옴 
        //분 : 60을 나누면 m나옴
        float time=_player.GetTime();
        _timeText.text=((int)(time/3600)).ToString()+"h "+((int)(time/60)).ToString()+"m";
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
    }

    public void NoBut()
    {
        alter.SetActive(false);
    }
}
