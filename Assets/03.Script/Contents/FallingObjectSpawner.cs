using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FallingObjectSpawner : MonoBehaviour
{
    public List<GameObject> fallingObjectPrefabs = new List<GameObject>();
    public float spawnInterval = 1.0f;
    public int spawnRangeX = 5; // X축 랜덤 범위
    public int maxObjects = 15; // 최대 쌓일 물체 개수
    public int targetHeight = 100;

    public List<GameObject> fallingObjects = new List<GameObject>();
    ObjectManager _objectManager;

    void Start()
    {
        if (this.gameObject==isActiveAndEnabled)
            InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        if (fallingObjectPrefabs.Count == 0)
        {
            Debug.LogError("No fallingObjectPrefab is assigned!");
            return;
        }

        float randomX = Random.Range(this.transform.position.x - 60, this.transform.position.x + 60);
        Vector3 spawnPosition = new Vector3(randomX, this.transform.position.y+100, 0f);

        int randomPrefabIndex = Random.Range(0, fallingObjectPrefabs.Count);
        GameObject newObject = Instantiate(fallingObjectPrefabs[randomPrefabIndex], spawnPosition, Quaternion.identity) as GameObject;
        newObject.transform.SetParent(transform);
        fallingObjects.Add(newObject);

        // 물체가 쌓이면 확인하여 제거
        CheckAndRemoveObjects();
        if (this.gameObject == isActiveAndEnabled)
            StartCoroutine(MoveObject(newObject.transform, targetHeight, 1.0f));
    }

    void CheckAndRemoveObjects()
    {
        if (fallingObjects.Count > maxObjects)
        {
            GameObject objectToRemove = fallingObjects[0]; // 가장 처음에 생성된 물체를 가져옴
            fallingObjects.RemoveAt(0); // 리스트에서 제거
            Destroy(objectToRemove); // 물체 제거
        }
    }
    IEnumerator MoveObject(Transform objTransform, float targetY, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = objTransform.position;

        while (elapsedTime < duration)
        {
            objTransform.position = Vector3.Lerp(initialPosition, new Vector3(initialPosition.x, targetY, initialPosition.z), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objTransform.position = new Vector3(initialPosition.x, targetY, initialPosition.z);
    }
}