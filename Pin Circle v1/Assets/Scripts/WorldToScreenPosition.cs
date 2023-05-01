using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    [SerializeField] 
    private Vector3 distance = Vector3.zero;    // 목표 위치로부터 일정 거리 떨어져서 배치될 때 설정
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;   // UI가 쫓아다닐 대상 설정
        rectTransform = GetComponent<RectTransform>();  // RectTransform 컴포넌트 가져오기
    }

    // 오브젝트의 위치가 갱싱된 후 UI도 함께 위치하도록 LateUpdate()로 작성
    private void LateUpdate()
    {
        // 화면에 target이 보이지 않으면 UI 삭제
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        // 화면내에서 좌표 + distance만큼 떨어진 위치를 UI의 위치로 설정
        rectTransform.position = screenPosition + distance;

    }
}
