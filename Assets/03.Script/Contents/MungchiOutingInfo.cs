using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MungchiOutingInfo 
{
    public int id;
    public bool[] diaryStatus;
}

[System.Serializable]
public class MungchiOutingInfos
{
        public MungchiOutingInfo[] chapters;
}