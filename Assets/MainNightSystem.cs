using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNightSystem : MonoBehaviour
{

    [SerializeField]
    GameObject moon_main;
    public void InstMoonSystem(){
        Instantiate(moon_main,this.transform.parent);
    }
}
