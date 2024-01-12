using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProgressUIController : MonoBehaviour
{
    [SerializeField]
    int day = 0; //현재 day가 무엇인지 확인하기 위해서 serializeField로 변경...
    //1Day로 바꿀 예정
    [SerializeField]
    bool isInstant;
    [SerializeField]
    GameObject dragIcon;
    [SerializeField]
    GameObject dragScroller;
    // Update is called once per frame

    [SerializeField]
    GameObject alter;
    [SerializeField]
    GameObject detailed_popup;
    [SerializeField]
    GameObject menu_default;

    PlayerController player;
    [SerializeField]//임시러 보기 위해서 
    Dictionary<int,GameObject> prograssUI; //prograss UI를 전체 관리할 예정 
    float width=0;


    //현재 생성된 개수를 알아야함 
    SkipController timeManager;
    //
    //임시 타이틀 배열
    string []titles = {"언어","결핍","얼굴들","과거","허망","우리","코미디","쌍둥이","사라지는 것","벽","환상","심연","남는 것들","검정"};

    #region 상세 팝업을 위한 변수
    [SerializeField]
    GameObject day_progress;
    [SerializeField]
    List<GameObject> phases;
    #endregion

    void OnEnable()
    {
        if(timeManager==null)
        {
            timeManager=GameObject.Find("TimeManager").GetComponent<SkipController>();
            timeManager.nextChangeChapter+=nextChapter;
        }
    }
    void Start()
    {
        timeManager=GameObject.Find("TimeManager").GetComponent<SkipController>();
        timeManager.nextChangeChapter+=nextChapter;
        
        int cnt=0;
        width=dragScroller.GetComponent<RectTransform>().rect.width; //보여주는 위치 
        player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        day=player.GetChapter();
        prograssUI=new Dictionary<int,GameObject>(); 
        //Sprite[] img=Resources.LoadAll<Sprite>("Sprite/PrograssUI/"); //모든 img, 지역변수로 load
        //필수로 실행되어야 하는 것. (default 기준)
        for(int i=1;i<=4;i++)
        { 
            Init(i);
            //초기 사이즈로 세팅함.
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
            cnt++;
        }

        if(day>2)
        {
            //3부터는 day+2만큼 생성된다.
            for(int i=5;i<=day+2;i++)
            {
                if(i>=15){
                    dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
                    break;
                }
                Init(i);
                dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
                cnt++;
            }
        }
        //scrollRect 원하는 위치로 이동하는 방법
        //현재 (idx * 아이템의 크기) / (전체영역 - 보여주는 영역)
        float val=(day*dragIcon.GetComponent<RectTransform>().rect.width)/(dragScroller.GetComponent<ScrollRect>().content.rect.width-width);
        //중앙 위치 계산
        
        dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition=val/cnt; //(현재 위치/개수)

    }
    void openChapter(int chapter)
    {
            //3부터는 day+2만큼 생성된다.
        Debug.Log(prograssUI.Count);
        if(prograssUI.Count>=15){
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
            return;
        }
        Init(prograssUI.Count);
        dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);

        day=player.GetChapter();
    }
    void Init(int dayIdx)
    {
        
        GameObject icon=Instantiate(dragIcon,dragScroller.transform.GetChild(0));
        //Title - title
        //Src - Scr-> Background(Mask) -> Background(Img)의 Script 수정
        //Script - Script
        DragIcon curIconScript=icon.GetComponent<DragIcon>();
        TMP_Text title = icon.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        Image src = icon.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        TMP_Text script = icon.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>();
        // hard cording? 
        icon.name=dayIdx.ToString()+"days";
        curIconScript.chapter=dayIdx;
        /*대사 프로토 타입*/
        curIconScript.title="제 "+dayIdx.ToString()+"장. "+titles[dayIdx-1];
        title.text=curIconScript.title;
        /**/
        curIconScript.source=Resources.Load<Sprite>("Sprites/PrograssUI/"+dayIdx);;

        src.sprite=curIconScript.source;
        //만약 dayIdx <=day 일경우, lock false
        if(dayIdx<=day)
        {
            //lock을 해제함 (day 전)
            Destroy(src.gameObject.transform.GetChild(0).gameObject);
        }
        prograssUI[dayIdx]=icon;
        icon.GetComponent<Button>().onClick.AddListener(this.gameObject.GetComponent<ProgressUIController>().onClickdragIcon);
        //이미지 변경 
    }

    public void nextChapter()
    {
        openChapter(player.GetChapter());
    }
    public void Scroll()
    {
        float val=dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition;
        if(val>=1f)
        {
            alter.SetActive(true);
        }
    }
    public void onClickdragIcon(){

        GameObject day=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if(day.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.childCount!=0){
            alter.SetActive(true);
        }else{
            dragScroller.transform.parent.gameObject.SetActive(false);
            //텍스트 수정 부분 
            day_progress.GetComponent<TMP_Text>().text=day.GetComponent<DragIcon>().title;
            detailed_popup.SetActive(true);
        }
    }

    public void canceled(){
        alter.SetActive(false);
    }
    
    public void exit(){
        //현재 게임 오브젝트가 DayProgress_Default이면, DayProgressUI SetActive한다.

        if(detailed_popup.activeSelf){
            detailed_popup.SetActive(false);
            dragScroller.transform.parent.gameObject.SetActive(true);
        }else{
            menu_default.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

}
