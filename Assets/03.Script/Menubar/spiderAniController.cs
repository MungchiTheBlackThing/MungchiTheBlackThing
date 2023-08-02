using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAniController : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent=this.transform.parent.gameObject;
    }

    public void AniExit(){
        parent.GetComponent<MenuController>().MenuAniExit();
    }
}
