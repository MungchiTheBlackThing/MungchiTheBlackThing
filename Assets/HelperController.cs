using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperController : MonoBehaviour
{
    [SerializeField]
    GameObject menuOn;

    public void exit(){
        this.gameObject.SetActive(false);
        menuOn.SetActive(true);
    }
}
