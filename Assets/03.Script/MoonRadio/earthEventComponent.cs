using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthEventComponent : MonoBehaviour
{
    [SerializeField]
    EarthRadioUIController earthController;

    void ExitEvents(){
        earthController.send2MoonButEventExit();
        this.GetComponent<Animator>().SetBool("isGoing",false);
        this.transform.parent.GetComponent<Animator>().SetBool("isGoing",true);
        //textbox isSetActive(true)s
    }
}
