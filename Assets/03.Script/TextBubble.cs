using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TextBubble : MonoBehaviour
{
    TMP_InputField inputField;
    public TMP_Text textDisplay;
    public Button nextButton;
    public TMP_Text Question;
    private string defaultText = "생각을 입력해주세요";
    private bool isInputEnabled = true;
    private string userInput = "";

    void Start()
    {
        this.inputField = GetComponent<TMP_InputField>();
        // InputField에 이벤트 리스너 추가
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Next 버튼에 이벤트 리스너 추가
        nextButton.onClick.AddListener(OnNextButtonClick);

        // 초기화
        ResetInputField();
    }

    void OnInputValueChanged(string input)
    {
        // 인풋 필드에 값이 변경되면 호출
        userInput = input;

        // "생각을 입력해주세요" 텍스트가 있으면 지우기
        if (input == defaultText)
        {
            inputField.text = "";
        }
    }

    void OnNextButtonClick()
    {
        // 입력한 텍스트를 텍스트 필드에 표시
        textDisplay.text = "답변: " + userInput;
        inputField.text = "";
        Question.text = "Answer";
        // 인풋 필드 및 다음 버튼 비활성화
        inputField.interactable = false;
        nextButton.interactable = false;
    }

    void ResetInputField()
    {
        // 기본 텍스트 설정 및 입력 필드 활성화
        inputField.text = defaultText;
        userInput = "";
        inputField.interactable = true;
        nextButton.interactable = true;
    }
}