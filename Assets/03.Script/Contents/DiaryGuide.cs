using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryGuide : MonoBehaviour
{
    PlayerController sc;
    private int chapter;
    void Start()
    {
        chapter = sc.GetChapter();

        Debug.Log("Chapter: " + chapter);

        if (chapter != 2 )
        {
            this.gameObject.SetActive(false);
        }
    }
}