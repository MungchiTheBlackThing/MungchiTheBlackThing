using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MungchiPos : MonoBehaviour
{
    /*뭉치의 좌표도 저장 예정이지만, 이클래스는 사용 안할 예정.. 수정해야함 ! 임시용*/
    [SerializeField]
    GameObject background;

    public void onDialogue(GameObject parents){
        parents.transform.Find(background.name).gameObject.SetActive(true);
        Debug.Log(parents.name);
    }
}
