using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToRemove : MonoBehaviour
{
    public void OnMouseDown()
    {
        Destroy(this.gameObject);
    }
}
