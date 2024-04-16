using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("GoDialogue");   
    }

    IEnumerator GoDialogue()
    {
        yield return new WaitForSeconds(3f); //다음에는 데이터베이스 호출 
        //씬이동
        SceneManager.LoadScene("01.Scenes/Test");
    }
}
