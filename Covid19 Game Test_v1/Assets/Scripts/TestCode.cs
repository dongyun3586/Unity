using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 3D 월드 좌표를 선언하고 초기화
        Vector3 worldPos = new Vector3(0f, 2f, 0f);

        // 카메라 메인의 WorldToScreenPoint 메서드를 사용하여 월드 좌표를 스크린 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 결과 출력
        Debug.Log("World Position: " + worldPos);
        Debug.Log("Screen Position: " + screenPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
