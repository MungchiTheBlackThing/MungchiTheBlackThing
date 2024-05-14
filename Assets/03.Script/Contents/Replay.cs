using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    [SerializeField]
    GameObject alert;

    [SerializeField]
    PlayerController _player;

    [SerializeField]
    SkipController _skipController;
    // Start is called before the first frame update
    void Start()
    {
        alert.SetActive(false);
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _skipController = GameObject.Find("TimeManager").GetComponent<SkipController>();
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
        SkipController.is_end = false;
        SkipController.is_Replay = true;
        DeathNoteClick.checkdeath = false;
        _player.Init();
        _skipController.isInit = true;
        _skipController.ifFirstUpdate = true;
        SceneManager.LoadScene("01.Scenes/Loading");
    }
}
