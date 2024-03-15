using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMungchi : MonoBehaviour
{
    public GameObject triggericon;
    private SubDialEvent subdial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUp()
    {
        triggericon.SetActive(false);  
    }
}
