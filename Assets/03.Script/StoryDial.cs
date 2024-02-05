using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using TMPro;

public class StoryDial : MonoBehaviour
{
    [Serializable]
    public class Maparray
    {
        public VideoClip videoClip;
        public string[] subtitles;
    }

    public VideoPlayer videoPlayer;
    public Maparray[] videoMaps; // 비디오 클립들과 자막들을 저장할 배열
    public TMP_Text TextMesh;
    const int setWidth = 2796;
    const int setHeight = 1920;
    public int currentClipIndex = 0; // 현재 재생 중인 비디오 클립의 인덱스
    public int currentSubtitleIndex = 0; // 현재 표시 중인 자막의 인덱스
    public int endSubtitleIndex;
    public bool SeeScript = false;
    public bool isstop = false;

    void Start()
    {
        SeeScript = false;
        isstop = false;
        // 비디오 플레이어 컴포넌트 설정
        videoPlayer.loopPointReached += OnVideoClipFinished; // 비디오 클립 재생 완료 시 호출될 메소드 등록
        // 초기 비디오 클립 재생
        PlayNextVideoClip();
    }

    void PlayNextVideoClip()
    {
        isstop = false;
        if (currentClipIndex < videoMaps.Length)
        {
            Maparray map = videoMaps[currentClipIndex];
            endSubtitleIndex = videoMaps[currentClipIndex].subtitles.Length - 1;
            if (map.videoClip != null)
            {
                videoPlayer.clip = map.videoClip;
                videoPlayer.Play();

                if (map.subtitles != null && map.subtitles.Length > 0)
                {
                    currentSubtitleIndex = 0;
                    TextMesh.text = map.subtitles[currentSubtitleIndex];
                }
                else
                {
                    TextMesh.text = "";
                    currentSubtitleIndex = 0; // 자막이 없을 경우 0으로 설정
                }
            }
            else
            {
                Debug.LogError("비디오 클립이 없습니다.");
            }
        }
        else
        {
            Debug.Log("모든 비디오 클립 재생 완료");
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            // 모든 비디오 클립 재생이 완료된 경우에 대한 처리를 추가할 수 있습니다.
        }
    }

    void OnVideoClipFinished(VideoPlayer vp)
    {
        // 비디오 클립 재생이 끝나면 다음 비디오 클립 재생
        //if(SeeScript)
        //{
        //    currentClipIndex++;
        //    PlayNextVideoClip();
        //}
        //else
        //{
        //    videoPlayer.Pause();
        //    isstop = true;
        //}
        videoPlayer.Pause();
        isstop = true;  
    }

    void Update()
    {
        // 마우스 클릭 등의 이벤트를 받아서 호출할 메소드를 추가할 수 있습니다.
        // 예를 들면, 아래와 같이 작성할 수 있습니다.
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
        if (currentSubtitleIndex == endSubtitleIndex)
        {
            SeeScript = true;
        }
        else
            SeeScript = false;
    }

    void OnMouseClick()
    {
        // 마우스 클릭이 발생하면 텍스트 업데이트
        Maparray map = videoMaps[currentClipIndex];

        if (currentSubtitleIndex < endSubtitleIndex)
        {
            currentSubtitleIndex++;
            TextMesh.text = map.subtitles[currentSubtitleIndex];

            // 모든 자막을 표시한 경우에만 다음 비디오 클립으로 이동 
            if (currentSubtitleIndex == endSubtitleIndex)
            {
                SeeScript = true;
            }
        }
        if (isstop && SeeScript)
        {
            currentClipIndex++;
            PlayNextVideoClip();
        }
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
