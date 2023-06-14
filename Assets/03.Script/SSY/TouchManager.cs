using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour,IPointerDownHandler,IDragHandler
{

    float distance = -10.0f;

    float leftX;
    float midX;
    Vector3 mousePosition;

    [Header("마우스/터치 이동 속도 조절")]
    [SerializeField]
    [Range(1f, 10f)]
    float speed = 3f;
    /*
      RectTransform Left - rectTransform.offsetMin.x
      RectTransform Right - rectTransform.offsetMax.x
      경계값 지정.
    */
    void Start(){
        leftX=this.gameObject.GetComponent<RectTransform>().offsetMin.x;
        midX=this.gameObject.GetComponent<RectTransform>().position.x+(Screen.width/2);//중앙값 결정
    }

    public void OnPointerDown(PointerEventData eventData){
        mousePosition = new Vector3(Input.mousePosition.x, 
        this.transform.position.y, distance);
        Debug.Log(mousePosition+" "+midX);
    }
    
    public void OnDrag(PointerEventData eventData)
    { 
        Vector3 dir=this.transform.position-mousePosition;
        if(mousePosition!=null)
            if(mousePosition.x>=midX)
                this.transform.Translate(dir*speed*Time.smoothDeltaTime);
            else if(mousePosition.x<midX)
                this.transform.Translate(dir*-speed*Time.smoothDeltaTime);
    }
}
