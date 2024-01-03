using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PoemReadController : MonoBehaviour
{
    GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        background=GameObject.Find("Background").gameObject;

        Vector2 systemPos=background.GetComponent<RectTransform>().anchoredPosition;
        this.GetComponent<RectTransform>().anchoredPosition=new Vector2(Mathf.Abs(systemPos.x),systemPos.y);
        Debug.Log(background.transform.GetChild(0).name);
        if(background!=null)
        {
            this.gameObject.GetComponent<Image>().sprite=Resources.Load<Sprite>("Background/PoemBackground/"+background.transform.GetChild(0).name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitClick()
    {
        Destroy(this.gameObject);
    }
}
