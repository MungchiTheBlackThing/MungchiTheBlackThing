using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Noteclose : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    public void OnPointerClick(PointerEventData eventData)
    {
       this.gameObject.SetActive(false);
    }
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
}
