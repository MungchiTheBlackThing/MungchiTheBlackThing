using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Data 
{
    public string title;
    public string script;
    public string[] path;
}

public class ProgressData : MonoBehaviour {

    Dictionary<int,Data> progressData;

    //비동기적으로 데이터 로드한다. 왜냐하면, 시작과 동시에 필요하지 않고, 메뉴를 눌렀을 때만 데이터가 존재하면 된다.
    void Start()
    {
        //데이터 로드 
        progressData=new Dictionary<int, Data>();
    }   
}
