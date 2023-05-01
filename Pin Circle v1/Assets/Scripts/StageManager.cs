using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner; // Pin 생성 객체
    [SerializeField] private int throwablePinCount; // 현 스테이지 Pin 개수
    [SerializeField] private int stuckPinCount; // Target에 고정되어 있는 Pin 개수
    [SerializeField] private Camera mainCamera; // 배경 색상 변경을 위한 Camera 컴포넌트
    [SerializeField] private CircleRotate circleRotate; // Pin이 배치되는 Target 오브젝트 회전 스크립트 
    private Color failBackgroundColor = new Color(0.4f, 0.1f, 0.1f); // Game Over 되었을 때 배경 색상
    public bool IsGameOver { set; get; } = false;   // 게임 제어를 위한 변수

    private Vector3 firstPinPosition = Vector3.down * 2;    // 첫 번째 Pin 위치
    public float PinDistance { private set; get; } = 1;     // pin 사이의 배치 거리 프로퍼티

    private void Awake()
    {
        // 화면 하단에 배치되는 pin 게임 오브젝트 생성
        for (int i = 0; i < throwablePinCount; i++)
        {
            pinSpawner.SpawnThrowblePin(firstPinPosition + Vector3.down * PinDistance * i, throwablePinCount-i);
        }

        // Stage가 시작될 때 Target Circle에 배치되어 있는 Pin 오브젝트 생성
        for (int i = 0; i < stuckPinCount; i++)
        {
            // 배치되는 Pin의 개수에 따라 일정한 간격으로 배치되는 각도
            float angle = (360 / stuckPinCount) * i;

            pinSpawner.SpawnStuckPin(angle, throwablePinCount+1+i);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;

        // 배경 색상 변경
        mainCamera.backgroundColor = failBackgroundColor;

        // Target 오브젝트 회전 중지
        circleRotate.Stop();
    }
}