using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{
    // isFour 변수에 따라 선택지 갯수가 달라진다.
    [SerializeField]
    bool isFour;

    // 선택지를 저장할 리스트
    [SerializeField]
    List<GameObject> options;

    // 현재 선택된 체크박스의 인덱스를 저장할 변수
    public int selectedIndex = -1;

    // 선택지를 선택하는 메서드
    public void Choose()
    {
        GameObject option = EventSystem.current.currentSelectedGameObject;

        if (option == null)
        {
            return;
        }

        // 선택된 옵션의 인덱스를 찾기
        int newSelectedIndex = options.IndexOf(option);

        // 모든 선택지를 비활성화
        for (int i = 0; i < options.Count; i++)
        {
            options[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        // 새로운 선택된 체크박스를 활성화
        if (newSelectedIndex != -1)
        {
            options[newSelectedIndex].transform.GetChild(0).gameObject.SetActive(true);
            selectedIndex = newSelectedIndex;
        }
    }
}