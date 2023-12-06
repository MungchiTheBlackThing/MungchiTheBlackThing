using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject close;
    public GameObject delobject;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Tutorial");
    }
    // Update is called once per frame
    public void del_background()
    {
        delobject.SetActive(false);
    }

    public void closewardobe()
    {
        close.SetActive(true);
    }
    public void click()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Click");
    }
    public void twinkle()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Twinke");
    }
}
