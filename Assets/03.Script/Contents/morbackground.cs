using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morbackground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    RectTransform background;
    [SerializeField]
    RectTransform mask;
    void Start()
    {
        Debug.Log($"{background.sizeDelta.x},{background.sizeDelta.y}");
        mask.sizeDelta=new Vector2(background.sizeDelta.x,background.sizeDelta.y);
        mask.localScale=Vector3.one;
    }
    public void del_background()
    {
        this.gameObject.SetActive(false);
    }
}
