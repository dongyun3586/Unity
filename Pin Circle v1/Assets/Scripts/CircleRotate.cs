using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField]    private float rotate_speed = 60f;                   // 회전 속도
    [SerializeField]    private Vector3 rotate_angle = Vector3.forward;     // 회전 방향

    void Update()
    {
        transform.Rotate(rotate_angle * rotate_speed * Time.deltaTime);
    }

    public void Stop()
    {
        rotate_speed = 0;
    }
}