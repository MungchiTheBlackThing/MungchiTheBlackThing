using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainExit : MonoBehaviour
{
    [SerializeField]
    MenuController menuController;

    [SerializeField]
    DialogueManager dialogueManager;   

    // Start is called before the first frame update
    private void OnEnable()
    {
        menuController = GameObject.Find("Menu").GetComponent<MenuController>();
        dialogueManager = GameObject.Find("MainDialogue").GetComponent<DialogueManager>();
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
        dialogueManager.DialEnd();
        this.transform.parent.gameObject.SetActive(false);
        menuController.skipon();
    }
}