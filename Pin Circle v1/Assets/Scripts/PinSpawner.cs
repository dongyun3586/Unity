using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject pinPrefab; // pin prefab
    [SerializeField] private GameObject textPinIndexPrefab; // Pin에 숫자를 표시하는 Text UI
    [SerializeField] private Transform textParent;  // Pin의 숫자 Text가 배치되는 Panel Transform

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform; // Target Circle의 Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2; // Target Circle의 위치
    [SerializeField] private float targetRadius = 0.8f; // Target Circle의 반지름
    [SerializeField] private float pinLength = 1.5f;// Pin의 막대 길이

    [Header("Throwable Pin")]
    [SerializeField] private float bottomAngle = 270;   // 마우스 클릭으로 배치되는 Pin의 각도
    private List<Pin> throwablePins = new List<Pin>();  // 화면 하단의 던져야하는 Pin 오브젝트 리스트

    private void Update()
    {
        // Game Over(클리어 or 실패)이면 실행 X
        if (stageManager.IsGameOver == true) return;

        // 플레이어가 마우스 왼쪽 버튼을 클릭하면 Pin 생성
        if(Input.GetMouseButtonDown(0) && throwablePins.Count > 0)
        {
            // throwablePins 리스트에 저장된 첫 번째 Pin을 Target에 배치
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            // Target에 배치된 첫 번재 Pin을 throwablePins 리스트에서 제거
            throwablePins.RemoveAt(0);

            // 남아있는 Pin들의 위치를 위로 이동
            for(int i=0; i < throwablePins.Count; i++)
            {
                throwablePins[i].MoveOneStep(stageManager.PinDistance);
            }
        }
    }

    // 발사핀 생성
    public void SpawnThrowblePin(Vector3 position, int index)
    {
        // Pin 오브젝트 객체 생성
        GameObject pin_obj = Instantiate(pinPrefab, position, Quaternion.identity);    // pin object 생성

        // Pin 컴포넌트 정보를 얻어와 Setup() 메소드 호출
        Pin pin = pin_obj.GetComponent<Pin>();
        pin.Setup(stageManager);

        // 생성된 Pin 오브젝트를 throwablePins 리스트에 추가
        throwablePins.Add(pin);

        // Pin 오브젝트에 표시되는 Text UI 생성
        SpawnTextUI(pin_obj.transform, index);
    }

    // 고정핀 생성
    public void SpawnStuckPin(float angle, int index)
    {
        // pin 게임오브젝트 생성
        GameObject clone = Instantiate(pinPrefab);

        // "Pin" 컴포넌트 정보를 얻어와 Setup() 메소드 호출
        Pin pin = clone.GetComponent<Pin>();
        pin.Setup(stageManager);

        // Pin을 Target에 배치
        SetInPinStuckToTarget(clone.transform, angle);

        // Pin 오브젝트에 표시되는 Text UI 생성
        SpawnTextUI(clone.transform, index);
    }

    private void SpawnTextUI(Transform target, int index)
    {
        // 숫자를 나타내는 Text UI 생성
        GameObject textClone = Instantiate(textPinIndexPrefab);

        // Text UI의 부모를 textParent로 설정
        textClone.transform.SetParent(textParent);

        // 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
        textClone.transform.localScale = Vector3.one;

        // UI가 쫓아다닐 대상 설정
        textClone.GetComponent<WorldToScreenPosition>().Setup(target);

        // UI에 표시되는 텍스트 내용
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }

    // 핀을 Target에 배치
    private void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // Target에 Pin이 꽂혔을 때의 위치
        pin.position = GetPositionFromAngle(targetRadius + pinLength, angle) + targetPosition;
        // Pin 게임오브젝트 회전 설정
        pin.rotation = Quaternion.Euler(0, 0, angle);
        // Pin 오브젝트를 Target의 자식으로 설정
        pin.SetParent(targetTransform);
        // Pin의 square 보이도록 설정
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }

    // Target에 꼳힐 Pin의 위치 반환
    private Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = Mathf.PI * angle / 180;

        position.x = Mathf.Cos(angle) * radius;
        position.y = Mathf.Sin(angle) * radius;

        return position;
    }
}