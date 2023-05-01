using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public float speed = 5f; // Player �̵� �ӵ�

    private Vector2 lastPosition; // ���� �����ӿ����� ��ġ ��ġ

    void Update()
    {
        // ��ġ �Է� �޾ƿ���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ������ ���
            if (touch.phase == TouchPhase.Began)
            {
                // ���� �����ӿ����� ��ġ ��ġ �ʱ�ȭ
                lastPosition = touch.position;
            }
            // ��ġ�� �̵��� ���
            else if (touch.phase == TouchPhase.Moved)
            {
                // �巡���� �������� Player �̵�
                Vector2 deltaPosition = touch.position - lastPosition;
                transform.Translate(0, deltaPosition.y * speed * Time.deltaTime, 0);

                // ���� �����ӿ����� ��ġ ��ġ ����
                lastPosition = touch.position;
            }
        }
    }
}
