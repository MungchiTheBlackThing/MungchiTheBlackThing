using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDialEvent : MonoBehaviour
{
    [System.Serializable]
    public class ChildCameraData
    {
        public Transform childTransform;
        public Vector2 cameraPos;
    }
    public Transform parentObj;
    public ChildCameraData[] childCameras;

    private void Start()
    {
        // 처음에 모든 자식 오브젝트를 비활성화
        foreach (var childCameraData in childCameras)
        {
            childCameraData.childTransform.gameObject.SetActive(false);
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
        if (parentObj != null)
        {
            transform.parent = parentObj;
        }
        else
        {
            Debug.LogError("Parent Object not assigned!");
        }
    }
    public void OnDisable()
    {
        transform.parent = null;
    }
    public void trigger()
    {
        // 여기에 서브 다이얼로그 실행 조건을 만들면 됩니다.
        ActivateRandomChild();
    }

    public void ActivateRandomChild()
    {
        // 모든 자식 오브젝트를 비활성화
        foreach (var childCameraData in childCameras)
        {
            childCameraData.childTransform.gameObject.SetActive(false);
        }

        // 무작위로 하나의 자식 오브젝트를 활성화
        int randomIndex = Random.Range(0, childCameras.Length);
        var selectedChildCamera = childCameras[randomIndex];
        selectedChildCamera.childTransform.gameObject.SetActive(true);

        // 카메라를 선택된 자식 오브젝트의 카메라 위치로 이동
        Camera.main.transform.position = new Vector3(selectedChildCamera.cameraPos.x, selectedChildCamera.cameraPos.y, Camera.main.transform.position.z);
    }

}
