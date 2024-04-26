using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class DefaultSpawn : ActionNode
{
    public GameObject imagePrefab; // 이미지로 사용할 프리팹
    public List<Vector3> positionList; // 이미지를 배치할 위치 목록

    protected override void OnStart()
    {
        // 포지션 리스트가 비어있거나 프리팹이 설정되지 않았을 경우 작업을 종료합니다.
        if (positionList.Count == 0 || imagePrefab == null)
        {
            Debug.LogWarning("Position list is empty or image prefab is not set.");
            return;
        }

        // 포지션 리스트에서 랜덤한 인덱스를 선택합니다.
        int randomIndex = Random.Range(0, positionList.Count);
        Vector3 randomPosition = positionList[randomIndex];

        // 선택된 위치에 이미지를 생성하여 배치합니다.
        GameObject imageObject = Instantiate(imagePrefab, randomPosition, Quaternion.identity);
    }

    protected override void OnStop()
    {
        // 작업 중지 시 필요한 경우 추가 정리 작업을 수행
    }

    protected override State OnUpdate()
    {
        // 이미지 생성 및 배치 작업은 한 번만 실행하므로 성공 상태 반환
        return State.Success;
    }
}
