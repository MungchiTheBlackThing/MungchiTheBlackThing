using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPlay2 : MonoBehaviour,IPointerDownHandler
{
    public GameObject Mungchi;
    public GameObject Diary;

    // Start is called before the first frame update
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Animator animator = Mungchi.GetComponent<Animator>();
        Diary.SetActive(true);
        animator.SetTrigger("read");
        Debug.Log("read");
    }
    // Update is called once per frame
    void Start()
    {
        
    }
}
