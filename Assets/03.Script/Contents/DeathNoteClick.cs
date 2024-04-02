using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNoteClick : MonoBehaviour
{
    static bool checkdeath = false;
    public bool SUN = false;
    [SerializeField]
    GameObject _deathnote;

    [SerializeField]
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //이건 지금 예시임
        SUN = false;
    }

    // Update is called once per frame
    public void Onclick()
    {
        checkdeath = true;
        if (SUN)
        {
            _deathnote = Instantiate(Resources.Load<GameObject>("Sun_deathnote"), canvas.transform);
        }
        else
        {
            _deathnote = Instantiate(Resources.Load<GameObject>("Moon_deathnote"), canvas.transform);
        }
        _deathnote.SetActive(true);
    }
}
