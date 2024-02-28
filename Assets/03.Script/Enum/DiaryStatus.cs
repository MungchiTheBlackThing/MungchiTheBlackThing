using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiaryStatus
{
    FISRT_NONE, //처음이지만, 읽을 수 없는 상태
    FIRST_READ, //처음 읽은 상태
    READ,//처음은 아님, 그런데 읽을 수 있는 상태
    NOT_READ,//처음은 아니고, 읽을 수 없는 상태
    END,
}
