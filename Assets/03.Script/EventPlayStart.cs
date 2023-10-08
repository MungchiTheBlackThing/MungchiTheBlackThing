using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPlayStart : MonoBehaviour,IPointerDownHandler
{
    public GameObject Mungchi;
   
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Animator animator = Mungchi.GetComponent<Animator>();
        NoteClick.CanScroll = false;
        animator.SetTrigger("start");
        Debug.Log("start");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
