using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2StoryGuide : MonoBehaviour
{
    public GameObject GuideBackground;
    Animator animator;
    StoryDial dial;
    public int currentClipIndex;
    public bool isGimic = false;
    Dragable dragable;
    Drop drop;
    // Start is called before the first frame update
    void Start()
    {
        dial = this.GetComponent<StoryDial>();
        GuideBackground.SetActive(false);
    }
    public void GuideStart()
    {
        animator = GuideBackground.GetComponent<Animator>();
        Debug.Log("왜 안켜져");
        isGimic = true;
        GuideBackground.SetActive(true);
        animator.SetTrigger("Drag"); 
        dragable = GameObject.FindWithTag("Dragable").gameObject.GetComponent<Dragable>();
        drop = GameObject.FindWithTag("Drop").gameObject.GetComponent<Drop>();

        drop.destroyObject += dragable.SelfDestroy;
    }
    public void GuideEnd()
    {
        dial.Guide = true;
        dial.OnMouseClick();
        isGimic=false;
        GuideBackground.SetActive(false);
    }
}
