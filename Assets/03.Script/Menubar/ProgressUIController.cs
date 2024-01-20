using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressUIController : MonoBehaviour
{
    [SerializeField]
    int chapter = 0; //현재 day가 무엇인지 확인하기 위해서 serializeField로 변경...
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

    bool isFirst=true;
    //현재 생성된 개수를 알아야함 
    SkipController timeManager;
    //
    //임시 타이틀 배열
    #region 상세 팝업을 위한 변수
    [SerializeField]
    GameObject day_progress;
    [SerializeField]
    List<GameObject> phases;
    #endregion

    static bool isUpdate=false;

    private void OnEnable() {
        if(player==null)
            player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if(prograssUI==null){
            prograssUI=new Dictionary<int,GameObject>();
            return;
        }
        isUpdate=true;
    }
    void Start()
    {

        chapter=player.GetChapter()-1;    
        int cnt=0;
        width=dragScroller.GetComponent<RectTransform>().rect.width; //보여주는 위치 
        //Sprite[] img=Resources.LoadAll<Sprite>("Sprite/PrograssUI/"); //모든 img, 지역변수로 load
        //필수로 실행되어야 하는 것. (default 기준)
        for(int i=1;i<=4;i++)
        { 
            Init(i-1);
            //초기 사이즈로 세팅함.
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
            cnt++;
        }

        if(chapter>2)
        {
            //3부터는 day+2만큼 생성된다.
            for(int i=5;i<=chapter+2;i++)
            {
                if(i>=15){
                    dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
                    break;
                }
                Init(i-1);
                dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
                cnt++;
            }
        }
        //scrollRect 원하는 위치로 이동하는 방법
        //현재 (idx * 아이템의 크기) / (전체영역 - 보여주는 영역)
        float val=(chapter*dragIcon.GetComponent<RectTransform>().rect.width)/(dragScroller.GetComponent<ScrollRect>().content.rect.width-width);
        //중앙 위치 계산
        
        dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition=val/cnt; //(현재 위치/개수)
    }

    void Update()
    {     
        if(isUpdate)
        {
            openChapter();
            isUpdate=false;
        }    
    }

    void openChapter()
    {
        if(prograssUI.Count>=15){
            dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);
            return;
        }
        chapter=player.GetChapter();    
            
        Init(chapter+1);
        dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width+dragIcon.GetComponent<RectTransform>().rect.width,dragScroller.GetComponent<RectTransform>().rect.height);

    }
    void Init(int dayIdx)
    {
        GameObject icon=Instantiate(dragIcon,dragScroller.transform.GetChild(0));
        icon.name=MenuController.chapterList.chapters[dayIdx].chapter;
        DragIcon curIconScript=icon.GetComponent<DragIcon>();
        curIconScript.Settings(MenuController.chapterList.chapters[dayIdx].id,MenuController.chapterList.chapters[dayIdx].title,MenuController.chapterList.chapters[dayIdx].mainFilePath,"서브 타이틀 주세요");
        //만약 dayIdx <=day 일경우, lock false
        if(chapter<prograssUI.Count)
            for(int i=0;i<=chapter;i++){
                //lock을 해제함 (day 전)
                    prograssUI[i].GetComponent<DragIcon>().DestoryLock();
                //Destroy(src.gameObject.transform.GetChild(0).gameObject);
            }

        prograssUI[dayIdx]=icon;
        icon.GetComponent<Button>().onClick.AddListener(this.gameObject.GetComponent<ProgressUIController>().onClickdragIcon);
        //이미지 변경 
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

        if(day.GetComponent<DragIcon>().isLocking()){
            alter.SetActive(true);
        }else{
            dragScroller.transform.parent.gameObject.SetActive(false); //메인 다이얼로그 진행
            detailed_popup.SetActive(true); //서브 다이얼로그 설정(진행바)

            int findChapter=1;
            foreach(KeyValuePair<int, GameObject> item in prograssUI) {
                if(item.Value.name==day.name){
                    findChapter=item.Key;
                    break;
                }
            }
            detailed_popup.GetComponent<ChapterProgressManager>().PassData(MenuController.chapterList.chapters[findChapter],player);
            
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
