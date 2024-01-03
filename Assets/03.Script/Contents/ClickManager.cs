using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    { 

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);
        

        for (int i = 0; i < results.Count; i++)
            Debug.Log(results[i].ToString());

    }
}
