using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Script.TimeEnum;
using UnityEngine.SceneManagement;

public class SkipController : MonoBehaviour
{
    //Start is called before the first frame update
    //SkipController가 시간을 보관한다.

    public static bool isFirstEntry=false;
    float[] _timeStamp = { 3600f, 1800f, 7200f, 1800f, 1800f};

    [SerializeField]
    GameObject alter;
    TMP_Text _alterText; //alterText를 보관한다.
    [SerializeField]
    TMP_Text _timeText;
    [SerializeField]
    GameObject phaseWriting;
    [SerializeField]
    GameObject eventPlay;
    [SerializeField]
    GameObject MainMungchi;
    
    [SerializeField]
    Image checklist; //Time에 따라서 이미지가 변경된다.

    [SerializeField]
    GameObject checkList_note;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject _background;

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
    public bool ifFirstUpdate = true;
    public bool isInit = true;
    [SerializeField]
    List<GameObject> curProgress;
    [SerializeField]
    GameObject TimeUI;
    static bool isFirstTime=true;
    //delegate 액션 함수 보관 -> 챕터 증가시 연관된 함수 호출
    delegate void OnUpdatedProgressDelegate(int chapter);
    OnUpdatedProgressDelegate onUpdatedProgress;
    //다이얼로그 실행
    GameObject story;
    public static bool is_end = false;
    public static bool is_Replay = false;

    void Awake()
    {
        onUpdatedProgress=new OnUpdatedProgressDelegate(GameObject.Find("Menu").GetComponent<MenuController>().OnUpdatedProgress);
        curProgress = new List<GameObject>();
        checkList_childs = new List<GameObject>();
        _objManager = GameObject.FindWithTag("ObjectManager").GetComponent<ObjectManager>();
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //onUpdatedProgress(_player.GetChapter());
        Debug.Log("Awake");
    }

    void Start()
    {
        _background = GameObject.Find("Background");
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
        onUpdatedProgress(_player.GetChapter());
        is_Replay = false;
        Debug.Log("Start");
    }

    void OnEnable()
    {
        if (is_Replay)
        {
            for (int i = 0; i < checkList_note.transform.childCount; i++)
            {
                checkList_childs.Add(checkList_note.transform.GetChild(i).gameObject);
            }
            onUpdatedProgress(_player.GetChapter());
            Debug.Log("리플레이");
        }
        is_Replay=false;
        Debug.Log("OnEnable");
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
                Debug.Log("자연스러운넘김: " + curIdx);
                switch (GetTimeCurIdx)
                {
                    case (int)TimeStamp.TS_WATCHING:
                        watcingPhase();
                        time = _timeStamp[GetTimeCurIdx];
                        VideoMainDialogue();

                        break;
                    case (int)TimeStamp.TS_DIALA:
                        /*메인A*/
                        Maindial();
                        time = _timeStamp[GetTimeCurIdx];
                        break;

                    case (int)TimeStamp.TS_THINKING:
                        DeactiveMain();
                        time = _timeStamp[GetTimeCurIdx];
                        break;

                    case (int)TimeStamp.TS_DIALB:
                        Maindial();
                        time = _timeStamp[GetTimeCurIdx];
                        break;

                    case (int)TimeStamp.TS_WRITING:
                        DeactiveMain();
                        writingPhase();
                        time = _timeStamp[GetTimeCurIdx];
                        break;

                    case (int)TimeStamp.TS_PLAY:
                        phasePlay();
                        break;

                    case (int)TimeStamp.TS_NEXTCHAPTER:
                        Debug.Log("다음챕터");
                        ClickSkipBut();
                        if (eventPlay != null)
                        {
                            _objManager.memoryPool.DeactivateObject(eventPlay.name);
                        }
                        //Destroy(eventPlay);
                        //해제해야함.
                        _objManager.NextChapter();
                        if (_player.GetChapter() <= 14)
                            _player.SetChapter();
                        _objManager.transChapter(_player.GetChapter());
                        isInit = true;
                        //현재 시간을 가져와서, 다음날 한시까지 계산을 한다.
                        GetTimeCurIdx = 0;
                        time = _timeStamp[GetTimeCurIdx];
                        alter.SetActive(false);

                        SkipBackground = _objManager.memoryPool.SearchMemory("skip_sleeping");

                        if (SkipBackground == null && _objManager._chapter < 15)
                        {
                            SkipBackground = Instantiate(Resources.Load<GameObject>("skip_sleeping"), _objManager.transform.parent.parent);
                            SkipBackground.name = "skip_sleeping";
                            _objManager.memoryPool.InsertMemory(SkipBackground);
                        }
                        if (_objManager._chapter < 15)
                        {
                            _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                            onUpdatedProgress(_player.GetChapter());
                        }
                        if (SkipBackground == null && _objManager._chapter >= 15)
                        {
                            SkipBackground = Instantiate(Resources.Load<GameObject>("end_animation"), _objManager.transform.parent.parent);
                            SkipBackground.name = "end_animation";
                            _objManager.memoryPool.InsertMemory(SkipBackground);
                            _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                            is_end = true; //엔딩
                        }
                        break;
                    default:
                        Debug.LogError("알 수 없는 상태입니다: " + GetTimeCurIdx);
                        break;
                }
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
        _player.SetAlreadyEndedPhase();
        Debug.Log("Curidx: " + GetTimeCurIdx);
        DeactiveMain();
        if (curIdx < 6)
        {
            ++GetTimeCurIdx;
        }
        switch (GetTimeCurIdx)
        {
            case (int)TimeStamp.TS_WATCHING:
                watcingPhase();
                Debug.Log("시작");
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);

                VideoMainDialogue();
                
                break;
            case (int)TimeStamp.TS_DIALA:
                /*메인A*/
                Maindial();
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null)
                {
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name = "skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;

            case (int)TimeStamp.TS_THINKING:
                DeactiveMain();
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
               
                break;
            case (int)TimeStamp.TS_DIALB:
                /*메인B*/
                Maindial();
                time = _timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null)
                {
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name = "skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;

            case (int)TimeStamp.TS_WRITING:
                DeactiveMain();
                writingPhase();
                time=_timeStamp[GetTimeCurIdx];
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;

            case (int)TimeStamp.TS_PLAY:
                phasePlay();
                SkipBackground = _objManager.memoryPool.SearchMemory("skip_animation");
                if (SkipBackground == null)
                {
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name = "skip_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                break;
            case (int)TimeStamp.TS_NEXTCHAPTER:
                Debug.Log("다음챕터");
                ClickSkipBut();
                if(eventPlay!=null)
                {
                    _objManager.memoryPool.DeactivateObject(eventPlay.name);
                }
                //Destroy(eventPlay);
                //해제해야함.
                _objManager.NextChapter();
                if(_player.GetChapter()<=14)
                    _player.SetChapter();
                _objManager.transChapter(_player.GetChapter());
                isInit = true;
                //현재 시간을 가져와서, 다음날 한시까지 계산을 한다.
                GetTimeCurIdx = 0;
                time = _timeStamp[GetTimeCurIdx];
                alter.SetActive(false);

                SkipBackground = _objManager.memoryPool.SearchMemory("skip_sleeping");

                if (SkipBackground == null && _objManager._chapter<15){
                    SkipBackground = Instantiate(Resources.Load<GameObject>("skip_sleeping"), _objManager.transform.parent.parent);
                    SkipBackground.name="skip_sleeping";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                }
                if (_objManager._chapter < 15)
                {
                    _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                    onUpdatedProgress(_player.GetChapter());
                }
                if (SkipBackground == null && _objManager._chapter >= 15)  
                {
                    SkipBackground = Instantiate(Resources.Load<GameObject>("end_animation"), _objManager.transform.parent.parent);
                    SkipBackground.name = "end_animation";
                    _objManager.memoryPool.InsertMemory(SkipBackground);
                    _objManager.memoryPool.SetActiveObject(SkipBackground.name);
                    is_end = true; //엔딩
                }  
                break;
            default:
                Debug.LogError("알 수 없는 상태입니다: " + GetTimeCurIdx);
                break;
        }
       
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

        yield return new WaitForSeconds(1f);
        if(curIdx == (int)TimeStamp.TS_THINKING )
        {
            //story.SetActive(true);
        }
    }

    public void SetSleepCheckList()
    {
        ifFirstUpdate = false;
        GetTimeCurIdx = 6;
        _player.SetAlreadyEndedPhase();
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
        yield return new WaitForSeconds(6f);
        if (SkipBackground != null)
        {
            _objManager.memoryPool.DeactivateObject(SkipBackground.name);
            //Destroy(SkipBackground);
            SkipBackground = null;
        }
        OpenAllBackgroundMenu();
    }

    public void VideoMainDialogue()
    {
        //story = Instantiate(Resources.Load<GameObject>("Story/"+_player.GetChapter().ToString()), _objManager.transform.parent.parent);
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
        if(!PlayEventController.EventOn) //false 일때만 스킵이 켜지게 수정
            _timeText.transform.parent.gameObject.SetActive(true); //여기서 타임 스킵이 켜지는데 -> 문제는 스크립트 PlayEventController의 OnEnable 보다 얘가 더 나중에 실행되서 문제
        StartCoroutine("OpenCheckList");

    }
    public void Maindial()
    {
        MainMungchi = _objManager.memoryPool.SearchMemory("MainMungchi");
        if (MainMungchi == null)
        {
            MainMungchi = Instantiate(Resources.Load<GameObject>("MainMungchi"), _objManager.gameObject.transform);
            MainMungchi.name = "MainMungchi";
            _objManager.memoryPool.InsertMemory(MainMungchi);
        }
        _objManager.memoryPool.SetActiveObject(MainMungchi.name);
    }
    public void DeactiveMain()
    {
        if (MainMungchi != null)
            _objManager.memoryPool.DeactivateObject(MainMungchi.name);
    }

    void watcingPhase()
    {
        Debug.Log("watchingPhase");
        _objManager.Close();
        _objManager.RemoveWatchingObject();
    }

    void writingPhase()
    {
        phaseWriting = _objManager.memoryPool.SearchMemory("PhaseWriting");
        if (phaseWriting == null)
        {
            phaseWriting = Instantiate(Resources.Load<GameObject>("PhaseWriting"), _objManager.gameObject.transform);
            phaseWriting.name = "PhaseWriting";
            _objManager.memoryPool.InsertMemory(phaseWriting);
        }
        _objManager.memoryPool.SetActiveObject(phaseWriting.name);
        DateTime today = DateTime.Now; //현재 지금 시간
        string format = string.Format("{0} 13:00:00", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
        DateTime tomorrow = Convert.ToDateTime(format); //format 변환
        TimeSpan disTime = tomorrow - today; //두 차이를
        time = (float)disTime.TotalSeconds; //초로 변환함
    }
    void phasePlay()
    {
        if (phaseWriting != null)
        {
            _objManager.memoryPool.DeactivateObject(phaseWriting.name);
        }

        eventPlay = _objManager.memoryPool.SearchMemory("SleepSystem");
        if(eventPlay==null)
        {
            eventPlay = Instantiate(Resources.Load<GameObject>("SleepSystem"), _objManager.gameObject.transform);
            eventPlay.name="SleepSystem";
            _objManager.memoryPool.InsertMemory(eventPlay);
        }
        _objManager.memoryPool.SetActiveObject(eventPlay.name);
        eventPlay.transform.GetChild(0).gameObject.SetActive(true);
        eventPlay.transform.GetChild(1).gameObject.SetActive(false);
        DateTime today = DateTime.Now; //현재 지금 시간
        string format = string.Format("{0} 13:00:00", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
        DateTime tomorrow = Convert.ToDateTime(format); //format 변환
        TimeSpan disTime = tomorrow - today; //두 차이를
        time = (float)disTime.TotalSeconds; //초로 변환함
    }
    //void sleepPhase()
    //{
    //    //자는 모션을 setActive 
    //     eventPlay = _objManager.memoryPool.SearchMemory("SleepSystem");
    //    if(eventPlay==null)
    //    {
    //        eventPlay = Instantiate(Resources.Load<GameObject>("SleepSystem"), _objManager.gameObject.transform);
    //        eventPlay.name="SleepSystem";
    //        _objManager.memoryPool.InsertMemory(eventPlay);
    //    }
    //    _objManager.memoryPool.SetActiveObject(eventPlay.name);
    //    eventPlay.transform.GetChild(0).gameObject.SetActive(false);
    //    eventPlay.transform.GetChild(1).gameObject.SetActive(true);
    //            //애니메이션 생성
    //    DateTime today = DateTime.Now; //현재 지금 시간
    //    string format = string.Format("{0} 13:00:00", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
    //    DateTime tomorrow = Convert.ToDateTime(format); //format 변환
    //    TimeSpan disTime = tomorrow - today; //두 차이를
    //    time = (float)disTime.TotalSeconds; //초로 변환함
    //}

    public void load()
    {
        GetTimeCurIdx = _player.GetAlreadyEndedPhase();
        if(GetTimeCurIdx<_timeStamp.Length)
            time = _timeStamp[GetTimeCurIdx];
        Debug.Log("처음시작:"+GetTimeCurIdx);
        switch(GetTimeCurIdx)
        {
            case (int)TimeStamp.TS_WATCHING:
            watcingPhase();   //3.26 변경 페이즈 관련 부분 공부중
            break;

            case (int)TimeStamp.TS_DIALA://object에서 생성    //3.26 변경 페이즈 관련 부분 공부중
            Maindial();
            watcingPhase();
            break;

            case (int)TimeStamp.TS_THINKING:
            DeactiveMain();
            watcingPhase();
            //Dialogue 시작
            break;

            case (int)TimeStamp.TS_DIALB://object에서 생성    //3.26 변경 페이즈 관련 부분 공부중
            Maindial();
            watcingPhase();
            break;

            case (int)TimeStamp.TS_WRITING:
            DeactiveMain();
            watcingPhase();
            writingPhase();
            break;

            case (int)TimeStamp.TS_PLAY:
            watcingPhase();
            phasePlay();
            break;

            case (int)TimeStamp.TS_NEXTCHAPTER:
            isFirstEntry=true;
            phasePlay();
            break;
        }
    }
}
