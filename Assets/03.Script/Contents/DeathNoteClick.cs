using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNoteClick : MonoBehaviour
{
    public static bool checkdeath = false;
    public bool SUN = false;
    [SerializeField]
    GameObject _deathnote;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    MenuController menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu").GetComponent<MenuController>();
        canvas = GameObject.Find("Canvas");
        //이건 지금 예시임
        SUN = false;
    }

    // Update is called once per frame
    public void Onclick()
    {
        if(!checkdeath)
        {
            if (SUN)
            {
                _deathnote = Instantiate(Resources.Load<GameObject>("Sun_deathnote"), canvas.transform);
            }
            else
            {
                _deathnote = Instantiate(Resources.Load<GameObject>("Moon_deathnote"), canvas.transform);
            }
            Destroy(this.transform.GetChild(0).gameObject);
        }
        checkdeath = true;
        _deathnote.SetActive(true);
        menu.replayON();
    }
}
