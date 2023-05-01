using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public TextMeshProUGUI tmp;

    private float y;
    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);    // 현재 터치 상태를 반환하는 Touch 구조체 반환

            tmp.text = $"PlayerWorldPos: {transform.position}\n " +
                $"touch.position: {touch.position}\n" +
                $"GetTouchWorldPos: {Camera.main.ScreenToWorldPoint(touch.position)}\n" +
                $"Screen.width: {Screen.width}\n" +
                $"Screen.height: {Screen.height}";

            if (touch.phase == TouchPhase.Moved)
            {
                y = (touch.position.y > (Screen.height / 2)) ? 1 : -1;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                y = 0.0f;
            }
        }
        rb.velocity = new Vector2(0, y * speed);
    }
}
