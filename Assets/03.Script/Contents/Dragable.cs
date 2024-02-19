using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public static Vector2 DefaultPos;
    DragsObject dragObjects;
    private Image image;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        dragObjects = this.GetComponentInParent<DragsObject>();
        image = this.GetComponent<Image>();
        color = image.color;
        color.a = 0;
        image.color = color;
    }


    public void OnBeginDrag(PointerEventData eventdata)
    {
        DefaultPos = this.transform.position;
        color.a = 150;
        image.color = color;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
        color.a = 150;
        image.color = color;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = DefaultPos;
        color.a = 0;
        image.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerdown");
        color.a = 0;
        image.color = color;
    }
    public void SelfDestroy()
    {
        dragObjects. DestroyChilde();
    }
}
