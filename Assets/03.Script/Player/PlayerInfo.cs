using System;

public class PlayerInfo
{
    int _id; //player 구분 키 값
    string _nickname;
    //player 입장 시간 - 입장한 날짜까지 전부 들고 있는다. 
    DateTime _datetime;
    //현재 진행 중인 챕터
    int _chapter;
    
    float _bgmVolume=50f;
    float _acousticVolume=50f;
    public PlayerInfo(int id,string nickname,int chapter){
        _id=id;
        _nickname=nickname;
        _chapter=chapter;
    }

    public float BgmVolume{ get=>_bgmVolume; set=>_bgmVolume = value; }
    public float AcousticVolume { get=>_acousticVolume; set=>_acousticVolume=value; }
    public int CurrentChapter { get => _chapter; set => _chapter = value; }
    public DateTime Datetime { get => _datetime; set => _datetime = value;}
    public string Nickname { get => _nickname; set => _nickname = value; }
    public int Id { get => _id; set => _id = value; }
}
