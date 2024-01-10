using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityController : MonoBehaviour
{

    public enum Type:int
    {
        Discode,
        Instagram,
        Twitter,
        Invailed,
    };

    readonly string[] url = {"https://discord.gg/mfK4J9tsHK","https://www.instagram.com/moonstew_games/","https://twitter.com/moonstew_games"};

    public void OnClick(string type)
    {
        Type urlType=Type.Invailed;
        switch(type)
        {
            case "Discode":
                urlType=Type.Discode;
            break;
            case "Instagram":
                urlType=Type.Instagram;
            break;
            case "Twitter":
                urlType=Type.Twitter;
            break;    
        }
        if(urlType!=Type.Invailed)
            OpenURL(urlType);
    }
    void OpenURL(Type type)
    {
        Application.OpenURL(url[(int)type]);
    }
}
