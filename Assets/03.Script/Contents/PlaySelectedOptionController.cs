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
        for(int i=0;i<selected.Length;i++)
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
}
