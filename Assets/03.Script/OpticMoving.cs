using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 쌍안경 움직임을 가능하게 하는 코드
public class OpticMoving : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerDownHandler
{
    RectTransform rectransform;
    public static Vector2 DefaultPos;
    public GameObject target;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; 
        this.transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;
        this.transform.position = mousePos;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        List<RaycastResult> results=new();
        EventSystem.current.RaycastAll(eventData, results);

        for(int i=0;i<results.Count;i++)
            if(results[i].gameObject.tag=="Mungchi")
            {
                target.GetComponent<MungchiClick>().OnMouseDown();
                Debug.Log("뭉치 찾음");
                break;
            }
                
    }

    void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}