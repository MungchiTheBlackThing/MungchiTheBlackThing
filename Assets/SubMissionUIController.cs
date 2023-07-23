using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMissionUIController : MonoBehaviour
{
    GameObject pre_alter;

    public void checkedLocker(){
        GameObject icon = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        for(int i=0;i<icon.transform.childCount;i++){
            if(icon.transform.GetChild(i).name=="Sub_Locker"){
                if(icon.transform.GetChild(i).gameObject.activeSelf){
                    icon.transform.GetChild(i+1).gameObject.SetActive(true);
                    if(pre_alter!=null)
                        pre_alter.SetActive(false);
                    pre_alter=icon.transform.GetChild(i+1).gameObject;
                    break;
                }
                if(pre_alter!=null)
                    pre_alter.SetActive(false);
                
            }
        }

    }
}
