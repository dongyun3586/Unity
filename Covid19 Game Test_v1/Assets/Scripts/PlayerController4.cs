using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 mOffset;
    private float mZCoord;

    private float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 터치가 발생했으면
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);    // 현재 터치 상태를 반환하는 Touch 구조체 반환

            if (touch.phase == TouchPhase.Began)
            {
                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mOffset = gameObject.transform.position - GetTouchWorldPos(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float yPosition = GetTouchWorldPos(touch.position).y;
                rb.MovePosition(new Vector2(rb.position.x, yPosition));
            }
        }
    }
    private Vector3 GetTouchWorldPos(Vector3 touchPosition)
    {
        touchPosition.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }
}
