using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkipController : MonoBehaviour
{
    // Start is called before the first frame update
    //SkipController가 시간을 보관한다.

    float[] _timeStamp={3600f,7200f,1800f};

    [SerializeField]
    GameObject alter;
    [SerializeField]
    TMP_Text _timeText;

    [SerializeField]
    GameObject checkList_note;

    [SerializeField]
    GameObject menu;

    List<GameObject> checkList_childs;
    [SerializeField]
    int curIdx=0;
    float time=0;
    PlayerController _player;
    const int HOUR=3600;
    const int MIN=60;
    GameObject SkipBackground;
    ObjectManager _objManager;
    [SerializeField]
    Sprite[] fly_icon;
    string []alterTexts={"시간은 상대적인 것. 느리게 흐르는\n이 기다림의 시간을 빨리 감아 넘길까요?\n","뭉치는 달콤한 수면 후 다음 오후 한 시에 외출합니다.\n이 기다림의 시간을 빨리 감아 넘길까요?\n"};

    IEnumerator CloseCoroutine;
  
    public int GetTimeCurIdx { get => curIdx; set => curIdx = value; }

    [SerializeField]
    bool isFirstTime=true;
    bool isInit=true;
    [SerializeField]
    List<GameObject> curProgress;
    
    void Start()
    {
        curProgress=new List<GameObject>();
        checkList_childs=new List<GameObject>();
        _objManager=GameObject.FindWithTag("ObjectManager").GetComponent<ObjectManager>();
        _player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        time=_timeStamp[(curIdx)%3];
    }

    void Update()
    {
        //초 단위 3600을 나누면 h나옴 
        //1분 60
        //1시간 3600초 
        //((현재 시간)%3600)/60
        int hour=(int)time/HOUR; //3600초 나눔
        int min =((int)time%HOUR)/MIN;
        _timeText.text=(hour).ToString()+"h "+(min).ToString()+"m";
        time-=Time.deltaTime;

        if(time<=0)
        {
            if(curIdx==0)
                _objManager.Close();
            GetTimeCurIdx++;
            isFirstTime=true;

            //ObjectManager에게 전달.
        }

        if(isFirstTime){
            if(isInit&&curIdx==0) Init();

            for(int i=0;i<checkList_childs.Count;i++)
            {
                if(checkList_childs[i].name.Contains("Background")) continue;
                int idx=int.Parse(checkList_childs[i].name.Split(' ')[1]);
                if(idx<=GetTimeCurIdx)
                {
                    GameObject timeCheck=checkList_childs[i].transform.GetChild(0).gameObject;
                    if(timeCheck.activeSelf==false&&isFirstTime){
                        curProgress.Add(timeCheck);
                        StartCoroutine(CheckListOn());
                        isFirstTime=false;
                    }
                        
                }
            }
        }

    }

    public void ClickSkipBut()
    {
        if(alter!=null)
        {
            alter.SetActive(true);

            if(GetTimeCurIdx==(int)TimeStamp.TS_NEXTCHAPTER)
            {
                if(_alterText!=null)
                    _alterText.text=alterTexts[1];
            }
            else
            {
                if(_alterText!=null)
                    _alterText.text=alterTexts[0];
            }

        }
    }

    public void YesBut()
    {
        //player 시간을 빠르게 만든다.
        alter.SetActive(false);
        isFirstTime=true;
    
        switch(GetTimeCurIdx)
        {
            case (int)TimeStamp.TS_WATCHING:
                _objManager.Close();
                _objManager.RemoveWatchingObject();
                ++GetTimeCurIdx;
                time=_timeStamp[GetTimeCurIdx];
                if(SkipBackground==null)
                    SkipBackground=Instantiate(Resources.Load<GameObject>("skip_animation"),_objManager.transform.parent.parent);
            break;

            case (int)TimeStamp.TS_THINKING:
                ++GetTimeCurIdx;
                time=_timeStamp[GetTimeCurIdx];
                if(SkipBackground==null)
                    SkipBackground=Instantiate(Resources.Load<GameObject>("skip_animation"),_objManager.transform.parent.parent);
            break;

            case (int)TimeStamp.TS_WRITING:
            //시잃기 시작.
            eventPlay=Instantiate(Resources.Load<GameObject>("SleepSystem"),_objManager.gameObject.transform);
            //애니메이션 생성
            DateTime today=DateTime.Now; //현재 지금 시간
            string format=string.Format("{0} 13:00:00",DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")); //다음날 1시 까지 남은 시간
            DateTime tomorrow=Convert.ToDateTime(format); //format 변환
            TimeSpan disTime=tomorrow-today; //두 차이를
            time=(float)disTime.TotalSeconds; //초로 변환함
            ++GetTimeCurIdx;
            if(SkipBackground==null)
                SkipBackground=Instantiate(Resources.Load<GameObject>("skip_animation"),_objManager.transform.parent.parent);
            
            break;
            case (int)TimeStamp.TS_NEXTCHAPTER:
            ClickSkipBut();
            Destroy(eventPlay);
            //해제해야함.
            _objManager.NextChapter();
            _player.SetChapter();
            _objManager.transChapter(_player.GetChapter());
            isInit=true;
            //현재 시간을 가져와서, 다음날 한시까지 계산을 한다.
            GetTimeCurIdx=0;
            time=_timeStamp[GetTimeCurIdx];
            if(SkipBackground==null)
                SkipBackground=Instantiate(Resources.Load<GameObject>("skip_sleeping"),_objManager.transform.parent.parent);
    
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
        checklist.sprite=fly_icon[GetTimeCurIdx];
        for(int i=0;i<curProgress.Count;i++)
        {
            curProgress[i].SetActive(false);
        }

        curProgress.Clear();
        isInit=false;
    }
    IEnumerator OpenCheckList()
    {
        yield return new WaitForSeconds(0.5f);

        if(GetTimeCurIdx>=0&&GetTimeCurIdx-1<fly_icon.Length){
            checklist.sprite=fly_icon[GetTimeCurIdx-1];
        }

        for(int i=0;i<curProgress.Count;i++)
        {
            curProgress[i].SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        
        CloseCoroutine=CloseAlter(checkList_note);
        StartCoroutine(CloseCoroutine);

    }
    IEnumerator CloseAlter(GameObject checkList){
        
        yield return new WaitForSeconds(2f);
        checkList.SetActive(false);
    }

    public void SetSleepCheckList()
    {
        isFirstTime=false;
        GetTimeCurIdx=4;

        for(int i=0;i<checkList_childs.Count;i++)
        {
            if(checkList_childs[i].name.Contains("Background")) continue;
            int idx=int.Parse(checkList_childs[i].name.Split(' ')[1]);

            if(idx<=GetTimeCurIdx)
            {
                GameObject timeCheck=checkList_childs[i].transform.GetChild(0).gameObject;
                if(timeCheck.activeSelf==false){
                    curProgress.Add(timeCheck);
                    OpenAllBackgroundMenu();
                }
                    
            }
        }
    }
    IEnumerator CheckListOn()
    {
        if(CloseCoroutine!=null)
            StopCoroutine(CloseCoroutine);
        //3초간 애니메이션 송출 
    
        CloseAllBackgroundMenu();
        yield return new WaitForSeconds(3f);
        if(SkipBackground!=null){
            Destroy(SkipBackground);
            SkipBackground=null;
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
        checkList_note.SetActive(true);
        checkList_note.transform.parent.gameObject.SetActive(true);
        _timeText.transform.parent.gameObject.SetActive(true);
        StartCoroutine("OpenCheckList");
    
    }
}
