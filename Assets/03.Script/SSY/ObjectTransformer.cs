using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransformer : MonoBehaviour
{
    RectTransform currObject;
    RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        currObject=this.gameObject.GetComponent<RectTransform>();
        //canvasRect=GameObject.
        InitScaleAndMove();
    }

    /*Figma에서 가져온 layer 시작시 자동 배치 함수*/
    void InitScaleAndMove(){
        float canvasHeight=Screen.height;
        float canvasWidth=Screen.width;

        foreach(RectTransform child in currObject){
            Vector2 totalRatio=new Vector2(2796f/canvasWidth,1290f/canvasWidth);
            Vector2 ratio = new Vector2(canvasWidth/child.rect.width,canvasHeight/child.rect.height);
            child.localScale=new Vector2(ratio[0],ratio[1]);
            child.position=new Vector2(0,canvasHeight);
            //child.anchoredPosition=new Vector3(0,0);
        }
    }
}
