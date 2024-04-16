using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    [SerializeField]
    GameObject alert;
    // Start is called before the first frame update
    void Start()
    {
        alert.SetActive(false);
    }

    // Update is called once per frame
    public void alert_on()
    {
        alert.SetActive(true);
    }
    public void cancel()
    {
        alert.SetActive(false);
    }
    public void replay()
    {
        SceneManager.LoadScene("01.Scenes/Loading");
    }
}
