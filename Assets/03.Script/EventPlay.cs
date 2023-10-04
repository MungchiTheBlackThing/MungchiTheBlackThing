using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPlay : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    public GameObject Mungchi;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Animator animator = Mungchi.GetComponent<Animator>();

        animator.SetTrigger("sleep");
        Debug.Log("sleep");
    }
    // Update is called once per frame
    void Start()
    {
        
    }
}
