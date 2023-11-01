using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MorningSystem : MonoBehaviour
{
    [SerializeField]
    GameObject Diary;
    public void OnClickDiary(){
        GameObject obj = EventSystem.current.currentSelectedGameObject;

        Transform []child=obj.GetComponentsInChildren<Transform>();

        for(int i=1;i<child.Length;i++){
            if(child[i].gameObject.name=="LightEffect"&&child[i].gameObject.activeSelf){
                Instantiate(Diary,this.transform.parent.parent);
                //Diary 생성으로 바꿀 예정.. instantiate 사용.
            }
        }
    }
}
