using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    [SerializeField] 
    private Vector3 distance = Vector3.zero;    // ��ǥ ��ġ�κ��� ���� �Ÿ� �������� ��ġ�� �� ����
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;   // UI�� �Ѿƴٴ� ��� ����
        rectTransform = GetComponent<RectTransform>();  // RectTransform ������Ʈ ��������
    }

    // ������Ʈ�� ��ġ�� ���̵� �� UI�� �Բ� ��ġ�ϵ��� LateUpdate()�� �ۼ�
    private void LateUpdate()
    {
        // ȭ�鿡 target�� ������ ������ UI ����
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        // ȭ�鳻���� ��ǥ + distance��ŭ ������ ��ġ�� UI�� ��ġ�� ����
        rectTransform.position = screenPosition + distance;

    }
}
