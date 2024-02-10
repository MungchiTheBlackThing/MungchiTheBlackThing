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
    bool isGoOut;

    public MungchiController()
    {
        isGoOut = false;
    }
}
