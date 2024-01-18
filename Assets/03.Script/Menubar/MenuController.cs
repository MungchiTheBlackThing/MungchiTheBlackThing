using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    /*Menu 변수*/
    #region Chapter 정보를 가진 변수,오직 한개만 존재해야하고, 수정해선 안된다.
    public static Chapters chapterList;
    #endregion

    [SerializeField]
    GameObject MenuBut;
    [SerializeField]
    GameObject Icon;

    [SerializeField]
    GameObject DayProgressUI;
    [SerializeField]
    GameObject MenuDefault;
    [SerializeField]
    GameObject Helper;
    [SerializeField]
    GameObject MyPageUI;
    [SerializeField]
    GameObject TimeUI;
    [SerializeField]
    GameObject checkList;

    void Start()
    {
        //데이터 로드 
        var loadedJson = Resources.Load<TextAsset>("Json/Chapters");

        if(loadedJson)
        {
            chapterList = JsonUtility.FromJson<Chapters>(loadedJson.ToString());
        }
    }
    public void onMenu()
    {
        if (!Icon.activeSelf)
        {
            TimeUI.SetActive(false);
            //checklist의 부모
            checkList.transform.parent.gameObject.SetActive(false);
            Icon.transform.parent.gameObject.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning", false);
        }
        else
        {
            Icon.SetActive(false);
            this.gameObject.GetComponent<Animator>().SetBool("isDowning", true);
        }
    }

    public void offMenu()
    {
        if (Icon.activeSelf)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isDowning", true);
            Icon.SetActive(false);
        }
    }

    public void MenuoffExit()
    {
        TimeUI.SetActive(true);
        checkList.transform.parent.gameObject.SetActive(true);
        Icon.transform.parent.gameObject.SetActive(false);
    }
    public void MenuAniExit()
    {
        Icon.SetActive(true);
    }
    public void onDayProgressUI()
    {
        //DayProgressUI on,.,
        DayProgressUI.SetActive(true);
        MenuDefault.SetActive(false);

    }

    public void onClickHelper()
    {
        Helper.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickMypage()
    {
        MyPageUI.SetActive(true);
        MenuDefault.SetActive(false);
    }


    IEnumerator CloseAlter(GameObject checkList)
    {
        yield return new WaitForSeconds(2f);
        checkList.SetActive(false);
    }
    public void onClickCheckListIcon()
    {
        if (checkList.activeSelf == false)
        {
            checkList.SetActive(true);
            StartCoroutine(CloseAlter(checkList));
        }
        else
            checkList.SetActive(false);

    }

}
