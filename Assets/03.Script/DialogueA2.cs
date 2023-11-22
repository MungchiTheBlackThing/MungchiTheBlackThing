using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class DialogueA2 : MonoBehaviour
{
    public List<GameObject> dialogues = new List<GameObject>();
    private int currentDialogueIndex = 0;
    private int endDialogueIndex;
    private VideoPlayer videoPlayer;
    private bool isstop = false;

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
        if (Input.GetKeyDown(KeyCode.Space) && isstop)
        {
            Debug.Log("스페이스바 눌림");
            HandleNonCondition();
            isstop = false;
        }
    }

    void InitializeDialogue(int index)
    {
        videoPlayer = dialogues[index].transform.GetChild(1).GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoLoopPointReached;
    }

    void OnVideoLoopPointReached(UnityEngine.Video.VideoPlayer vp)
    {
        if (dialogues[currentDialogueIndex].CompareTag("Condition"))
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
        isstop = true;
        videoPlayer.Pause();
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
    }

    void SetDialogueActive(bool active)
    {
        dialogues[currentDialogueIndex].gameObject.SetActive(active);
    }

    bool CheckCondition()
    {
        return false; // You can implement your condition check here
    }
}