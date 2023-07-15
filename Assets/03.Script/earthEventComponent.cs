using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthEventComponent : MonoBehaviour
{
    EarthRadioUIController earthController;
    void Start(){
        earthController=this.transform.parent.parent.GetComponent<EarthRadioUIController>();
    }
    void EnterEvents(){
        earthController.send2MoonButEventEnter();
    }
    void ExitEvents(){
        earthController.send2MoonButEventExit();
    }
}
