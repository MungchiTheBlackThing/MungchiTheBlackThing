using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class DialogueA : MonoBehaviour
{
    public List<GameObject> Dials = new List<GameObject>();
    public int Current=0;
    public int EndDial;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.transform;
        EndDial = transform.childCount - 1;
    }

    // Update is called once per frame

    public void NextAni()
    {
        Transform transform = this.transform;
        if (Current == EndDial)
        {
            return;
        }
        transform.GetChild(Current).gameObject.SetActive(false);
        Current++;
        transform.GetChild(Current).gameObject.SetActive(true);
    }
}
