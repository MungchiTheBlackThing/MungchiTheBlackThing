using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using TMPro;

public class StoryDial : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips; // 비디오 클립들을 저장할 배열
    public string[] subtitles;
    public TMP_Text TextMesh;
    private int currentClipIndex = 0; // 현재 재생 중인 비디오 클립의 인덱스
    public RenderTexture rd;
    const int setWidth = 2796;
    const int setHeight = 1920;

    void Start()
    {
        // 비디오 플레이어 컴포넌트 설정
        videoPlayer.loopPointReached += OnVideoClipFinished; // 비디오 클립 재생 완료 시 호출될 메소드 등록
        // 초기 비디오 클립 재생
        PlayNextVideoClip();
    }

    void PlayNextVideoClip()
    {
        if (currentClipIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
            TextMesh.text = subtitles[currentClipIndex];
            currentClipIndex++;
        }
        else
        {
            Debug.Log("모든 비디오 클립 재생 완료");
            // 모든 비디오 클립 재생이 완료된 경우에 대한 처리를 추가할 수 있습니다.
        }
    }

    void OnVideoClipFinished(VideoPlayer vp)
    {
        // 비디오 클립 재생이 끝나면 다음 비디오 클립 재생
        PlayNextVideoClip();
    }

    public void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }

    void SetResolution()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        //현 기기의 너비와 높이
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        //기기의 해상도 비율을 비로 조절

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기
        if ((float)setWidth / setHeight >= (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
        Camera.main.aspect = Screen.width / Screen.height;
        Camera.main.ResetAspect();
    }
}
