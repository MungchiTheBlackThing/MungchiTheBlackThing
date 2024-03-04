using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Noteclose : MonoBehaviour
{
    DefaultController defaultController;
    public void Close()
    {
        defaultController.OpenMenu();
        this.transform.parent.gameObject.GetComponent<ScrollRect>().horizontal = true;
        //checklist play true된다.
        Destroy(this.gameObject);
    }
    public void Start()
    {
        defaultController = GameObject.FindWithTag("Time").GetComponent<DefaultController>();
        defaultController.CloseMenu();
        this.transform.parent.gameObject.GetComponent<ScrollRect>().horizontal = false;
    }
}
