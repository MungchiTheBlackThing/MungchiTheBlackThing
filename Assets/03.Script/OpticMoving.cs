using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 쌍안경 움직임을 가능하게 하는 코드
public class OpticMoving : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectransform;
    public static Vector2 DefaultPos;

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
    
    void Update()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(rectransform.position, rectransform.forward);

        Debug.DrawRay(rectransform.position,rectransform.forward*10000 , Color.red);
            for (int i = 0; i < hit.Length; i++)
            {
                Debug.Log(hit[i].collider.name);
            }
    }
}