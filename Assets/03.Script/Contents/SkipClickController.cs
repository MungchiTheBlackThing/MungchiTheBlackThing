using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SkipClickController : MonoBehaviour,IPointerClickHandler
{

   
    Transform moonText;
    List<GameObject> clickObject;
    int idx=0;
    float fadeSpeed=2.0f;
    bool isAlreadyClick=false;
    // Start is called before the first frame update
    void Start()
    {
        clickObject=new List<GameObject>();
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).name=="MoonText")
            {
                moonText=transform.GetChild(i);
            }  
        }

        for(int i=0;i<moonText.childCount;i++)
        {
            clickObject.Add(moonText.GetChild(i).gameObject);
        }

    }

    public void GoToMain()
    {
        Destroy(this.gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(idx<0||idx>=clickObject.Count||isAlreadyClick) return;
        
        if(clickObject[idx].gameObject.name=="Exit")
                clickObject[idx].GetComponent<Button>().interactable=true;

        if(idx-1>=0)
            StartCoroutine(FadeOut(clickObject[idx-1].GetComponent<TMP_Text>())); 
        StartCoroutine(FadeIn(clickObject[idx++].GetComponent<TMP_Text>())); 
        
    }

    IEnumerator FadeIn(TMP_Text text)
    {
        isAlreadyClick=true;
        while (text.alpha < 1)
        {
            text.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        isAlreadyClick=false;
    }
    IEnumerator FadeOut(TMP_Text text)
    {
        while (text.alpha > 0)
        {
            text.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}
