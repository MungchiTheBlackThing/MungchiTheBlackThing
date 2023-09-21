using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DefaultController : MonoBehaviour
{
    //Animating 
    public void isClick(){
        Animator ani=EventSystem.current.currentSelectedGameObject.GetComponent<Animator>();
        Debug.Log(ani);
        ani.SetTrigger("isTouch");
    }
}
