using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        waitAlert();
    }

    // Update is called once per frame
    public void waitAlert()
    {
        StartCoroutine("waitForTransmission");
    }
    //waitForTransmission
    public IEnumerator waitForTransmission(){

        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
        yield return null;
        //main.SetActive(true);
        //Destroy(this.gameObject);
    }
}
