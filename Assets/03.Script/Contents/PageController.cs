using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    /*
    마우스가 페이지 위에 있으면, ClickPage를 실행시킨다.
    마우스를 누르면 rightToLeftPage 또는 leftTorightPage를 실행시킨다.
    */

    Animator animator;
    void Start(){
        animator=this.gameObject.transform.parent.GetComponent<Animator>();
        Debug.Log(this.gameObject.name);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("실행");
        animator.SetBool("onClick",true);
    }

    public void OnPointerUp(PointerEventData pointerEventData){
        animator.SetBool("isClicked",true);
        Invoke("Init",1f);
    }

    void Init(){
        animator.SetBool("isClicked",false);
        animator.SetBool("onClick",false);
    }

}
