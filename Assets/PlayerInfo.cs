using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//enum 파일 만들어서 정리해 놓아야할듯
public enum LetterDay{
    Day3=3,
    Day7=7,
    Day11=11,
    Day12=12

}
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
    [SerializeField]
    Transform timesBackground;
    void Start(){
        /*데이터 베이스에 저장된 날로 업데이트 해야 한다.*/
        currDay=0; 
        timesBackground=GameObject.FindGameObjectWithTag(currTime).gameObject.transform;
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() {
        if(passTimes&&currDay<15){
            currDay++;

            for(int i=0;i<timesBackground.childCount;i++){
                if(timesBackground.GetChild(i).name.Contains("bread")&&currDay==8){
                    Destroy(timesBackground.GetChild(i).gameObject);
                }
                if(timesBackground.GetChild(i).name.Contains("letter")){
                    Destroy(timesBackground.GetChild(i).gameObject);
                }else if(currDay==(int)LetterDay.Day12||currDay==(int)LetterDay.Day3||currDay==(int)LetterDay.Day7||currDay==(int)LetterDay.Day11){
                    //2일-3일 간의 관계 해결해야함.. 안그러면 중복으로 데이터를 가져옴
                    GameObject obj=Instantiate(Resources.Load<GameObject>("phase_letter"),timesBackground);
                    break;
                }

                if(currDay==2){ //currDay 2 4 6 8 에 해당하면.. 
                //bino_day+currDay.to_string() 해서, 키면된다.
                    if(timesBackground.GetChild(i).name.Contains("bino")){
                        timesBackground.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            Instantiate(Resources.Load<GameObject>("ch_books_"+currDay),timesBackground);
            if(currDay%2==0){
                Instantiate(Resources.Load<GameObject>("ch_clothes_"+currDay),timesBackground);
            }
            passTimes=!passTimes;
        }
    }
}
