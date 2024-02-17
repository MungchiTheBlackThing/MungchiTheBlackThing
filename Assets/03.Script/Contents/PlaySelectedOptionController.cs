using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlaySelectedOptionController : MonoBehaviour
{
    [SerializeField]
    GameObject []selected;
    [SerializeField]
    TMP_Text DotsText;
    bool isSleep=false;

    [SerializeField]
    GameObject poems;

    [SerializeField]
    GameObject afterPlay;

    public bool IsSleep { get=>isSleep;}

    void OnEnable()
    {
        //선택지 다 끄기
        for (int i=0;i<selected.Length;i++)
            selected[i].SetActive(true);
    }
    public void PlayPoem()
    {
        isSleep=false;
        Instantiate(poems,transform.parent.parent.parent.parent);
        //Poem 장면 on
    }
    public void Sleep()
    {
        //선택지 다 끄기
        isSleep=true;
        for(int i=0;i<selected.Length;i++)
            selected[i].SetActive(false);
        
        DotsText.text="응.. 알겠어. 나는... 자러 갈게.\n 절대 아쉽지 않아. 괜찮아!";
        StartCoroutine(CloseAlter(this.transform.parent.gameObject));
    }

    IEnumerator CloseAlter(GameObject obj){
        yield return new WaitForSeconds(2f);
        afterPlay.SetActive(true);
        obj.SetActive(false);
    }
    //시를 보고 자러 갈때
    public void Sleep2()
    {
        //선택지 다 끄기
        isSleep = true;
        for (int i = 0; i < selected.Length; i++)
            selected[i].SetActive(false);

        DotsText.text = "나... 눈이 무거워지고 있어.<br>이제 거미줄에 안겨들 시간이야.";
        StartCoroutine(CloseAlter(this.transform.parent.gameObject));
    }
}
