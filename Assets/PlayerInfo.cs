using System;

public class PlayerInfo
{
    Int32 _id; //player 구분 키 값
    string _nickname;
    //player 입장 시간 - 입장한 날짜까지 전부 들고 있는다. 
    DateTime _datetime;
    //현재 진행 중인 챕터
    Int32 _chapter;

    public PlayerInfo(Int32 id,string nickname,Int32 chapter){
        _id=id;
        _nickname=nickname;
        _chapter=chapter;
    }

    public global::System.Int32 CurrentChapter { get => _chapter; set => _chapter = value; }
    public DateTime Datetime { get => _datetime; set => _datetime = value;}
    public global::System.String Nickname { get => _nickname; set => _nickname = value; }
    public global::System.Int32 Id { get => _id; set => _id = value; }
}
