using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PoemInfo
{
    public int id;
    public int page;
    public string textKor;
    public string textEng;
}


[System.Serializable]
public class Poems
{
        public PoemInfo[] poems;
}
