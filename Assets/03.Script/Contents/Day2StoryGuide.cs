using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2StoryGuide : MonoBehaviour
{
    public GameObject storyDial;
    public GameObject GuideBackground;
    PlayerController playerController;
    Animator animator;
    StoryDial dial;
    public int chapter;
    public int currentClipIndex;
    public bool isGimic = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GuideBackground.GetComponent<Animator>();
        dial = storyDial.GetComponent<StoryDial>();
        animator.SetTrigger("Drag");
        chapter = playerController.GetChapter();
        GuideBackground.SetActive(false);
        Debug.Log(chapter);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isGimic)
        {
            GuideEnd();
        }
    }
    public void GuideStart()
    {
        Debug.Log("왜 안켜져");
        isGimic = true;
        GuideBackground.SetActive(true);
        animator.SetTrigger("Drag");
    }
    public void GuideEnd()
    {
        dial.Guide = true;
        dial.OnMouseClick();
        isGimic=false;
        GuideBackground.SetActive(false);
    }
}
