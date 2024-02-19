using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragsObject : MonoBehaviour
{
    void OnEnable()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DestroyChilde()
    {
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
