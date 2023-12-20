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
        GameObject.Find("Night").GetComponent<DefaultController>().OpenMenu();
        this.transform.parent.gameObject.GetComponent<ScrollRect>().horizontal=true;
        //checklist play true된다.
        Destroy(this.gameObject);    
    }
    public void Start()
    {
        
        GameObject.Find("Night").GetComponent<DefaultController>().CloseMenu();
        NoteClick.CanScroll = false;
    }
}
