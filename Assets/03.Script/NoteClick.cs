using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 문제: 위에 검은 배경으로 덮어버리면 원래배경에서 스크롤, 클릭이 안됨 -> 검은 배경 Raytarget 을 제거하면 되지만 이러면 OpticMoving을 못씀 ...
public class NoteClick : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    public bool isnote = true;
    public GameObject NoteBackground;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);


        for (int i = 0; i < results.Count; i++)
            if(results[i].gameObject.tag== "Note")
            {
                Instantiate(NoteBackground,GameObject.Find("Background").transform);
            }

    }
    void Start()
    {
        if (!isnote)
        {
            this.gameObject.SetActive(false);
        }
    }
}
