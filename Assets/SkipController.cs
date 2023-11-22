using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipController : MonoBehaviour
{
    // Start is called before the first frame update
    //SkipController가 시간을 보관한다.

   // const float[] _timeStamp={};
    GameObject alter;
    [SerializeField]
    TMP_Text _timeText;

    PlayerController _player;
    void Start()
    {
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        for(int i=0;i<transform.childCount;i++)
        {
            
            GameObject tmp=this.transform.GetChild(i).gameObject;

            if(tmp.activeSelf==false)
            {
                alter=tmp;
            }
        }
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
