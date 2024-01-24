//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase;
//using Firebase.Messaging;
//using System;
//using System.Text;
//using System.IO;
//public class MessagingSystem : MonoBehaviour
//{
//    const string playerInfoDataFileName="PlayerData.json";
    
//    PlayerInfo _player;
//    FirebaseApp _app;
//    string condition="NO_PARTICIPATION"; //현재는 1분간 게임을 키지 않았을 때 알림이 온다.
//    // Start is called before the first frame update
//    void Start()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith( task =>
//        {
//            if(task.Result == DependencyStatus.Available)
//            {
//                _app=FirebaseApp.DefaultInstance;
//                //이벤트 등록
//                FirebaseMessaging.TokenReceived +=OnTokenReceive;
//                FirebaseMessaging.MessageReceived+=OnMessageReceive;
//            }

//        });
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void OnTokenReceive(object sender, TokenReceivedEventArgs e)
//    {

//    }

//    void OnMessageReceive(object sender, MessageReceivedEventArgs e)
//    {
//        //json파일을 불러오고, 내부적으로 플레이어의 조건을 보고 메시지 수신 여부를 결정한다.
//        //const var playerData;
//        Debug.Log("FCM Message Received : "+ e.Message.RawData);

//        string customCondition = e.Message.Data[condition];

//        if(CheckConditionWithJson(customCondition)==false)
//        {
//            Debug.Log("조건 만족못함");
//            return;
//        }
//    }

//    bool CheckConditionWithJson(string customCondition)
//    {
//        readStringFromPlayerFile();

//        if(_player!=null)
//        {

//            return true;
//        }

//        return false;
//    }
    
//    void readStringFromPlayerFile()
//    {
//        string path=pathForDocumentsFile(playerInfoDataFileName);

//        if(File.Exists(path))
//        {
//            FileStream fileStream = new FileStream(path,FileMode.Open);
//            byte[] data = new byte[fileStream.Length];
//            fileStream.Read(data,0,data.Length);
//            fileStream.Close();
//            string json = Encoding.UTF8.GetString(data);

//            if(_player!=null){
//                _player = JsonUtility.FromJson<PlayerInfo>(json);
//                Debug.Log(_player.CurrentChapter);
//            }
//        }
//    }
//    string pathForDocumentsFile(string filename)
//    {
//        if(Application.platform==RuntimePlatform.IPhonePlayer)
//        {
//             string path = Application.dataPath.Substring(0,Application.dataPath.Length-5);
//             path=path.Substring(0,path.LastIndexOf('/'));
//             return Path.Combine(Path.Combine(path,"Documents"),filename);

//        }
//        else if(Application.platform==RuntimePlatform.Android)
//        {
//             string path = Application.persistentDataPath;
//             path=path.Substring(0,path.LastIndexOf('/'));
//             return Path.Combine(path,filename);
//        }
//        else
//        {
//             string path = Application.dataPath;
//             path=path.Substring(0,path.LastIndexOf('/'));
//            return Path.Combine(Application.dataPath,filename);
//        }

//    }
//}
