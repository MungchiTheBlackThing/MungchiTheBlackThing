using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class DialogueA2: MonoBehaviour
{
    public List<GameObject> Dials = new List<GameObject>();
    public int Current = 0;
    public int EndDial;
    public VideoPlayer vid;
    public bool conditionmet = false;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.transform;
        EndDial = transform.childCount - 1;
        Recursion(0);
    }

    void Recursion(int Current)
    {
        vid = transform.GetChild(Current).GetChild(1).GetComponent<VideoPlayer>();
        vid.loopPointReached += NextAni;
    }

    void NextAni(UnityEngine.Video.VideoPlayer vid)
    {
        Transform transform = this.transform;
        if (Current == EndDial)
        {
            return;
        }
        //컨디션이 그 동영상에 기믹(?)을 추가해야해서 동영상을 잠깐 멈춰야함
        if (transform.GetChild(Current).CompareTag("Condition"))
        {
            //while (!conditionmet)
            //{
            //    if (CheckCondition())
            //    {
            //        conditionmet = true;
            //    }
            //}

            /* while 문 쓰면 멈춤 ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ */
        }
        transform.GetChild(Current).gameObject.SetActive(false);
        Current++;
        transform.GetChild(Current).gameObject.SetActive(true);
        Recursion(Current);
        conditionmet = false;
    }

    bool CheckCondition()
    {
        return false;
    }
}
