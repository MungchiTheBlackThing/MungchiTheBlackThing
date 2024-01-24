using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoMungchiTalk : MonoBehaviour
{
    public GameObject ChatText;
    public TMP_Text TextMesh;
    public List<string> dialogues; // 텍스트 리스트

    private int currentDialogueIndex = 0;

    void Start()
    {
        dialogues.Clear();
        TextMesh = ChatText.GetComponent<TMP_Text>();
        string[] dial1 = { "지구의 밝음과 아름다움 사이를 탐험하느라\n몸이 조금 저리게 되었지만", "그럴 만한 가치가 있었지.\n이곳을 찾게 되었으니!", "작고 구석진 모퉁이. 나를 닮은 곳.", "아이, 어두워. 비좁아. 지저분해.", "끈적한 그림자가 감싸는 것 같지.", "이곳일까? 나의 멋진\n‘인간’을 만나게 될 곳?" };
        foreach (string dial in dial1)
        {
            dialogues.Add(dial);
        }
        StartCoroutine(ShowNextDialogue());
    }

    IEnumerator ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            TextMesh.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
            Debug.Log(currentDialogueIndex);
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(ShowNextDialogue());
        }
        else
        {
            StopCoroutine(ShowNextDialogue());
            yield return null;
            currentDialogueIndex = 0;
            dialogues.Clear();
        }
         
    }
}