using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float speed = 5f; // Player 이동 속도

    private Vector2 lastPosition; // 이전 프레임에서의 터치 위치

    void Update()
    {
        // 터치 입력 받아오기
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치를 시작한 경우
            if (touch.phase == TouchPhase.Began)
            {
                // 이전 프레임에서의 터치 위치 초기화
                lastPosition = touch.position;
            }
            // 터치를 이동한 경우
            else if (touch.phase == TouchPhase.Moved)
            {
                // 드래그한 방향으로 Player 이동
                Vector2 deltaPosition = touch.position - lastPosition;
                transform.Translate(0, deltaPosition.y * speed * Time.deltaTime, 0);

                // 이전 프레임에서의 터치 위치 갱신
                lastPosition = touch.position;
            }
        }
    }
}
