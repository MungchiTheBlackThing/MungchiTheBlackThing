using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Script.TimeEnum;


public class SkipController : MonoBehaviour
{
    // Start is called before the first frame update
    //SkipController가 시간을 보관한다.

    float[] _timeStamp = { 3600f, 7200f, 1800f };

    [SerializeField]
    GameObject alter;
    TMP_Text _alterText; //alterText를 보관한다.
    [SerializeField]
    TMP_Text _timeText;
    [SerializeField]
    GameObject eventPlay;
    [SerializeField]
    Image checklist; //Time에 따라서 이미지가 변경된다.

    [SerializeField]
    GameObject checkList_note;

    [SerializeField]
    GameObject menu;

    
    public List<GameObject> checkList_childs;
    [SerializeField]
    int curIdx = 0;

    float time = 0;
    PlayerController _player;
    const int HOUR = 3600;
    const int MIN = 60;
    GameObject SkipBackground;
    ObjectManager _objManager;
    [SerializeField]
    Sprite[] fly_icon;
    string[] alterTexts = { "시간은 상대적인 것. 느리게 흐르는\n이 기다림의 시간을 빨리 감아 넘길까요?\n", "뭉치는 달콤한 수면 후 다음 오후 한 시에 외출합니다.\n이 기다림의 시간을 빨리 감아 넘길까요?\n" };

    IEnumerator CloseCoroutine;

    public int GetTimeCurIdx { get => curIdx; set => curIdx = value; }

    [SerializeField]
    bool ifFirstUpdate = true;
    bool isInit = true;
    [SerializeField]
    List<GameObject> curProgress;

    static bool isFirstTime=true;
    //delegate 액션 함수 보관 -> 챕터 증가시 연관된 함수 호출
    void Start()
    {

        curProgress = new List<GameObject>();
        checkList_childs = new List<GameObject>();
        _objManager = GameObject.FindWithTag("ObjectManager").GetComponent<ObjectManager>();
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        Transform child = alter.transform.GetChild(0).transform;
        
        for (int i = 0; i < child.childCount; i++)
        {
            if (child.GetChild(i).name.Contains("title"))
                _alterText = child.GetChild(i).GetComponent<TMP_Text>();
        }

        for (int i = 0; i < checkList_note.transform.childCount; i++)
        {
            checkList_childs.Add(checkList_note.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {

        if(isFirstTime)
        {
            load();
            for (int i = 0; i < checkList_childs.Count; i++)
            {
                if (checkList_childs[i].name.Contains("Background")) continue;
                int idx = int.Parse(checkList_childs[i].name.Split(' ')[1]);
                if (idx <= GetTimeCurIdx)
                {
                    GameObject timeCheck = checkList_childs[i].transform.GetChild(0).gameObject;
                    if (timeCheck.activeSelf == false)
                    {
                        curProgress.Add(timeCheck);
                        timeCheck.SetActive(true);
                    }
                }
            }
            isFirstTime=false;
            ifFirstUpdate=false;
        }
        else
        {
            //초 단위 3600을 나누면 h나옴 
            //1분 60
            //1시간 3600초 
            //((현재 시간)%3600)/60
            int hour = (int)time / HOUR; //3600초 나눔
            int min = ((int)time % HOUR) / MIN;
            _timeText.text = (hour).ToString() + "h " + (min).ToString() + "m";
            time -= Time.deltaTime;

            if (time <= 0)
            {
                if (GetTimeCurIdx == 0)
                    _objManager.Close();
                GetTimeCurIdx++;
                ifFirstUpdate = true;
                //ObjectManager에게 전달.
            }

            if (ifFirstUpdate)
            {
                if (isInit && curIdx == 0) Init();

                for (int i = 0; i < checkList_childs.Count; i++)
                {
                    if (checkList_childs[i].name.Contains("Background")) continue;
                    int idx = int.Parse(checkList_childs[i].name.Split(' ')[1]);
                    if (idx <= GetTimeCurIdx)
                    {
                        GameObject timeCheck = checkList_childs[i].transform.GetChild(0).gameObject;
                        if (curProgress.Contains(timeCheck)==false&&timeCheck.activeSelf == false && ifFirstUpdate)
                        {
                            curProgress.Add(timeCheck);
                            StartCoroutine(CheckListOn());
                            ifFirstUpdate = false;
                        }

                    }
                }
            }
        }

    }

    public void ClickSkipBut()
    {
        if (alter != null)
        {
            alter.SetActive(true);

            if (GetTimeCurIdx == (int)TimeStamp.TS_NEXTCHAPTER)
            {
                if (_alterText != null)
                    _alterText.text = alterTexts[1];
            }
            else
            {
                if (_alterText != null)
                    _alterText.text = alterTexts[0];
            }

        }
    }

    public void YesBut()
    {
        //player 시간을 빠르게 만든다.
        alter.SetActive(false);
        ifFirstUpdate = true;

        switch (GetTimeCurIdx)
        {
            case (int)TimeStamp.TS_WATCHING:
                watcingPhase();
                ++GetTimeCurIdx;
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;

            case (int)TimeStamp.TS_THINKING:
                ++GetTimeCurIdx;
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);

                break;

            case (int)TimeStamp.TS_WRITING:
                writingPhase();
                ++GetTimeCurIdx;
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;
            case (int)TimeStamp.TS_NEXTCHAPTER:
                ClickSkipBut();
                if(eventPlay!=null)
                {
                    _objManager.memoryPool.DeactivateObject(eventPlay.name);
                }
                //Destroy(eventPlay);
                //해제해야함.
                _objManager.NextChapter();
                _player.SetChapter();
                _objManager.transChapter(_player.GetChapter());
                isInit = true;
                //현재 시간을 가져와서, 다음날 한시까지 계산을 한다.
                GetTimeCurIdx = 0;
                time = _timeStamp[GetTimeCurIdx];
                alter.SetActive(false);

                SkipBackground = _objManager.memoryPool.SearchMemory("skip_sleeping");

                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_sleeping"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_sleeping";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;
        }
        _player.SetAlreadyEndedPhase(GetTimeCurIdx);
        CloseAllBackgroundMenu();
        //ObjectManager에게 전달.
    }

    void Init()
    {
        menu.SetActive(true);
        checkList_note.transform.parent.gameObject.SetActive(true);
        _timeText.transform.parent.gameObject.SetActive(true);
        checklist.sprite = fly_icon[GetTimeCurIdx];
        for (int i = 0; i < curProgress.Count; i++)
        {
            curProgress[i].SetActive(false);
        }

        curProgress.Clear();
        isInit = false;
    }
    IEnumerator OpenCheckList()
    {
        
        yield return new WaitForSeconds(0.5f);

        checkList_note.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);

        if (GetTimeCurIdx >= 0 && GetTimeCurIdx - 1 < fly_icon.Length)
        {
            checklist.sprite = fly_icon[GetTimeCurIdx - 1];
        }

        for (int i = 0; i < curProgress.Count; i++)
        {
            curProgress[i].SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        CloseCoroutine = CloseAlter(checkList_note);
        StartCoroutine(CloseCoroutine);

    }
    IEnumerator CloseAlter(GameObject checkList)
    {

        yield return new WaitForSeconds(2f);
        checkList.SetActive(false);
    }

    public void SetSleepCheckList()
    {
        ifFirstUpdate = false;
        GetTimeCurIdx = 4;
        _player.SetAlreadyEndedPhase(GetTimeCurIdx);
        for (int i = 0; i < checkList_childs.Count; i++)
        {
            if (checkList_childs[i].name.Contains("Background")) continue;
            int idx = int.Parse(checkList_childs[i].name.Split(' ')[1]);

            if (idx <= GetTimeCurIdx)
            {
                GameObject timeCheck = checkList_childs[i].transform.GetChild(0).gameObject;
                if (timeCheck.activeSelf == false)
                {
                    curProgress.Add(timeCheck);
                    OpenAllBackgroundMenu();
                }

            }
        }
    }
    IEnumerator CheckListOn()
    {
        if (CloseCoroutine != null)
            StopCoroutine(CloseCoroutine);
        //3초간 애니메이션 송출 

        CloseAllBackgroundMenu();
        yield return new WaitForSeconds(3f);
        if (SkipBackground != null)
        {
            _objManager.memoryPool.DeactivateObject(SkipBackground.name);
            //Destroy(SkipBackground);
            SkipBackground = null;
        }
        OpenAllBackgroundMenu();
    }


    public void NoBut()
    {
        alter.SetActive(false);
    }

    public void CloseAllBackgroundMenu()
    {
        menu.SetActive(false);
        checkList_note.transform.parent.gameObject.SetActive(false);
        _timeText.transform.parent.gameObject.SetActive(false);
    }

    void OpenAllBackgroundMenu()
    {
        menu.SetActive(true);
        checkList_note.transform.parent.gameObject.SetActive(true);
        _timeText.transform.parent.gameObject.SetActive(true);
        StartCoroutine("OpenCheckList");

    }

    void watcingPhase()
    {
        _objManager.Close();
        _objManager.RemoveWatchingObject();
    }
    void writingPhase()
    {
        eventPlay = _objManager.memoryPool.SearchMemory("SleepSystem");
        if(eventPlay==null)
        {
            eventPlay = Instantiate(Resources.Load<GameObject>("SleepSystem"), _objManager.gameObject.transform);
            eventPlay.name="SleepSystem";
            _objManager.memoryPool.InsertMemory(eventPlay);
        }
        _objManager.memoryPool.SetActiveObject(eventPlay.name);
                //애니메이션 생성
        DateTime today = DateTime.Now; //현재 지금 시간
        string format = string.Format("{0} 13:00:00", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
        DateTime tomorrow = Convert.ToDateTime(format); //format 변환
        TimeSpan disTime = tomorrow - today; //두 차이를
        time = (float)disTime.TotalSeconds; //초로 변환함
        
    }
    void sleepPhase()
    {
        //자는 모션을 setActive 
         eventPlay = _objManager.memoryPool.SearchMemory("SleepSystem");
        if(eventPlay==null)
        {
            eventPlay = Instantiate(Resources.Load<GameObject>("SleepSystem"), _objManager.gameObject.transform);
            eventPlay.name="SleepSystem";
            _objManager.memoryPool.InsertMemory(eventPlay);
        }
        _objManager.memoryPool.SetActiveObject(eventPlay.name);
        eventPlay.transform.GetChild(0).gameObject.SetActive(false);
        eventPlay.transform.GetChild(1).gameObject.SetActive(true);
                //애니메이션 생성
        DateTime today = DateTime.Now; //현재 지금 시간
        string format = string.Format("{0} 13:00:00", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
        DateTime tomorrow = Convert.ToDateTime(format); //format 변환
        TimeSpan disTime = tomorrow - today; //두 차이를
        time = (float)disTime.TotalSeconds; //초로 변환함
    }

    public void load()
    {
        GetTimeCurIdx = _player.GetAlreadyEndedPhase();
        if(GetTimeCurIdx<_timeStamp.Length)
            time = _timeStamp[GetTimeCurIdx];
        
        switch(GetTimeCurIdx)
        {
            case (int)TimeStamp.TS_WATCHING: //object에서 생성
            break;
            case (int)TimeStamp.TS_THINKING:
            //Dialogu 시작
            break;
            case (int)TimeStamp.TS_WRITING:
            break;
            case (int)TimeStamp.TS_SLEEPING:
            writingPhase();
            break;
            case (int)TimeStamp.TS_NEXTCHAPTER:
            sleepPhase();
            break;
        }
    }
}
