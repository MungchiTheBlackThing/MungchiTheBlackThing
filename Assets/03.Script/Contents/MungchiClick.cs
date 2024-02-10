using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 문제: 위에 검은 배경으로 덮어버리면 원래배경에서 스크롤, 클릭이 안됨 -> 검은 배경 Raytarget 을 제거하면 되지만 이러면 OpticMoving을 못씀 ...
public class MungchiClick : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isinoptic = false;
    Vector3 vel = Vector3.zero;
    [SerializeField]
    GameObject exit;
    [SerializeField]
    Transform []optic;
    float speed=5f;
    float time=1f;

    Vector2 originScale;
    void Awake()
    {
        originScale = transform.localScale;
    }
    public void OnMouseDown()
    {
        Animator[] animator = this.GetComponentsInChildren<Animator>();

        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetTrigger("Trigger");
            animator[i].SetBool("BoolAni", true);
        }
        StartCoroutine("CloseBino");
    }

    IEnumerator CloseBino(){
        //X UI를 뜨도록 canvas를 킨다.

        while(optic[0].transform.localScale.x<35f)
        {
            optic[0].transform.localScale = originScale * (1f + time * speed);
            optic[1].transform.localScale = originScale * (1f + time * speed);
            
            time +=Time.deltaTime;
            yield return null; //다음 프레임에서 실행
        }

        yield return new WaitForSeconds(1f);
        Instantiate(exit,GameObject.Find("Canvas").transform);
    }
    //Animator[] animator = this.GetComponentsInChildren<Animator>();

    //    for (int i = 0; i<animator.Length; i++)
    //    {
    //        animator[i].SetTrigger("Trigger");

    void Start()
    {
        isinoptic = false;
    }

    // Update is called once per frame
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "optic")
        {
            isinoptic = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "optic")
        {
            isinoptic = false;
        }
    }

    //void Update()
    //{
    //    //마우스 클릭시
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        List<RaycastResult> results = new();
    //        //마우스 클릭한 좌표값 가져오기
    //        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        //해당 좌표에 있는 오브젝트 찾기
    //        RaycastHit2D[] hit = Physics2D.RaycastAll(pos, Vector2.zero, 0f);

    //        for (int i = 0; i < results.Count; i++)
    //            if (results[i].gameObject.tag == "Mungchi")
    //            {
    //                target.GetComponent<MungchiClick>().OnMouseDown();
    //                Debug.Log("뭉치 찾음");
    //                break;
    //            }

    //    }
    //}
}
