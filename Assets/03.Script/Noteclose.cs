using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Noteclose : MonoBehaviour
{
    
    public void Close()
    {
         NoteClick.CanScroll = true;
        this.transform.parent.gameObject.GetComponent<ScrollRect>().horizontal=true;
        Destroy(this.gameObject);    
    }
    public void Start()
    {
        NoteClick.CanScroll = false;
    }
}
