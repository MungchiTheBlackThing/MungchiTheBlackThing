using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner2 : MonoBehaviour
{
    
    [SerializeField]
    GameObject spawnParent;
    [SerializeField]
    List<GameObject> spawnPrefab;
    Vector3 initPos;
    Vector3 prePos;
    float spawnInterval = 2.0f;
    public List<GameObject> spawn;
    public List<Vector3> spawnPos;

    
    void Awake()
    {
        initPos = spawnParent.GetComponent<RectTransform>().transform.position; //상대 위치 가져옴.
        Debug.Log(initPos);
    }

    private void OnEnable() {

        if(spawn.Count != 0)
            StartCoroutine("DropRandomObject",0.5f);
    }

    void Update()
    {
        if(prePos!=initPos)
        {
            UpdatePosObjects();
            prePos=initPos;
        }
    }
    void Start()
    {
        InitializeObjects();

        StartCoroutine("DropRandomObject",0.5f);
    }

    void UpdatePosObjects()
    {
        for(int i=0;i<spawnPos.Count;i++)
        {
            float randomX = Random.Range(initPos.x - 60, initPos.x + 60);
            Vector3 spawnPosition = new Vector3(randomX, initPos.y + 100, 0f);
            spawnPos[i]=spawnPosition;
        }

        Debug.Log("Pos 변경 여부 : "+initPos);
    }
    void InitializeObjects()
    {   
        foreach(var prefab in spawnPrefab)
        {
            float randomX = Random.Range(initPos.x - 60, initPos.x + 60);
            Vector3 spawnPosition = new Vector3(randomX, initPos.y + 100, 0f);
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity,transform) as GameObject;
            newObject.SetActive(false);
            spawnPos.Add(spawnPosition);
            spawn.Add(newObject);
        }
    }

    IEnumerator DropRandomObject()
    {
        while(true)
        {
            selectedObjectSetActive();
            yield return new WaitForSeconds(spawnInterval);
            //비활성화된 오브젝트 중 랜덤으로 선택하여 활성화
        }
    }

    List<int> GetIdxDisableObject()
    {
        List<int> indexs = new List<int>();
        for(int i=0;i<spawn.Count;i++)
        {
            if(!spawn[i].activeSelf)
            {
                indexs.Add(i);
            }
        }
        return indexs;
    }
    void selectedObjectSetActive()
    {
        // 비활성화된 물체 중에서 랜덤으로 선택하여 활성화
        List<int> indexs = GetIdxDisableObject();

        if(indexs.Count>0)
        {
            int randomIndex = Random.Range(0, indexs.Count);
            GameObject randomObject = spawn[indexs[randomIndex]];
            randomObject.transform.position = spawnPos[indexs[randomIndex]];
            Debug.Log(spawnPos[indexs[randomIndex]]);
            randomObject.SetActive(true);
            StartCoroutine(MoveObject(randomObject.transform,spawnPos[indexs[randomIndex]], 1.0f));
        }
    }

    IEnumerator MoveObject(Transform objTransform,Vector3 position, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = position; 

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

            // 현재 위치 업데이트
            objTransform.position = initialPosition + new Vector3(displacementX, displacementY, 0f);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    public void SetPosition(Vector3 movePos)
    {
        initPos = movePos;
        //호출 ?
    }

    void OnDisable()
    {
        StopCoroutine("DropRandomObject"); //이 오브젝트가 꺼질 때, 해당 코루틴을 종료한다.
    }
}
