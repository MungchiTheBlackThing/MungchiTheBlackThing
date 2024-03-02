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

    [Header("0번이 기본입니다. 번호 작성 후 원래대로 돌아갈 때 11번 체크 후 넘어가주세요.")]
    [Range(0,14)]
    [SerializeField]
    int activityId=0;

    Animator animator;

    public MungchiController()
    {
        isGoOut = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        animator.SetInteger("ActivityID",activityId);
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
