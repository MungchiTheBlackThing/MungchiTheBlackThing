using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Assets.Script.TimeEnum;
public class GameManager : MonoBehaviour
{

    PlayerController player; //현재 플레이어의 컨트롤러 
    const int endDay = 15; //실제는 15를 초과하면 끝..
    const int setWidth=2796;
    const int setHeight=1920;

    [SerializeField]
    GameObject _backgroud;
    //현재 시간을 보여주기 위한 UI (Test용)
    GameObject _currTimeBackground; //현재 배경화면에 대한 데이터 정보 
    //현재 해당하는 시간 저장하는 list
    void Start(){
        /*데이터 베이스에 저장된 날로 업데이트 해야 한다.*/
        //currDay=0; 
        //플레이어 설정 세팅
        setPlayer();
        //게임 배경화면 설정 세팅
        initBackground();
        //player의 정보를 가져오는 database를 호출한다.
        //timesBackground=GameObject.FindGameObjectWithTag(currTime).gameObject.transform;
    }
    void setPlayer()
    {
        player=GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //현재 시간을 체크한다. -> player controller에게 현재 시간 전달 
        if(player!=null)
            player.EntryGame(DateTime.Now);
    }

    void initBackground()
    {
        //현재 시만 가져온다. 
        Int32 hh=Int32.Parse(DateTime.Now.ToString(("HH"))); //현재 시간을 가져온다
        GameObject[] backgrounds=Resources.LoadAll<GameObject>("Background/");

        for(int i=0;i<backgrounds.Length;i++)
        {
            if(hh>=(int)STime.T_DAWN&&hh<(int)STime.T_MORNING)
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_DAWN],_backgroud.transform);
                break;
            }
            if(hh<(int)STime.T_EVENING)
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_MORNING],_backgroud.transform);
                break;
            }
            else if(hh<(int)STime.T_NIGHT) 
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_EVENING],_backgroud.transform);
                break;
            }else
            {
                _currTimeBackground=Instantiate(backgrounds[(int)STimeIdx.SI_NIGHT],_backgroud.transform);
                break;
            }
        }
    }

    void Update()
    {
        setResolution();
    }

    public int getEndDay()  { return endDay; } //enum으로 변경해야함.
    // Start is called before the first frame update
    void setResolution(){
        Camera.main.transform.position=new Vector3(0,0,-10f);
        //현 기기의 너비와 높이
        int deviceWidth=Screen.width;
        int deviceHeight=Screen.height;
        //기기의 해상도 비율을 비로 조절

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기
        if ((float)setWidth / setHeight >= (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        Camera.main.aspect=Screen.width/Screen.height;
        Camera.main.ResetAspect();
    }

    /*DB 연결 시 수정되어야 하는 것
    currDay : 현재 게임에 접속된 횟수 : 날짜(일 단위)
    currTime : 현재 시간에 따른 낮인지, 저녁인지, 밤인지, 새벽인지를 나타내는 변수
    */
/*
    [SerializeField]
    bool passTimes;
    // Start is called before the first frame update
    [SerializeField]
    string currTime; //현재 시간 - > 밤낮..ㅇㅇ
    [SerializeField]
    int currDay;//진행 시간
    public int GetCurrDay(){
        return currDay;
    }
    public string GetCurrTime(){
        return currTime;
    }
    [SerializeField]
    Transform timesBackground;
    GameObject clothes;
    GameObject letter;

    [SerializeField]
    TMP_Text tmp; //임시 테스트용 삭제 예정
    // Update is called once per frame

    public void DisplayTest(){
        passTimes=true;
        tmp.text=(currDay+1).ToString();
    }
    // Update is called once per frame

*/
        /* day사용을 위해서 잠시 꺼둔다
    private void FixedUpdate() {
        if(passTimes&&currDay<15){
            currDay++;

            for(int i=0;i<timesBackground.childCount;i++){
                if(timesBackground.GetChild(i).name.Contains("bread")&&currDay==8){
                    Destroy(timesBackground.GetChild(i).gameObject);
                }

                if((currDay==(int)BinoDay.Day2)||(currDay==(int)BinoDay.Day5)||(currDay==(int)BinoDay.Day8)||(currDay==(int)BinoDay.Day10)||(currDay==(int)BinoDay.Day13)){ //currDay 2 4 6 8 에 해당하면.. 
                //bino_day+currDay.to_string() 해서, 키면된다.
                //13일차 확인하기
                    if(timesBackground.GetChild(i).name.Contains("bino")){
                        timesBackground.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }else{
                    if(timesBackground.GetChild(i).name.Contains("bino")){
                        timesBackground.GetChild(i).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }

            if((currDay==(int)LetterDay.Day3)||(currDay==(int)LetterDay.Day7)||(currDay==(int)LetterDay.Day11)||(currDay==(int)LetterDay.Day12))
            {
                if(letter==null){
                //2일-3일 간의 관계 해결해야함.. 안그러면 중복으로 데이터를 가져옴
                    letter=Instantiate(Resources.Load<GameObject>("phase_letter"),timesBackground);
                    letter.name=letter.name.Substring(0,letter.name.IndexOf('('));
                }
                
            }else{
                if(letter!=null){
                    Debug.Log(currDay);
                    Destroy(letter);
                    letter=null;
                }
            }

            GameObject book=Instantiate(Resources.Load<GameObject>("ch_books_"+currDay),timesBackground);
            book.name=book.name.Substring(0,book.name.IndexOf('('));
            if(currDay%2!=0){
                if(clothes!=null){
                    Destroy(clothes);
                    clothes=null;
                }
                clothes=Instantiate(Resources.Load<GameObject>("ch_clothes_"+currDay),timesBackground);
                clothes.name=clothes.name.Substring(0,clothes.name.IndexOf('('));
            }
            passTimes=!passTimes;
        }
    }
    */
}
