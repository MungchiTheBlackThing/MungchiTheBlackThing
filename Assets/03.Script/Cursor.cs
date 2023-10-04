using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public bool isrelease = false;
    public GameObject target;
    void Start(){
        isrelease = false;
    }
    private void Update() 
    {
        if (Input.GetMouseButton(0))
        {
            ObjectMove();
        }
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = new();
            //마우스 클릭한 좌표값 가져오기
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //해당 좌표에 있는 오브젝트 찾기
            RaycastHit2D[] hit = Physics2D.RaycastAll(pos, Vector2.zero, 0f);

            for (int i = 0; i < hit.Length; i++)
                if (hit[i].collider.tag == "Mungchi")
                {
                    target.GetComponent<MungchiClick>().OnMouseDown();
                    Debug.Log("뭉치 찾음");
                    break;
                }
        }
    }

    private void ObjectMove()
    {
        // Screen 좌표계인 mousePosition을 World 좌표계로 바꾼다
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        transform.position = point;
    }

}
