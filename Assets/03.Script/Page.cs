using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Page : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    public GameObject Diary;
    public int currentpage = 0;
    public int totalpage;
    public Button nextButton;
    public Button preButton;
    public Button cancelButton;
    public GameObject Mungchi;

    // Start is called before the first frame update
    void Start()
    {
        currentpage = 0;
        Transform transform = this.transform;
        totalpage = transform.childCount-1;
        for (int i = 0; i < transform.childCount; i++)
        {
            pages.Add(transform.GetChild(i).gameObject);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        this.nextButton.onClick.AddListener(() => { this.NextPage(); });
        this.preButton.onClick.AddListener(() => { this.PrePage(); });
        this.cancelButton.onClick.AddListener(() => { this.EndPage(); });
        cancelButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentpage == totalpage)
        {
            nextButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(true);
        }
        if (currentpage <= 0)
        {
            preButton.gameObject.SetActive(false);
        }
    }
    public void NextPage()
    {
        Transform transform = this.transform;
        preButton.gameObject.SetActive(true);
        if (currentpage == totalpage)
        {
            return;
        }
        transform.GetChild(currentpage).gameObject.SetActive(false);
        currentpage++;
        transform.GetChild(currentpage).gameObject.SetActive(true);
    }
    public void PrePage()
    {
        Transform transform = this.transform;
        nextButton.gameObject.SetActive(true);
        if (currentpage <= 0)
        {
            return;
        }
        transform.GetChild(currentpage).gameObject.SetActive(false);
        currentpage--;
        transform.GetChild(currentpage).gameObject.SetActive(true);
    }
    public void EndPage()
    {
        Debug.Log("sfsf");
        Animator animator = Mungchi.GetComponent<Animator>();
        animator.SetTrigger("exit");
    }
}
