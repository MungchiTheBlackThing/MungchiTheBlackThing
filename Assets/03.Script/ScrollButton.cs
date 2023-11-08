using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    public ScrollRect ParentSR;

    public void OnBeginDrag(PointerEventData e)
    {
        ParentSR.OnBeginDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        ParentSR.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        ParentSR.OnEndDrag(e);
    }
}
