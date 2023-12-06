using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyaniactivate : MonoBehaviour
{
    public GameObject GameObject;
    // Start is called before the first frame update
    public void Onclick()
    {
        GameObject.gameObject.SetActive(true);
    }
}
