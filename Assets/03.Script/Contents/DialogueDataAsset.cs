using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDataAsset : MonoBehaviour
{
    // Start is called before the first frame update

    static public List<string> skipDialogue = new List<string>(); //달나라 주인공 이름, 스킵 
    static public MungchiOutingInfos outingInfos;
    void Awake()
    {
        //데이터 베이스를 호출한다.
        skipDialogue.Add("요즘 사람들은 더 이상 \"라디오\"라는 걸 사용하지 않는다는데, 어떻게 그래?");
        skipDialogue.Add("인간은 성장의 동물. 그들은 라디오에서 mp3로, 다시 핸드폰이라는 기계로 성장했음.");
        skipDialogue.Add("~♫♮♭♩♫&?");
        var loadedJson = Resources.Load<TextAsset>("Json/OutingInformationData");

        if(loadedJson)
        {
            outingInfos = JsonUtility.FromJson<MungchiOutingInfos>(loadedJson.ToString());
        }
    }
}
