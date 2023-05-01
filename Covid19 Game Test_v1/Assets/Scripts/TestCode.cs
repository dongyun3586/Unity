using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 3D ���� ��ǥ�� �����ϰ� �ʱ�ȭ
        Vector3 worldPos = new Vector3(0f, 2f, 0f);

        // ī�޶� ������ WorldToScreenPoint �޼��带 ����Ͽ� ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // ��� ���
        Debug.Log("World Position: " + worldPos);
        Debug.Log("Screen Position: " + screenPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
