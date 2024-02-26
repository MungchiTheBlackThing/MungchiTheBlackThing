using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Page : MonoBehaviour
{
    public TMP_Text textMesh;
    public GameObject Diary;
    public int currentpage = 0;
    public int totalpage;
    public Button nextButton;
    public Button preButton;
    public Button cancelButton;
    public GameObject Mungchi;
    int chapter =1;
    // Start is called before the first frame update
    void OnEnable()
    {
        chapter = PlayerController._player.CurrentChapter - 1;
        currentpage = 0;
        //시 내용을 업데이트 한다.
        if(currentpage<DialogueDataAsset.poemsData.poems[chapter].text.Count)
            textMesh.text = DialogueDataAsset.poemsData.poems[chapter].text[currentpage].textKor;
    }

    void Start()
    {
        totalpage = DialogueDataAsset.poemsData.poems[chapter].text.Count-1;
        this.nextButton.onClick.AddListener(() => { this.NextPage(); });
        this.preButton.onClick.AddListener(() => { this.PrePage(); });
        cancelButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentpage == totalpage)
        {
            nextButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(true);
        }
        if (currentpage <= 0)
        {
            preButton.gameObject.SetActive(false);
        }
    }
    
    public void NextPage()
    {
        preButton.gameObject.SetActive(true);
        currentpage++;
        if(currentpage<DialogueDataAsset.poemsData.poems[chapter].text.Count)
            currentpage = DialogueDataAsset.poemsData.poems[chapter].text.Count - 1;
        
        textMesh.text = DialogueDataAsset.poemsData.poems[chapter].text[currentpage].textKor;
    }

    public void PrePage()
    {
        nextButton.gameObject.SetActive(true);
        currentpage--;
        if(currentpage<0)
            currentpage=0;
       
        textMesh.text = DialogueDataAsset.poemsData.poems[chapter].text[currentpage].textKor;
    }
}
