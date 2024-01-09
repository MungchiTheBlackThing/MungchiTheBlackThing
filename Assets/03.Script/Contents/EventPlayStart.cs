using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPlayStart : MonoBehaviour
{
    public GameObject Mungchi;
   
    public void Onclick()
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
}
