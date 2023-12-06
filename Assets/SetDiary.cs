using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetDiary : MonoBehaviour
{
    GameObject curTime;
    void Start()
    {
        curTime=GameObject.Find("Background").transform.GetChild(0).gameObject;
        
    }
    // Start is called before the first frame update
    public void SetDiaryLight()
    {
        curTime.GetComponent<DefaultController>().SetLightDiary();
    }
}
