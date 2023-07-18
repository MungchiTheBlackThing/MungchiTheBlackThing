using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputEnterText : MonoBehaviour
{
    TMP_InputField inputMessage;
    // Start is called before the first frame update
    void Start()
    {
        inputMessage=this.gameObject.GetComponent<TMP_InputField>();
        inputMessage.onEndEdit.AddListener(delegate{EnterInput(inputMessage);});
    }
    //입력처리 함수
    void EnterInput(TMP_InputField input){
        if(input.text.Length>0){
            input.text="\n";
        }
    }
}
