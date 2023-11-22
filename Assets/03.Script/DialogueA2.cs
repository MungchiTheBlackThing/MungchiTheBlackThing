using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class DialogueA2 : MonoBehaviour
{
    public List<GameObject> dialogues = new List<GameObject>();
    public RenderTexture rd;
    public int currentDialogueIndex = 0;
    private int endDialogueIndex;
    public int currentScript = 0;
    public int endScript;
    private VideoPlayer videoPlayer;
    private bool isstop = false;
    public bool SeeScript = false;
    const int setWidth = 2796;
    const int setHeight = 1920;

    void Start()
    {
        dialogues.Clear();
        foreach (Transform child in transform)
        {
            dialogues.Add(child.gameObject);
        }
        endDialogueIndex = transform.childCount - 1;
        InitializeDialogue(0);
    }

    void Update()
    {
        //조건에 걸렸을 경우(기믹 수행시 넘어가도록 작성해야함)
        if (Input.GetMouseButtonDown(0) && isstop)
        {
            Debug.Log("마우스 눌림");
            HandleNonCondition();
            isstop = false;
        }
        // 스크립트 대사 다 안봤을 경우(이거 어떻게 짜야함????????)
        if (Input.GetMouseButtonDown(0) && currentScript<endScript)
        {
            Debug.Log("마우스 눌림");
            dialogues[currentDialogueIndex].transform.GetChild(currentScript).gameObject.SetActive(false);
            currentScript ++;
            dialogues[currentDialogueIndex].transform.GetChild(currentScript).gameObject.SetActive(true);
        }
        if (currentScript == endScript)
        {
            SeeScript = true;
        }
        else
            SeeScript = false;

        SetResolution();
    }

    void InitializeDialogue(int index)
    {
        currentScript = 0;
        //스크립트 여러개 있을 경우 비디오 못찾는 문제 해결하기 위해서
        int last = dialogues[index].transform.childCount - 1;
        endScript = last - 1;
        //비디오 요소 가져오기
        videoPlayer = dialogues[index].transform.GetChild(last).GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoLoopPointReached;
    }

    void OnVideoLoopPointReached(UnityEngine.Video.VideoPlayer vp)
    {
        if (dialogues[currentDialogueIndex].CompareTag("Condition") || !SeeScript)
        {
            HandleCondition();
        }
        else
        {
            HandleNonCondition();
        }
    }

    void HandleCondition()
    {
        Debug.Log("Pause!");
        videoPlayer.Pause();
        isstop = true;
        Debug.Log("어디서 멈추나1");
    }

    void HandleNonCondition()
    {
        SetDialogueActive(false);
        currentDialogueIndex++;

        if (currentDialogueIndex <= endDialogueIndex)
        {
            SetDialogueActive(true);
            InitializeDialogue(currentDialogueIndex);
        }
        else
        {
            ClearOutRenderTexture(rd);
        }
    }

    void SetDialogueActive(bool active)
    {
        dialogues[currentDialogueIndex].gameObject.SetActive(active);
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }

    void SetResolution()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        //현 기기의 너비와 높이
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        //기기의 해상도 비율을 비로 조절

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기
        if ((float)setWidth / setHeight >= (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        Camera.main.aspect = Screen.width / Screen.height;
        Camera.main.ResetAspect();
    }

    bool CheckCondition()
    {
        return false; // You can implement your condition check here
    }
}