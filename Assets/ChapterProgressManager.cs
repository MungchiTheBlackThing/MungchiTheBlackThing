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
    
    ChapterInfo chapterInfo;
    PlayerController player;
    
    public void PassData(ChapterInfo chapterInfo, PlayerController player)
    {
        this.title.text=chapterInfo.title;
        this.sentence.text=chapterInfo.loadText;
        this.player=player;

        //켜질 때 현재 chapter값보다 작으면
        if(chapterInfo.id<this.player.GetChapter())
        {
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
            for(int i=0;i<=this.player.GetAlreadyEndedPhase();i++)
            {
                if(phaseIngUI.Count<=i) continue;
                phaseIngUI[i].SetActive(true);
            }
            for(int i=0;i<this.player.GetAlreadyEndedPhase();i++)
            {
                if(phaseEdUI.Count<=i) continue;
                phaseEdUI[i].SetActive(true);
            }
        }

        Debug.Log($"현재 클릭된 챕터 {chapterInfo.id}와 플레이어의 챕터 {this.player.GetChapter()} 그리고 현재 Phase{this.player.GetAlreadyEndedPhase()}");
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
