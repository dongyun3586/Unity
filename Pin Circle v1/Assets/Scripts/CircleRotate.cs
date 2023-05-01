using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField]    private float rotate_speed = 60f;                   // ȸ�� �ӵ�
    [SerializeField]    private Vector3 rotate_angle = Vector3.forward;     // ȸ�� ����

    void Update()
    {
        transform.Rotate(rotate_angle * rotate_speed * Time.deltaTime);
    }

    public void Stop()
    {
        rotate_speed = 0;
    }
}