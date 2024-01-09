using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueClick : MonoBehaviour,IPointerDownHandler
{
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);
        Animator animator = gameObject.GetComponent<Animator>();

        for (int i = 0; i < results.Count; i++)
            if (results[i].gameObject.tag == "Click")
            {
                Debug.Log("넘어가욧");
                gameObject.GetComponent<DialogueA>().NextAni();
            }
    }
}
