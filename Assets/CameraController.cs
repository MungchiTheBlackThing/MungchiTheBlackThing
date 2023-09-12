using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CameraController : MonoBehaviour,IPointerClickHandler
{

    [SerializeField]
    RectTransform shotArea;

    //아이폰용코드 작성 -> 아이폰 도구인 xcode다운로드 후에..


    //갤럭시용 코드 작성 -> 이후 디자인 도안 나오고


    //스크린샷용 코드 작성 -> 초기 default
    public void OnPointerClick(PointerEventData eventData){

        //버튼을 누르면 지정된 부분만 스크린샷
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "BRUNCH-SCREENSHOT-" + timestamp + ".png";
        
        //#if UNITY_IPHONE || UNITY_ANDROID
        //CaptureScreenForMobile(fileName);
        //#else
        CaptureScreenForPC(fileName);
        //#endif
    }
    private void CaptureScreenForPC(string fileName)
    {
        //Texture2D screenTex = new Texture2D(shotArea.rect.width, shotArea.rect.height, TextureFormat.RGB24, false);
        //Rect area = new Rect(shotArea.position.x,shotArea.position.y, shotArea.rect.width, shotArea.rect.height);
        //screenTex.ReadPixels(area, 0, 0);
        Debug.Log("촬영");
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    private void CaptureScreenForMobile(string fileName){
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture
        string albumName = "BRUNCH";

        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // cleanup
        Destroy(texture);
    }

}
