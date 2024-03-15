using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToRemove : MonoBehaviour
{
    private FallingObjectSpawner fallingObjectSpawner;

    void Start()
    {
        // FallingObjectSpawner를 찾아서 참조
        fallingObjectSpawner = FindObjectOfType<FallingObjectSpawner>();
        StartCoroutine("AutoDisable");
    }

    public void OnMouseDown()
    {
        // 클릭된 오브젝트를 삭제
        fallingObjectSpawner.MoveAndDeactivate(gameObject);
    }


    // 몇 초 뒤에 자동으로 사라지나?
    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(8f);

        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        //코루틴 해제한다.
        StopCoroutine("AutoDisable");
    }
}
