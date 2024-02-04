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
    }

    public void OnMouseDown()
    {
        // 클릭된 오브젝트를 삭제
        fallingObjectSpawner.MoveAndDeactivate(gameObject);
    }
}
