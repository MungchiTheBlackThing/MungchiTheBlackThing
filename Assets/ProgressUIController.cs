using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUIController : MonoBehaviour
{

    //1Day로 바꿀 예정
    [SerializeField]
    bool isInstant;
    [SerializeField]
    GameObject dragIcon;
    [SerializeField]
    GameObject dragScroller;
    // Update is called once per frame
    void Update()
    {
        
        if(isInstant){
            //이름 부여해야함.
            Instantiate(dragIcon,dragScroller.transform.GetChild(0));
            isInstant=false;
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
        }
    }
}
