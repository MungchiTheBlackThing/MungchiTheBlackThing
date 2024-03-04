using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScrollStop : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Vector2 DefaultPos;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = this.gameObject.GetComponent<ScrollRect>();
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        DefaultPos.x = this.transform.position.x-320;
        DefaultPos.y = this.transform.position.y;
    }

    // Update is called once per frame
    public void Update()
    {

        // if(this.transform.transform.position!=DefaultPos)
        // {
        //     this.transform.transform.position = DefaultPos;
        // }
    }
}
