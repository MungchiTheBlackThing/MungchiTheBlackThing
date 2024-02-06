using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject viewport;
    private void OnEnable() {
        viewport.GetComponent<ScrollRect>().verticalNormalizedPosition=1.0f;
    }
}
