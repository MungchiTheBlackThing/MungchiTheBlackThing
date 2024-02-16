using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour , IDropHandler
{
    public GameObject Day;
    Day2StoryGuide day2StoryGuide;
    public delegate void OnDestroyObject();
    public OnDestroyObject destroyObject;

    public void OnDrop(PointerEventData eventData)
    {
        day2StoryGuide = Day.GetComponent<Day2StoryGuide>();
        day2StoryGuide.GuideEnd();
        destroyObject();
    }
}
