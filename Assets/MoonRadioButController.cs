using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoonRadioButController : MonoBehaviour
{

    [SerializeField]
    GameObject popup;
    [SerializeField]
    GameObject main;

    public void ExitMoonChannel(){
        //exitPopup setActive on
        //exit but interactable off
        Button currObj=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        currObj.interactable=false;
        popup.SetActive(true);
    }

    public void ExitPopup(){
        //popup off -> move a main channel 
        //close moonRadio_moon channel
        popup.SetActive(false);
        this.gameObject.SetActive(false);
        main.SetActive(true);
    }
}
