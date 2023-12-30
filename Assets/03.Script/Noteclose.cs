using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Noteclose : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public GameObject NoteBackground;
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(NoteBackground);
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
