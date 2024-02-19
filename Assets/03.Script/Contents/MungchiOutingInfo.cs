using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MungchiOutingInfo 
{
    public int id;
    public bool watching;
    public bool thinking;

    public bool writing;
    public bool sleeping;

}

[System.Serializable]
public class MungchiOutingInfos
{
        public MungchiOutingInfo[] chapters;
}