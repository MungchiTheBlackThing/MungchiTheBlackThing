using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventPlayStart : MonoBehaviour
{
    public GameObject Mungchi;
   
    public void Onclick()
    {
        Animator animator = Mungchi.GetComponent<Animator>();
        GameObject.Find("Background").GetComponent<ScrollRect>().horizontal=true;
        animator.SetTrigger("start");
        Debug.Log("start");
    }
    void Start()
    {

    }
    // Update is called once per frame
}
