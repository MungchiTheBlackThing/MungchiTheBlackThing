using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
/*
1. 현재 모든 자식을 불러온다.
2. 클릭한 자식의 인덱스를 확인한다.
3. 다음 자식을 생성한다. - 동적 생성
*/
public class ChatClickManager : MonoBehaviour
{

    /*
    클릭할 때 AreaScript에게 다음 GameObject 전달
    AreaScript는 누를시 전달받은 GameObject 생성, 자기자신 비활성화 
    */
    [SerializeField]
    GameObject exit;
    [SerializeField]
    ScrollRect scrollRect;
    public int currIdx=0;

    public List<GameObject> radioScript=new List<GameObject>();
    int len = 0;
    void Start(){
        //Debug.Log(MoonRadioCallJson.radioScript.chapters[0].script_1[0].character);
        //currIdx번호로 새로 Instantiate 한다.
        
        len = MoonRadioCallJson.radioScript.chapter1.Count;
        for(int i=0;i<len;i++)
        {
            GameObject moonRadioObj = Instantiate(Resources.Load<GameObject>("MoonRadio/"+MoonRadioCallJson.radioScript.chapter1[i].character),this.transform);
            
            moonRadioObj.GetComponent<AreaScript>().SettingText(MoonRadioCallJson.radioScript.chapter1[i].speech);
            moonRadioObj.SetActive(false);

            if(i==0)
                moonRadioObj.SetActive(true);
            else
                moonRadioObj.SetActive(false);
            radioScript.Add(moonRadioObj);
        }

    }

    public void Exit()
    {
        //현재 자식들 모두 Destroy 
        Debug.Log("여기까지 와?");
        for(int i=0;i<this.transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        radioScript.Clear(); //가비지 컬렉션에 의해서 놓아준 자원을 없애줌.
        currIdx=0;
        len = MoonRadioCallJson.radioScript.chapter2.Count;

        for(int i=0;i<len;i++)
        {
            GameObject moonRadioObj = Instantiate(Resources.Load<GameObject>("MoonRadio/"+MoonRadioCallJson.radioScript.chapter2[i].character),this.transform);
            
            moonRadioObj.GetComponent<AreaScript>().SettingText(MoonRadioCallJson.radioScript.chapter2[i].speech);
            moonRadioObj.SetActive(false);

            if(i==0)
                moonRadioObj.SetActive(true);
            else
                moonRadioObj.SetActive(false);
            radioScript.Add(moonRadioObj);
        }
    }
    public void RunScript(){
        currIdx+=1;
        if(currIdx>=len){
            if(!exit.activeSelf){
                exit.SetActive(true);
            }
            return;
        }
        radioScript[currIdx].gameObject.SetActive(true);
    }
}
