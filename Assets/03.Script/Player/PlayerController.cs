using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
public class PlayerController : MonoBehaviour
{

    public static DiaryStatus diaryStatus=DiaryStatus.FISRT_NONE;
    const string playerInfoDataFileName = "PlayerData.json";
    public static PlayerInfo _player;
    //player 접속 경과 시간
    float _elapsedTime;

    //임시 저장을 위한 serialize..
    [SerializeField]
    string nickname;

    public bool isDiaryCheck = false;
    bool isNextChapter=false;
    const float _passTime = 1800f; //30분을 기준으로 한다.
    // Start is called before the first frame update
    private void Awake()
    {
        //앞으로 player을 동적으로 생성해서 관리할 예정.. 아직은 미리 초기화해서 사용한다.
        _player = new PlayerInfo(0, nickname, 1);
        //WritePlayerFile();
        readStringFromPlayerFile();
    }

    void Start()
    {
        UpdateDiary();
    }

    // Update is called once per frame
    //1시간이 되었는지 체크하기 위해서 저정용도
    void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public void Init()
    {
        diaryStatus = DiaryStatus.FISRT_NONE;
        _player = new PlayerInfo(0, nickname, 1);
        WritePlayerFile();
    }

    public float GetTime()
    {
        return _elapsedTime;
    }

    public void SetChapter()
    {
        _player.CurrentChapter += 1;
    }
    //시간 설정 : (현재 시간 - watching이 진행된 시간)+60분
    public void PassWathingTime()
    {
        //현재 진행시간에 60분을 더한다.
        //Time.deltaTime => 1초 
        //1분 => 60초
        //60분 => 60*60 => 3600초
        //30분 => 60*30 => 1800초
        //120분 => 60*120 => 7200초
        _elapsedTime += (_passTime * 2); //1시간 Update
    }
    public void PassWriting()
    {
        _elapsedTime += (_passTime);
    }
    public void PassThinkingTime()
    {
        _elapsedTime += (_passTime * 4); //2시간 1800*4 => 7200
    }
    public void EntryGame(DateTime dateTime)
    {
        if (_player != null)
        {
            _player.Datetime = dateTime;
        }
    }
    public int GetAlreadyEndedPhase()
    {
        return _player.AlreadyEndedPhase;
    }
    public void SetAlreadyEndedPhase()
    {
        UpdateDiary();
    }

    public void SetIsDiaryCheck(bool isCheck)
    {
        _player.isDiaryCheck=isCheck;
    }
    public void UpdateDiary()
    {

        Debug.Log(_player.AlreadyEndedPhase);
        if(_player.AlreadyEndedPhase==3)
        {
            diaryStatus=DiaryStatus.FISRT_NONE;
            _player.isDiaryCheck=false;
            return;
        }
        else if(_player.AlreadyEndedPhase==4)
        {
            diaryStatus=DiaryStatus.FIRST_READ;
            _player.isDiaryCheck=false;
            isNextChapter=true;
            return;
        }
        else
        {
            if(DialogueDataAsset.outingInfos.chapters[_player.CurrentChapter].diaryStatus.Length>_player.AlreadyEndedPhase)
            {
                
                if(DialogueDataAsset.outingInfos.chapters[_player.CurrentChapter].diaryStatus[_player.AlreadyEndedPhase] == true) //외출 했다면?
                { 
                    if(isNextChapter && _player.isDiaryCheck == false)
                    {
                        diaryStatus=DiaryStatus.FIRST_READ;
                    }
                    else
                    {    
                        if(isDiaryCheck)
                        {
                            isNextChapter=false;
                        }
                        diaryStatus=DiaryStatus.READ;
                    }
                }   
                else
                {
                    if(isNextChapter && _player.isDiaryCheck == false)
                    {
                        diaryStatus=DiaryStatus.FISRT_NONE;
                    }
                    else
                    {    
                        if(isDiaryCheck)
                        {
                            isNextChapter=false;
                        }
                        diaryStatus=DiaryStatus.NOT_READ;
                    }
                    //외출 안했으면 볼 수 없음.
                }
                
            }
        }
        //Debug.Log(diaryStatus);
    }
    public int GetChapter()
    {
        return _player.CurrentChapter;
    }

    public string GetNickName()
    {
        return _player.Nickname;
    }
    public void SetNickName(string InName)
    {
        _player.Nickname = InName;
    }
    public float GetAcousticVolume()
    {
        return _player.AcousticVolume;
    }
    public float GetMusicVolume()
    {
        return _player.AcousticVolume;
    }

    public void WritePlayerFile()
    {
        //PlayerInfo 클래스 내에 플레이어 정보를 Json 형태로 포멧팅 된 문자열 생성
        string jsonData = JsonUtility.ToJson(_player);
        string path = pathForDocumentsFile(playerInfoDataFileName);
        File.WriteAllText(path, jsonData);
    }

    void readStringFromPlayerFile()
    {
        string path = pathForDocumentsFile(playerInfoDataFileName);

        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string json = Encoding.UTF8.GetString(data);

            if (_player != null)
            {
                _player = JsonUtility.FromJson<PlayerInfo>(json);
            }
        }
        else
        {
            WritePlayerFile();
        }
    }
    string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Application.dataPath, filename);
        }

    }

    private void OnApplicationPause(bool pauseStatus)
    {
        WritePlayerFile();
    }
    void OnApplicationQuit()
    {
        WritePlayerFile();
    }
}
