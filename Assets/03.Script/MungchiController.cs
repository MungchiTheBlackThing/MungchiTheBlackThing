using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESTATUS : ushort
{
    GOOUT,
    ISHOME,
    END
}
public class MungchiController : MonoBehaviour
{
    public bool isGoOut;

    public MungchiController()
    {
        isGoOut = false;
    }

    public void OnNextPhase(bool isGoOut)
    {
        if(this.isGoOut != isGoOut)
        {
            this.isGoOut=isGoOut;
        }

        if(this.isGoOut)
        {
            //true면, 외출했다는 소리. 즉, 다이어리를 볼 수 있음.
            if(PlayerController.diaryStatus != DiaryStatus.FISRT_NONE) 
            {
                PlayerController.diaryStatus = DiaryStatus.FIRST_READ;
            }else
            {
                PlayerController.diaryStatus = DiaryStatus.READ;
            }
        }
        else
        {
            
        }
    }
}
