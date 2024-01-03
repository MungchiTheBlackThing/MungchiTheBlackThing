using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    ScrollRect ParentSR;

    void Start()
    {
        ParentSR=GameObject.FindGameObjectWithTag("ObjectManager").GetComponent<ScrollRect>();
    }
    public void OnBeginDrag(PointerEventData e)
    {
        if(e==null)
            return;
        ParentSR.OnBeginDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        if(e==null)
            return;
        ParentSR.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        if(e==null)
            return;
        ParentSR.OnEndDrag(e);
    }
}
