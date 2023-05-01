using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    //public TextMeshProUGUI tmp;

    private Vector3 touchPos, moveDir;
    float previousTouch, currentTouch;

    bool isMoving = false;

    private Vector3 mOffset;
    private float mZCoord;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //float y = Input.GetAxisRaw("Vertical");
        //rb.velocity = new Vector2(0, y * speed);

        //OnSingleTouch();
        ObjectMovement();
    }

    private void ObjectMovement()
    {

    }

    private void OnSingleTouch()
    {
        if (isMoving)
            currentTouch = (touchPos - transform.position).magnitude;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouch = 0;
                currentTouch = 0;
                isMoving = true;
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                moveDir = (touchPos - transform.position).normalized;
                rb.velocity = new Vector2(0f, moveDir.y * speed * Time.deltaTime);
            }
        }

        if (currentTouch > previousTouch)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
            previousTouch = (touchPos - transform.position).magnitude;





        //if (Input.touchCount > 0)
        //{
        //    // �νĵ� ù ��° ��ġ ���� ��������
        //    Touch touch = Input.GetTouch(0);

        //    // ȭ�鿡 ��ġ�� ���۵� ����
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        inputPosition = GetInputPosition(Input.GetTouch(0).position);
        //        tmp.text = $"Touch Begin: {inputPosition}";
        //    }
        //    // ��ġ�� ȭ�鿡�� ������ ���
        //    else if (touch.phase==TouchPhase.Moved)
        //    {
        //        nowPos = touch.position - touch.deltaPosition;
        //        movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * speed;
        //        rb.velocity = new Vector2(0, y * speed);
        //        prePos = touch.position - touch.deltaPosition;
        //        tmp.text = $"Touch Moved\n{touch.position.y}";
        //    }
        //    // ȭ�鿡�� �հ����� �� ���
        //    else if (touch.phase == TouchPhase.Ended)
        //    {
        //        tmp.text = "Touch End";
        //    }
        //}
    }
}