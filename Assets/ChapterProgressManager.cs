using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterProgressManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> phaseIngUI;
    [SerializeField]
    List<GameObject> phaseEdUI;
    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text sentence;
    
    int chapter=1;
    int playerChapter=1;
    public int Chapter { 
        get { return chapter; }
        set { chapter=value; }
    }

    private void OnEnable() {
    }

    public void PassData(string title,int clickedChapter,int chapter,int curPhase)
    {
        this.title.text=title;
        this.chapter=clickedChapter;
        this.playerChapter=chapter;

        //켜질 때 현재 chapter값보다 작으면
        if(this.chapter<this.playerChapter)
        {
            Debug.Log("작으면, 모든 파리 ing ed true");

            for(int i=0;i<phaseEdUI.Count;i++) 
            {
                phaseEdUI[i].SetActive(true);
            }
            for(int i=0;i<phaseIngUI.Count;i++) 
            {
                phaseIngUI[i].SetActive(true);
            }
        }
        else
        {
            //Player Phase 단계에 따라서 진행.
            for(int i=0;i<=curPhase;i++)
            {
                phaseIngUI[i].SetActive(true);
            }
            for(int i=0;i<curPhase;i++)
            {
                phaseEdUI[i].SetActive(true);
            }
        }

        Debug.Log($"현재 클릭된 챕터 {this.chapter}와 플레이어의 챕터 {this.playerChapter} 그리고 현재 Phase{curPhase}");
    }

    private void OnDisable() {
        
        for(int i=0;i<phaseEdUI.Count;i++) 
        {
            phaseEdUI[i].SetActive(false);
        }
        for(int i=0;i<phaseIngUI.Count;i++) 
        {
            phaseIngUI[i].SetActive(false);
        }
    }
}
