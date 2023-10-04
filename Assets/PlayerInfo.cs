using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Script.DaysEnum;
//enum 파일 만들어서 정리해 놓아야할듯
public class PlayerInfo : MonoBehaviour
{
    /*DB 연결 시 수정되어야 하는 것
    currDay : 현재 게임에 접속된 횟수 : 날짜(일 단위)
    currTime : 현재 시간에 따른 낮인지, 저녁인지, 밤인지, 새벽인지를 나타내는 변수
    */
    [SerializeField]
    bool passTimes;
    // Start is called before the first frame update
    [SerializeField]
    string currTime;
    [SerializeField]
    int currDay;
    public int GetCurrDay(){
        return currDay;
    }
    [SerializeField]
    Transform timesBackground;
    GameObject clothes;
    GameObject letter;

    [SerializeField]
    TMP_Text tmp; //임시 테스트용 삭제 예정
    void Start(){
        /*데이터 베이스에 저장된 날로 업데이트 해야 한다.*/
        currDay=0; 
        timesBackground=GameObject.FindGameObjectWithTag(currTime).gameObject.transform;
    }
    // Update is called once per frame

    public void DisplayTest(){
        passTimes=true;
        tmp.text=(currDay+1).ToString();
    }
    private void FixedUpdate() {
        if(passTimes&&currDay<15){
            currDay++;

            for(int i=0;i<timesBackground.childCount;i++){
                if(timesBackground.GetChild(i).name.Contains("bread")&&currDay==8){
                    Destroy(timesBackground.GetChild(i).gameObject);
                }

                if((currDay==(int)BinoDay.Day2)||(currDay==(int)BinoDay.Day5)||(currDay==(int)BinoDay.Day8)||(currDay==(int)BinoDay.Day10)||(currDay==(int)BinoDay.Day13)){ //currDay 2 4 6 8 에 해당하면.. 
                //bino_day+currDay.to_string() 해서, 키면된다.
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
                if(letter==null)
                //2일-3일 간의 관계 해결해야함.. 안그러면 중복으로 데이터를 가져옴
                    letter=Instantiate(Resources.Load<GameObject>("phase_letter"),timesBackground);
            }else{
                if(letter!=null){
                    Debug.Log(currDay);
                    Destroy(letter);
                    letter=null;
                }
            }

            Instantiate(Resources.Load<GameObject>("ch_books_"+currDay),timesBackground);
            if(currDay%2!=0){
                if(clothes!=null){
                    Destroy(clothes);
                    clothes=null;
                }
                clothes=Instantiate(Resources.Load<GameObject>("ch_clothes_"+currDay),timesBackground);
            }
            passTimes=!passTimes;
        }
    }
}
