using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMissionUIController : MonoBehaviour
{
    Dictionary<string,GameObject> pre_alter;
    void Start()
    {
        pre_alter=new Dictionary<string, GameObject>();
    }
    public void checkedLocker(){
        Transform icon = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.parent;

        if(pre_alter.ContainsKey(icon.name))
        {
            if(pre_alter[icon.name].activeSelf)
            {
                pre_alter[icon.name].SetActive(false);
            }
            else
            {
                pre_alter[icon.name].SetActive(true);
            }
            
            return;
        }
        for(int i=0;i<icon.childCount;i++){
            if(icon.GetChild(i).name=="alert"){
                icon.GetChild(i).gameObject.SetActive(true);
                if(pre_alter.ContainsKey(icon.name)==false)
                {
                    pre_alter.Add(icon.name,icon.GetChild(i).gameObject);
                }
                break;
            }
        }
    }
}
