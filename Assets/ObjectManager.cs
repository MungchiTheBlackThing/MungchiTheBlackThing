using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Start is called before the first frame update

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

    int _chapter=0;

    void Update()
    {
        Init();
    }
    void Init()
    {
        //Null일때 초기화, 재사용성을 위해 함수로 빼놓음
        if(_timesBackground==null)
        {
            _timesBackground=transform.GetChild(0).gameObject;

            for(int i=0;i<_timesBackground.transform.childCount;i++)
            {
                GameObject child = _timesBackground.transform.GetChild(i).gameObject;
                if(child.name.Contains("bread"))
                    _bread=child;
                if(child.name.Contains("bino"))
                    _binocular=child;
                if(child.name.Contains("clothes"))
                    _preClothes=child;
            }
            InitBackground();
        }

        //현재 배경 초기화
    }

    public void transChapter(int chapter){
        _chapter=chapter;
    }

    void InitBackground()
    {
        if(_chapter==1) return ;
        for(int i=2;i<=_chapter;i++)
        {
            SetBook(i);
        }
        ChangeClothes(_chapter);
    }

    public void DestroyBread()
    {
        if(_bread!=null)
        {
            Destroy(_bread);
            _bread=null;
        }
    }
    /* - chapter : Watching */
    public void SetBino()
    {
        if(_binocular!=null){
            _binocular.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CloseBino()
    {
        if(_binocular!=null){
            _binocular.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void SetLetter()
    {
        if(_letter==null){
                //2일-3일 간의 관계 해결해야함.. 안그러면 중복으로 데이터를 가져옴
            _letter=Instantiate(Resources.Load<GameObject>("phase_letter"),_timesBackground.transform);
            _letter.name=_letter.name.Substring(0,_letter.name.IndexOf('('));
        }
    }
    public void CloseLetter()
    {
        if(_letter!=null){
            Destroy(_letter);
            _letter=null;
        }
    }

    public void SetBook(int currDay)
    {
        Debug.Log(_timesBackground.name);
        GameObject book=Instantiate(Resources.Load<GameObject>(_timesBackground.name+"/ch_books_"+currDay),_timesBackground.transform);
        book.name=book.name.Substring(0,book.name.IndexOf('('));
    }

    public void ChangeClothes(int currDay)
    {
        if(currDay%2!=0){
            if(_preClothes!=null){
                Destroy(_preClothes);
            }
            _preClothes=Instantiate(Resources.Load<GameObject>(_timesBackground.name+"/ch_clothes_"+currDay),_timesBackground.transform);
            _preClothes.name=_preClothes.name.Substring(0,_preClothes.name.IndexOf('('));
        }
    }
}
