using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;


public class WebcamHandler : MonoBehaviour
{
    public GameObject objectTarget = null;
    protected WebCamTexture textureWebCam = null;
    private Color[] data;

    void Start()
    {
        // 현재 사용 가능한 카메라의 리스트
        WebCamDevice[] devices = WebCamTexture.devices;

        // 사용할 카메라를 선택
        // 가장 처음 검색되는 후면 카메라 사용
        int selectedCameraIndex = -1;
        for (int i = 0; i < devices.Length; i++)
        {
            // 사용 가능한 카메라 로그
            Debug.Log("Available Webcam: " + devices[i].name + ((devices[i].isFrontFacing) ? "(Front)" : "(Back)"));

            // 후면 카메라인지 체크
            if (devices[i].isFrontFacing == false)
            {
                // 해당 카메라 선택
                selectedCameraIndex = i;
                break;
            }
        }

        selectedCameraIndex = 0;

        // WebCamTexture 생성
        if (selectedCameraIndex >= 0)
        {
            // 선택된 카메라에 대한 새로운 WebCamTexture를 생성
            textureWebCam = new WebCamTexture(devices[selectedCameraIndex].name);

            // 원하는 FPS를 설정
            if (textureWebCam != null)
            { textureWebCam.requestedFPS = 30; }
        }

        // objectTarget으로 카메라가 표시되도록 설정
        if (textureWebCam != null)
        {
            // objectTarget에 포함된 Renderer
            RawImage rawimage = objectTarget.GetComponent<RawImage>();

            // 해당 Renderer의 mainTexture를 WebCamTexture로 설정
            rawimage.texture = textureWebCam;
        }
    }

    void OnDestroy()
    {
        // WebCamTexture 리소스 반환
        if (textureWebCam != null)
        {
            textureWebCam.Stop();
            WebCamTexture.Destroy(textureWebCam);
            textureWebCam = null;
        }
    }

    // Play 버튼이 눌렸을 때
    // Connect 버튼 onClick과 연결
    public void OnPlayButtonClick()
    {
        // 카메라 구동 시작
        if (textureWebCam != null)
        {
            StartCoroutine(WaitForInit());
            textureWebCam.Play();
        }
    }

    // 카메라 width, height 설정 바로 안되는 오류해결 기다림
    IEnumerator WaitForInit()
    {
        while(textureWebCam.width <= 16)
        {
            yield return new WaitForEndOfFrame();
        }
        data = new Color[textureWebCam.width * textureWebCam.height];
        Debug.Log(textureWebCam.width);
        Debug.Log(textureWebCam.height);
    }

    // Stop 버튼이 눌렸을 때
    // Disconnect 버튼 onClick과 연결
    public void OnStopButtonClick()
    {
        // 카메라 구동 정지
        if (textureWebCam != null)
        { textureWebCam.Stop(); }
    }

    // 카메라 데이터 -> Byte 어레이 변환
    public byte[] MakeImgtoByte()
    {
        Texture2D tex = new Texture2D(textureWebCam.width, textureWebCam.height);
        tex.SetPixels(textureWebCam.GetPixels());
        tex.Apply();
        byte[] converted = tex.EncodeToJPG();
        return converted;
    }
}