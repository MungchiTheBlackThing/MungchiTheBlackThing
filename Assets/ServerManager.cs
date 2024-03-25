using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Best.HTTP;
using Best.SocketIO;
using Best.SocketIO.Events;
using System.Threading;

public class ServerManager : MonoBehaviour
{

    [System.Serializable]
    public class DialogueLine
    {
        public string scriptNumber;
        public int lineKey;
        public string background;
        public string actor;
        public string dotBody;
        public string dotExpression;
        public string textType;
        public string korText;
        public string engText;
        public string nextLineKey;
        public string animScene;
        public string afterScript;
        public string deathNote;
    }

    // JSON 데이터 클래스 정의
    [System.Serializable]
    public class RootObject
    {
        public DialogueLine[] ch1_main;
    }
    SocketManager manager;
    string jsonString = "";
    bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        manager = new SocketManager(new Uri("http://3.36.119.54:8080"));
        manager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        this.manager.Open();

        StartCoroutine("GetDialogue", 2);
    }

    void OnConnected(ConnectResponse resp)
    {
        Debug.Log("Connected!");
    }
    void OnDestroy()
    {
        this.manager?.Close();
        this.manager = null;
    }

    IEnumerator GetDialogue(int curDay)
    {
        manager.Socket.Emit("pass_script");
        manager.Socket.On<string>("give_script", ReadScript);

        yield return null;
    }
    private void ReadScript(string script)
    {
        jsonString = script.ToString();
    }

    private void Update()
    {

        if (isFirst && jsonString.Length != 0)
        {
            Debug.Log(jsonString);
            RootObject rootObject = JsonUtility.FromJson<RootObject>(jsonString);

            // 결과 출력
            foreach (DialogueLine dialogueLine in rootObject.ch1_main)
            {
                Debug.Log("Script Number: " + dialogueLine.scriptNumber);
                Debug.Log("Line Key: " + dialogueLine.lineKey);
                Debug.Log("Background: " + dialogueLine.background);
                Debug.Log("Actor: " + dialogueLine.actor);
                Debug.Log("Korean Text: " + dialogueLine.korText);
                Debug.Log("English Text: " + dialogueLine.engText);
                Debug.Log("Next Line Key: " + dialogueLine.nextLineKey);
                Debug.Log("Dot Expression: " + dialogueLine.dotExpression);
                Debug.Log("Text Type: " + dialogueLine.textType);
                Debug.Log("Anim Scene: " + dialogueLine.animScene);
                Debug.Log("After Script: " + dialogueLine.afterScript);
                Debug.Log("Death Note: " + dialogueLine.deathNote);
            }
            isFirst = false;
        }
    }
}
