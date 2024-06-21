using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainExit : MonoBehaviour
{
    [SerializeField]
    MenuController menuController;

    // Start is called before the first frame update
    private void OnEnable()
    {
        menuController = GameObject.Find("Menu").GetComponent<MenuController>();
    }
    public void menuexit()
    {
        Debug.Log(this.transform.parent.gameObject.name);
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            Transform child = this.transform.parent.GetChild(i);
            {
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                    break;
                }
            }
        }
        this.transform.parent.gameObject.SetActive(false);
        menuController.skipon();
    }
}