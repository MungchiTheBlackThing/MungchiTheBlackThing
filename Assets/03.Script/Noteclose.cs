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
    }
    // Update is called once per frame
}
