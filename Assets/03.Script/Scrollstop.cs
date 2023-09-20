using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Scrollstop : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public ScrollRect scrollRect;
    public Vector2 DefaultPos;
    public void Start()
    {
        scrollRect = this.gameObject.GetComponent<ScrollRect>();
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        DefaultPos = this.transform.position;
    }
    public void Update()
    {
        if (NoteClick.CanScroll == false)
        {
            this.transform.transform.position = DefaultPos;
            scrollRect.enabled = false;
        }
        else
        {
            scrollRect.enabled = true;
        }
    }

}
