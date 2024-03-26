using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseWriting : MonoBehaviour
{
    [SerializeField]
    GameObject Diary;
    private void OnEnable()
    {
        Diary = GameObject.Find("phase_diary");
        Debug.Log("일기장 끄기");
        Diary.SetActive(false);
    }
    // Start is called before the first frame update

    private void OnDisable()
    {
        Diary.SetActive(true);
    }
}
