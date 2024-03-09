using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject spawnPos;

    [SerializeField]
    List<GameObject> spawnPrefab;
    Vector3 initPos;
    
    List<GameObject> spawn;
    void Awake()
    {
        initPos = spawnPos.GetComponent<RectTransform>().transform.position; //상대 위치 가져옴.
    }

    void Enable()
    {

    }
    void Start()
    {
        InitializeObjects();

        //StartCoroutine("Spawn",1f);
    }

    void InitializeObjects()
    {   
        foreach(var prefab in spawnPrefab)
        {
            float randomX = Random.Range(initPos.x - 60, initPos.x + 60);
            Vector3 spawnPosition = new Vector3(randomX, initPos.y + 100, 0f);
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            spawn.Add(newObject);
        }
    }

    IEnumerator Spawn()
    {
        yield return null;


    }
}
