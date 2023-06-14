using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryTransformer : MonoBehaviour
{

    RectTransform currObject;
    RectTransform[] childObjects;
    // Start is called before the first frame update
    void Start()
    {
        currObject=this.gameObject.GetComponent<RectTransform>();
        InitScaleAndMove();
    }

    /*Figma에서 가져온 layer 시작시 자동 배치 함수*/
    void InitScaleAndMove(){
        int canvasHeight=Screen.height;
        int canvasWidth=Screen.width;
        childObjects=currObject.GetComponentsInChildren<RectTransform>();

        Debug.Log(childObjects[0]);

        foreach(RectTransform child in currObject){
            if(child.name==currObject.name)
                return;
            Vector2 ratio = new Vector2(canvasWidth/child.rect.width,canvasHeight/child.rect.height);
            child.localScale=childObjects[0].localScale*ratio;
            child.position=new Vector2(0,canvasHeight);
            Debug.Log(child.name);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
