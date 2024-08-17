using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.TimeEnum;
public class ObjectManager : MonoBehaviour
{
    // Start is called before the first frame update

    public MemoryPool memoryPool;
    //현재 아래 생성된 object를 가지고 온다.
    GameObject _timesBackground;
    //objects를 여러번 반복하지 않기 위해서 특정 오브젝트 (즉, 변화하는 오브젝트는 들고있는다.)
    [SerializeField]
    GameObject _binocular;
    [SerializeField]
    GameObject _bread;

    [SerializeField]
    GameObject _letter;
    [SerializeField]
    GameObject _preClothes;
    [SerializeField]
    GameObject _diary;
    [SerializeField]
    GameObject _dotOrder;
    [SerializeField]
    GameObject _bookPile;

    [SerializeField]
    GameObject _dots;

    [SerializeField]
    GameObject _sub;

    [SerializeField]
    GameObject _main;

    [SerializeField]
    GameObject _cup; //보이기 위한 용도

    [SerializeField]
    GameObject _deathnote;

    [SerializeField]
    GameObject _time;

    [SerializeField]
    GameObject _iconcheck;

    bool _isChapterUpdate = true;
    public int _chapter = 0;
    GameObject[] uiList;
    
    PlayerController _player;

    DotState dotState = DotState.Defualt;
    string animState = "anim_mud";
    bool isFirstUpdate=true;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        memoryPool=new MemoryPool();
        GameObject sub = Instantiate(_sub, this.transform);
        //GameObject main = Instantiate(_main, this.transform);
        _time = GameObject.Find("TimeUI");
        _iconcheck = GameObject.Find("icon_checklist");
    }
    void Init()
    {
        uiList = GameObject.FindGameObjectsWithTag("UI");
        //Null일때 초기화, 재사용성을 위해 함수로 빼놓음
        if (_timesBackground == null)
        {
            _timesBackground = transform.GetChild(0).gameObject;

            for (int i = 0; i < _timesBackground.transform.childCount; i++)
            {
                GameObject child = _timesBackground.transform.GetChild(i).gameObject;
                if (child.name.Contains("bread"))
                    _bread = child;
                if (child.name.Contains("bino"))
                    _binocular = child;
                if (child.name.Contains("clothes"))
                    _preClothes = child;
                if (child.name.Contains("phase_diary"))
                    _diary = child;
                if (child.name.Contains("bookpile"))
                    _bookPile = child;
                if (child.name.Contains("fix_dot_order"))
                    _dotOrder = child;
            }
            InitBackground();
        }

        //현재 배경 초기화
    }

    void SetChapterUpdate()
    {
        //현재 gameManager가 전달한 chapter을 받아서 background 설치 
        //초반에, 뭉치 외출을 조절하는 로직같음
        if (isFirstUpdate)
        {
            Debug.Log("챕터: "+_chapter);
            switch (_chapter)
            {
                case (int)ChapterDay.C_1DAY:
                break;
                case (int)ChapterDay.C_2DAY:
                case (int)ChapterDay.C_5DAY:
                case (int)ChapterDay.C_8DAY:
                case (int)ChapterDay.C_10DAY:
                case (int)ChapterDay.C_13DAY:
                //현재 phase가 watch일 때
                    GoToOther();
                    SetBino();
                    //passTime을 누를 시 player time+=60, case문 적용 안되도록 한다.
                    break;
                case (int)ChapterDay.C_4DAY:
                case (int)ChapterDay.C_6DAY:

                    break;
                case (int)ChapterDay.C_9DAY:

                    ChangeFromBreadToCup();
           
                    break;
                case (int)ChapterDay.C_12DAY:

                case (int)ChapterDay.C_14DAY:              
                    ChangeFromBreadToCup();
                    isAtHome();
                    SetLetter();
                    break;
                case (int)ChapterDay.END:
                    Debug.Log("end");
                    SkipController.is_end = true;
                    EndPhase();
                    break;
                default:
                    ChangeFromBreadToCup();
                    GoToOther();
                    SetLetter();
                    break;
            }

            if(_player.GetAlreadyEndedPhase()>=2)
            {
                RemoveWatchingObject();
            }
            isFirstUpdate=false;
        }
        if (_chapter != 1 && _chapter < 15)
        {
            for (int i = 2; i <= _chapter; i++)
            {
                SetBook(i);
            }
            ChangeClothes(_chapter);
        }
        if (_chapter >= 15)
        {
            Debug.Log("end2");
            for (int i = 2; i <= 14; i++)
            {
                SetBook(i);
            }
            EndPhase();
        }
        //업데이트에 필요한 코드 추가예정.
    }

    public void ChangeFromBreadToCup()
    {
        if (_bread != null)
        {
            _bread.SetActive(false);
            if (_cup != null)
            {
                _cup.SetActive(true);
            }
            else
            {
                _cup = Instantiate(Resources.Load<GameObject>(_timesBackground.name + "/ch_cup"), _bread.transform.parent);
            }
        }
    }
    public void transChapter(int chapter)
    {
        _chapter = chapter;
        _isChapterUpdate = true;
    }
    public void RemoveWatchingObject()
    {
        if (_dots != null)
        {
            _dots.SetActive(false);
        }
        if (_bookPile != null)
        {
            _bookPile.SetActive(false);
        }
    }
    void GoToOther()
    {
        if (_dots != null)
            _dots.SetActive(false);
        _bookPile.SetActive(true);
    }
    void isAtHome()
    {
        if (_dots == null)
        {      
            _dots = Instantiate(Resources.Load<GameObject>("Dot"), _dotOrder.transform);
        }
        _dots.GetComponent<Animator>().SetBool("isAtHome", true);
        _dots.GetComponent<Animator>().SetInteger("Day", _chapter);
        _bookPile.SetActive(false);
    }

    void InitBackground()
    {
        if (_chapter == 1) return;
        ChangeClothes(_chapter);
    }

    public void DestroyBread()
    {
        if (_bread != null)
        {
            Destroy(_bread);
            _bread = null;
        }
    }
    public void NextChapter()
    {
        this.GetComponent<ScrollRect>().horizontal = true;
        for (int i = 0; i < _diary.transform.childCount; i++)
        {
            Transform child = _diary.transform.GetChild(i);
            if (child.name.Contains("light"))
                child.gameObject.SetActive(false);
        }
    }
    /* - chapter : Watching */
    public void SetBino()
    {
        if (_binocular != null)
        {
            Debug.Log("바이노 왜 안켜짐 ㅠ");
            _binocular.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Close()
    {

        if (_binocular != null && _binocular.transform.GetChild(0).gameObject.activeSelf == true)
        {
            _binocular.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (_letter != null)
        {
            Destroy(_letter);
            _letter = null;
        }
    }
    public void SetLetter()
    {
        if (_letter == null)
        {
            //2일-3일 간의 관계 해결해야함.. 안그러면 중복으로 데이터를 가져옴
            _letter = Instantiate(Resources.Load<GameObject>(_timesBackground.name + "/phase_letter"), _timesBackground.transform);
            _letter.name = _letter.name.Substring(0, _letter.name.IndexOf('('));
        }
    }
    public void CloseLetter()
    {
        if (_letter != null)
        {
            for (int i = 0; i < uiList.Length; i++)
                uiList[i].SetActive(true);
            Destroy(_letter);
            _letter = null;
        }
    }

    public void SetBook(int currDay)
    {
        string bookName = _timesBackground.name + "/ch_books_" + currDay;
        GameObject existingBook = GameObject.Find(bookName);
        if (!existingBook)
        {
            GameObject bookPrefab = Resources.Load<GameObject>(bookName);
            GameObject book = Instantiate(bookPrefab, _timesBackground.transform);
            book.name = bookName;
        }
    }

    public void ChangeClothes(int currDay)
    {
        if (currDay % 2 != 0 && currDay < 15)
        {
            if (_preClothes != null)
            {
                Destroy(_preClothes);
            }
            _preClothes = Instantiate(Resources.Load<GameObject>(_timesBackground.name + "/ch_clothes_" + currDay), _timesBackground.transform);
            if(_preClothes)
                _preClothes.name = _preClothes.name.Substring(0, _preClothes.name.IndexOf('('));
        }
    }
    public void EndPhase()
    {
        Debug.Log("EndPhase");
        _time.SetActive(false);
        _iconcheck.SetActive(false);
        SkipController.is_end = true;
        if (_deathnote != null)
        {
            Destroy(_deathnote);
        }
        _deathnote = Instantiate(Resources.Load<GameObject>(_timesBackground.name + "/deathnote"), _timesBackground.transform);
        _deathnote.transform.SetAsFirstSibling();
        _bookPile.SetActive(false);
        if (_cup != null)
        {
            _cup.SetActive(false);
            if (_bread != null)
            {
                _bread.SetActive(true);
            }
        }
        PlayerController.diaryStatus = DiaryStatus.READ;

    }

    void Update()
    {
        Init();

        if (_isChapterUpdate)
        {
            SetChapterUpdate();
            _isChapterUpdate = false; //1번만 업데이트 하기 위해서 필요함
            //isChapterUpdate는 time을 누르고, chapter가 다음 챕터로 넘어갈때 true로 변경된다.
        }
    }


    public void OnChangedScroll()
    {
        GameObject sleepDot = GameObject.Find("Sleep_Dots");

        if(sleepDot == null)   return ;
        Vector3 movePos = sleepDot.GetComponent<RectTransform>().position;
        sleepDot.GetComponent<FallingObjectSpawner2>().SetPosition(movePos);
        
        Debug.Log("스크롤 된 위치 " + movePos);
    }
}