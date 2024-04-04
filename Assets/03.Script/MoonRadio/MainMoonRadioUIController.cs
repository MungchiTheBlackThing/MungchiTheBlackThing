using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMoonRadioUIController : MonoBehaviour
{
    [SerializeField]
    GameObject earth_radio;
    [SerializeField]
    GameObject moon_radio;

    [SerializeField]
    GameObject radio_main;

    GameObject firstRadioMoon;
    [SerializeField]
    GameObject alter_Moon;
    [SerializeField]
    GameObject radio_off;
    [SerializeField]
    int moon_Cnt;

    /*
    오브젝트 풀링 예정 - MoonRadio는 밤일 때 존재해야하기 때문에 없어지지 않는다.
    하지만, 2번째 이야기 까지 보면 Destory한다.
    */
    GameObject realMoonRadio;
    GameObject realRadio;
    
    private void OnEnable() {
        moon_Cnt=0;   
    }

    public int setMoonCnt()
    {
        moon_Cnt+=1;
        return moon_Cnt;
    }
    public int getMoonCnt(){
        return moon_Cnt;
    }
    public void goEarthChannel(){

        this.gameObject.SetActive(false);
    
        if(realMoonRadio == null)
        {
            realMoonRadio = Instantiate(earth_radio,this.transform.parent);
        }else
        {
            realMoonRadio.SetActive(true);
        }
    }
    //moon_Cnt는 시스템으로 이동해야함.. 왜냐하면, 하루가 지나면 reset될 수 있도록 설정하도록. 구현
    public void goMoonChannel(){
        moon_Cnt+=1;
        if(moon_Cnt<=2){   
            if(realRadio == null)
            {
                realRadio=Instantiate(moon_radio,this.transform.parent);
            }
            else
            {
                realRadio.SetActive(true);
                realRadio.GetComponent<MoonRadioButController>().ResetTalk();
            }
        }else{
            alter_Moon.SetActive(true);
        }
    }

    public void stillChannel(){
         alter_Moon.SetActive(false);
    }

    public void OnRadioOff(){
        radio_main.SetActive(false);
        radio_off.SetActive(true);
    }

    public void yesExit(){
        //Background를 찾아 setActive true로 만든 후 파괴한다.
        for(int i=0;i<this.transform.parent.childCount;i++){
            if(this.transform.parent.GetChild(i).name=="Background"){
                this.transform.parent.GetChild(i).gameObject.SetActive(true);
            }
        }
        //무슨 함수 호출해야할거같은데..    dd
        GameObject.Find("Night").GetComponent<DefaultController>().OpenMenu();
        Destroy(this.gameObject);
    }

    public void noExit(){
        radio_main.SetActive(true);
        radio_off.SetActive(false);
    }

}
