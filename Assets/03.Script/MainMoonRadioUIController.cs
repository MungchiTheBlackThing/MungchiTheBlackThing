using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMoonRadioUIController : MonoBehaviour
{
    [SerializeField]
    GameObject earth_radio;
    [SerializeField]
    GameObject moon_radio;

    public void goEarthChannel(){
        this.gameObject.SetActive(false);
        earth_radio.SetActive(true);
    }

    public void goMoonChannel(){
        this.gameObject.SetActive(false);
        moon_radio.SetActive(true);
    }
}
