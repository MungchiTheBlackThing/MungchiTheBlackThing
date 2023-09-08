using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 문제: 위에 검은 배경으로 덮어버리면 원래배경에서 스크롤, 클릭이 안됨 -> 검은 배경 Raytarget 을 제거하면 되지만 이러면 OpticMoving을 못씀 ...
public class MungchiClick : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public bool isinoptic = false;
    Vector3 vel = Vector3.zero;

    public void OnPointerClick(PointerEventData eventData)
    {
        Animator[] animator = this.GetComponentsInChildren<Animator>();

        for (int i = 0; i<animator.Length; i++)
        {
            animator[i].SetTrigger("Trigger");
        }

    }

    void Start()
    {
        isinoptic = false;
    }

    // Update is called once per frame
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "optic")
        {
            isinoptic = true;
            Debug.Log("true");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "optic")
        {
            isinoptic = false;
            Debug.Log("false");
        }
    }
}
