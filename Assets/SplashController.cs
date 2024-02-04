using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject loadingObject;

    public void EndAnimation()
    {

        loadingObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
