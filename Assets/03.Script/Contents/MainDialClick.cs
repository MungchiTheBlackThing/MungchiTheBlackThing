using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialClick : MonoBehaviour
{
    [SerializeField]
    GameObject Background;

    [SerializeField]
    GameObject MainDialogue;

    [SerializeField]
    Vector2[] positions;
    // 다이얼로그 자식들
    [SerializeField]
    List<GameObject> dialogues = new List<GameObject>();

    RectTransform rectTransform;
    // 다이얼로그 자식의 이름
    string[] dialogueNames = {"Morning", "Evening", "Night", "Dawn"};

    public int randomindex;
    // 활성화될 때 호출되는 함수
    
    void OnEnable()
    {
        Background = GameObject.Find("Background");
        MainDialogue = GameObject.Find("MainDialogue");
        rectTransform = this.GetComponent<RectTransform>();
        // 스크립트가 활성화될 때 랜덤한 위치를 활성화
        ActivateRandomPosition();
    }
    public void ActiveDialouge()
    {
        Debug.Log("메인다이얼로그1");
        // 다이얼로그 자식들을 찾아 리스트에 추가
        for (int i = 0; i < dialogueNames.Length; i++)
        {
            Transform child = MainDialogue.transform.Find(dialogueNames[i]);
            if (child != null)
            {
                dialogues.Add(child.gameObject);
            }
        }

        // 백그라운드의 이름을 가져와서 해당하는 다이얼로그 자식을 활성화
        ActivateDialogueByBackgroundName();
    }

    void ActivateRandomPosition()
    {
        // 만약 좌표가 없다면 함수를 종료
        if (positions.Length == 0)
        {
            Debug.LogError("There are no positions assigned.");
            return;
        }

        // 랜덤한 인덱스를 선택
        randomindex = Random.Range(0, positions.Length);

        // 선택된 위치를 활성화
        rectTransform.anchoredPosition = positions[randomindex];
    }

    // 백그라운드의 이름을 가져와서 해당하는 다이얼로그 자식을 활성화하는 함수
    void ActivateDialogueByBackgroundName()
    {
        Debug.Log("메인다이얼로그2");
        // 백그라운드의 이름 가져오기
        string backname = Background.transform.GetChild(0).name;
        Debug.Log(backname);
        // 다이얼로그 자식을 찾아서 활성화
        for (int i = 0; i < dialogueNames.Length; i++)
        {
            if (backname.Contains(dialogueNames[i]))
            {
                // 해당하는 다이얼로그 자식을 활성화
                dialogues[i].SetActive(true);
                if (randomindex == 0) //테이블
                {
                    dialogues[i].transform.GetChild(2).gameObject.SetActive(true);
                }
                if (randomindex == 1) //서랍 (일단 닫힌 서랍으로 Default)
                {
                    dialogues[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                if (randomindex == 2) //침대
                {
                    dialogues[i].transform.GetChild(3).gameObject.SetActive(true);
                }
                if (randomindex == 3) //해먹
                {
                    dialogues[i].transform.GetChild(4).gameObject.SetActive(true);
                }
            }
            else
            {
                // 해당하는 다이얼로그 자식을 비활성화
                dialogues[i].SetActive(false);
            }
        }
    }
}