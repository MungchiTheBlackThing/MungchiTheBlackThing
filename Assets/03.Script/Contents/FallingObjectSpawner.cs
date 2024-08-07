using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class FallingObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject SleepDotparent;
    public List<GameObject> objectPrefabs = new List<GameObject>(); // 다양한 프리팹들
    public float spawnInterval = 2.0f;
    public int maxActiveObjects = 10; // 최대 활성화 물체 개수
    public int targetHeight = 100;
    public int activeObjectCount;
    public RectTransform DefPos;
    public List<GameObject> fallingObjects = new List<GameObject>();
    public Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();
    public Queue<GameObject> activeObjectQueue = new Queue<GameObject>();

    void Awake()
    {
        DefPos = SleepDotparent.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        InitializeObjects();
        InvokeRepeating("DropRandomObject", 1f, spawnInterval);
    }
    void InitializeObjects()
    {
        foreach (var prefab in objectPrefabs)
        {
            float randomX = Random.Range(DefPos.position.x - 60, DefPos.position.x + 60);
            Vector3 spawnPosition = new Vector3(randomX, DefPos.position.y + 100, 0f);
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            fallingObjects.Add(newObject);
            initialPositions.Add(newObject, spawnPosition);
        }
    }

    void DropRandomObject()
    {
        // 현재 활성화된 물체 개수 세기
        activeObjectCount = CountActiveObjects();
        Debug.Log(activeObjectCount);
        // 현재 활성화된 물체가 최대 활성화 물체 개수보다 많으면 가장 먼저 활성화된 물체부터 비활성화
        if (activeObjectCount >= maxActiveObjects)
        {
            DeactivateOldestActiveObject();
        }

        // 비활성화된 물체 중에서 랜덤으로 선택하여 활성화
        ActivateRandomObject();
    }

    int CountActiveObjects()
    {
        int count = 0;
        foreach (var obj in fallingObjects)
        {
            if (obj.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    void DeactivateOldestActiveObject()
    {
        // 가장 먼저 활성화된 물체를 찾아서 비활성화 및 초기 위치로 이동
        if (activeObjectQueue.Count > 0)
        {
            GameObject oldestObject = activeObjectQueue.Dequeue();
            Debug.Log(oldestObject);
            MoveAndDeactivate(oldestObject);
        }
    }

    public void MoveAndDeactivate(GameObject obj)
    {
        // 초기 위치로 이동
        obj.transform.position = initialPositions[obj];

        // 물체 비활성화
        obj.SetActive(false);
    }

    void ActivateRandomObject()
    {
        // 비활성화된 물체 중에서 랜덤으로 선택하여 활성화
        List<GameObject> inactiveObjects = fallingObjects.FindAll(obj => !obj.activeSelf);

        if (inactiveObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveObjects.Count);
            GameObject randomObject = inactiveObjects[randomIndex];
            randomObject.transform.position = initialPositions[randomObject];
            randomObject.SetActive(true);
            activeObjectQueue.Enqueue(randomObject);
            StartCoroutine(MoveObject(randomObject.transform, 1.0f));
        }
    }

    IEnumerator MoveObject(Transform objTransform, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = objTransform.position;

        // 등속도 운동을 위한 초기 x 속도 계산
        float initialVelocityX = 100f;

        // 등가속도 운동을 위한 초기 y 속도 및 중력 계산
        float accelerationY = -600f;
        int random = Random.Range(0, 2) * 2 - 1; //방향 (1 or -1)

        while (elapsedTime < duration)
        {
            // x 및 y의 변화 계산
            float displacementX = initialVelocityX * elapsedTime * random;
            float displacementY = 0.4f * accelerationY * elapsedTime * elapsedTime;

            // z 변화 (필요에 따라 추가 가능)
            float displacementZ = 0f;

            // 현재 위치 업데이트
            objTransform.position = initialPosition + new Vector3(displacementX, displacementY, displacementZ);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    private void OnDisable() {
        CancelInvoke("DropRandomObject");
        fallingObjects.Clear();
    }
}