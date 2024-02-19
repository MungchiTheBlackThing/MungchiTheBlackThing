using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryGuide : MonoBehaviour
{
    private int chapter;

    void Start()
    {
        chapter = GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetChapter();

        Debug.Log("Chapter: " + chapter);

        if (chapter != 2 )
        {
            Destroy(this.gameObject); 
        }
    }

    public void OnClick()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject); 
    }

    
}