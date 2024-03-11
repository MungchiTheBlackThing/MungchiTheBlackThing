using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubDialEvent : MonoBehaviour
{
    [System.Serializable]
    public class ChildCameraData
    {
        public GameObject childTransform;
        public Vector2 cameraPos;
    }
    public RectTransform parentObj;
    public ChildCameraData[] childCameras;
    public GameObject background;
    public ScrollRect scroll;
    public RectTransform backrect;
    private RectTransform rect;
    private float smoothTime = 0.5f;
    private void Start()
    { 
        scroll= background.GetComponent<ScrollRect>();
        backrect= background.GetComponent<RectTransform>();
        // 처음에 모든 자식 오브젝트를 비활성화
        foreach (var childCameraData in childCameras)
        {
            childCameraData.childTransform.SetActive(false);
        }
    }
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        this.gameObject.SetActive(true);
    //    }
    //}
    
    public void OnEnable()
    {
        scroll = background.GetComponent<ScrollRect>();
        backrect = background.GetComponent<RectTransform>();
        backrect.anchoredPosition = Vector2.zero; //이거 위치를 고정해야지만 뭉치 위치가 변하지 않음 --> 근데 문제는 순간적으로 싹 움직이는게 맘에 안듬
        if (parentObj != null)
        {
            this.transform.parent = parentObj;
            scroll.enabled = false;
            trigger();
        }
        else
        {
            Debug.LogError("Parent Object not assigned!");
        }
    }
    //IEnumerator MoveRect()
    //{
    //    float elapsedTime = 0f; // 경과 시간 변수 초기화

    //    Vector2 initialPosition = backrect.anchoredPosition;
    //    while (elapsedTime < smoothTime)
    //    {
    //        // Lerp 함수를 사용하여 부드럽게 이동
    //        backrect.transform.position = Vector2.Lerp(initialPosition, Vector2.zero, elapsedTime / smoothTime);

    //        // 경과 시간 갱신
    //        elapsedTime += Time.deltaTime;

    //        yield return null; // 다음 프레임까지 대기
    //    }

    //    // 부드러운 이동 완료 후 정확한 목표 위치로 설정
    //    backrect.anchoredPosition = Vector2.zero;
    //}
    public void OnDisable()
    {
        scroll.enabled = true;
    }
    public void trigger()
    {
        // 여기에 서브 다이얼로그 실행 조건을 만들면 됩니다.
        ActivateRandomChild();
    }

    public void ActivateRandomChild()
    {
        Debug.Log("자식 켜져라");
        // 모든 자식 오브젝트를 비활성화
        foreach (var childCameraData in childCameras)
        {
            childCameraData.childTransform.SetActive(false);
        }
        Debug.Log(childCameras.Length);
        // 무작위로 하나의 자식 오브젝트를 활성화
        int randomIndex = Random.Range(0, childCameras.Length);
        Debug.Log(randomIndex);
        var selectedChildCamera = childCameras[randomIndex];
        Debug.Log(selectedChildCamera.childTransform.name);
        // 카메라를 선택된 자식 오브젝트의 카메라 위치로 이동
        StartCoroutine(MoveCamera(new Vector2(selectedChildCamera.cameraPos.x, backrect.transform.position.y),randomIndex));
    }
    IEnumerator MoveCamera(Vector2 targetPosition, int randomindex)
    {
        float elapsedTime = 0f; // 경과 시간 변수 초기화

        Vector2 initialPosition = backrect.transform.position;
        while (elapsedTime < smoothTime)
        {
            // Lerp 함수를 사용하여 부드럽게 이동
            backrect.transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / smoothTime);

            // 경과 시간 갱신
            elapsedTime += Time.deltaTime;

            yield return null; // 다음 프레임까지 대기
        }

        // 부드러운 이동 완료 후 정확한 목표 위치로 설정
        backrect.transform.position = targetPosition;
        active(randomindex);
    }
    public void active(int randomindex)
    {
        childCameras[randomindex].childTransform.SetActive(true);
    }
}
