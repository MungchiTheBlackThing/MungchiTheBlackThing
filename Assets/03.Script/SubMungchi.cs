using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMungchi : MonoBehaviour
{
    public GameObject triggericon;
    public SubDialEvent subdial;
    // Start is called before the first frame update
    void OnEnable()
    {
        triggericon = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void triggerStart()
    {
        triggericon.SetActive(false);  
    }
}
