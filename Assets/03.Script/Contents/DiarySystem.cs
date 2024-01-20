using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiarySystem : MonoBehaviour
{
    [SerializeField]
    GameObject openDiary;
    [SerializeField]
    GameObject closeDiary;

    public void OpenDiary(){
        openDiary.gameObject.SetActive(true);
        closeDiary.gameObject.SetActive(false);

    }
    public void Exit(){
        //실제론 Destroy할 예정..
        //애니메이션후 애니메이션 끝나면 Destory하도록 설정할예정

        FindObjectOfType<DefaultController>().OpenMenu();
        Destroy(this.gameObject);
    }
    public void Next(){
        Instantiate(openDiary,openDiary.transform.parent);
    }
}
