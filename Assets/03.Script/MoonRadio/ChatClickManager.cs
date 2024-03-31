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
        
        len = MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_1.Length;
        for(int i=0;i<len;i++)
        {
            GameObject moonRadioObj = Instantiate(Resources.Load<GameObject>("MoonRadio/"+MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_1[i].character),this.transform);
            
            moonRadioObj.GetComponent<AreaScript>().SettingText(MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_1[i].speech);
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

        for(int i=0;i<this.transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        radioScript.Clear(); //가비지 컬렉션에 의해서 놓아준 자원을 없애줌.
        currIdx=0;
        len = MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_2.Length;

        for(int i=0;i<len;i++)
        {
            GameObject moonRadioObj = Instantiate(Resources.Load<GameObject>("MoonRadio/"+MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_2[i].character),this.transform);
            
            moonRadioObj.GetComponent<AreaScript>().SettingText(MoonRadioCallJson.radioScript.moon_radio_script.chapters[0].script_2[i].speech);
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

        // Transform parentObject=this.gameObject.transform;

        // if(parentObject)
        // {
        //     if(parentObject.childCount>=3){
        //         Destroy(parentObject.GetChild(0).gameObject);
        //     }
        //     GameObject newObject=Instantiate(dreamNPC[idx[currIdx]]) as GameObject; //gameObject 생성
        //     newObject.name=newObject.name.Substring(0,newObject.name.IndexOf('('));
        //     newObject.transform.GetComponent<AreaScript>().CharacterText.text=txt[currIdx-1];
        //     newObject.transform.SetParent(parentObject,false); //현재 클릭된 오브젝트의 부모 위로 설정해주기.
        // }
    }
}
