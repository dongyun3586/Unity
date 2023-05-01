using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject pinPrefab; // pin prefab
    [SerializeField] private GameObject textPinIndexPrefab; // Pin�� ���ڸ� ǥ���ϴ� Text UI
    [SerializeField] private Transform textParent;  // Pin�� ���� Text�� ��ġ�Ǵ� Panel Transform

    [Header("Stuck Pin")]
    [SerializeField] private Transform targetTransform; // Target Circle�� Transform
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2; // Target Circle�� ��ġ
    [SerializeField] private float targetRadius = 0.8f; // Target Circle�� ������
    [SerializeField] private float pinLength = 1.5f;// Pin�� ���� ����

    [Header("Throwable Pin")]
    [SerializeField] private float bottomAngle = 270;   // ���콺 Ŭ������ ��ġ�Ǵ� Pin�� ����
    private List<Pin> throwablePins = new List<Pin>();  // ȭ�� �ϴ��� �������ϴ� Pin ������Ʈ ����Ʈ

    private void Update()
    {
        // Game Over(Ŭ���� or ����)�̸� ���� X
        if (stageManager.IsGameOver == true) return;

        // �÷��̾ ���콺 ���� ��ư�� Ŭ���ϸ� Pin ����
        if(Input.GetMouseButtonDown(0) && throwablePins.Count > 0)
        {
            // throwablePins ����Ʈ�� ����� ù ��° Pin�� Target�� ��ġ
            SetInPinStuckToTarget(throwablePins[0].transform, bottomAngle);
            // Target�� ��ġ�� ù ���� Pin�� throwablePins ����Ʈ���� ����
            throwablePins.RemoveAt(0);

            // �����ִ� Pin���� ��ġ�� ���� �̵�
            for(int i=0; i < throwablePins.Count; i++)
            {
                throwablePins[i].MoveOneStep(stageManager.PinDistance);
            }
        }
    }

    // �߻��� ����
    public void SpawnThrowblePin(Vector3 position, int index)
    {
        // Pin ������Ʈ ��ü ����
        GameObject pin_obj = Instantiate(pinPrefab, position, Quaternion.identity);    // pin object ����

        // Pin ������Ʈ ������ ���� Setup() �޼ҵ� ȣ��
        Pin pin = pin_obj.GetComponent<Pin>();
        pin.Setup(stageManager);

        // ������ Pin ������Ʈ�� throwablePins ����Ʈ�� �߰�
        throwablePins.Add(pin);

        // Pin ������Ʈ�� ǥ�õǴ� Text UI ����
        SpawnTextUI(pin_obj.transform, index);
    }

    // ������ ����
    public void SpawnStuckPin(float angle, int index)
    {
        // pin ���ӿ�����Ʈ ����
        GameObject clone = Instantiate(pinPrefab);

        // "Pin" ������Ʈ ������ ���� Setup() �޼ҵ� ȣ��
        Pin pin = clone.GetComponent<Pin>();
        pin.Setup(stageManager);

        // Pin�� Target�� ��ġ
        SetInPinStuckToTarget(clone.transform, angle);

        // Pin ������Ʈ�� ǥ�õǴ� Text UI ����
        SpawnTextUI(clone.transform, index);
    }

    private void SpawnTextUI(Transform target, int index)
    {
        // ���ڸ� ��Ÿ���� Text UI ����
        GameObject textClone = Instantiate(textPinIndexPrefab);

        // Text UI�� �θ� textParent�� ����
        textClone.transform.SetParent(textParent);

        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        textClone.transform.localScale = Vector3.one;

        // UI�� �Ѿƴٴ� ��� ����
        textClone.GetComponent<WorldToScreenPosition>().Setup(target);

        // UI�� ǥ�õǴ� �ؽ�Ʈ ����
        textClone.GetComponent<TMPro.TextMeshProUGUI>().text = index.ToString();
    }

    // ���� Target�� ��ġ
    private void SetInPinStuckToTarget(Transform pin, float angle)
    {
        // Target�� Pin�� ������ ���� ��ġ
        pin.position = GetPositionFromAngle(targetRadius + pinLength, angle) + targetPosition;
        // Pin ���ӿ�����Ʈ ȸ�� ����
        pin.rotation = Quaternion.Euler(0, 0, angle);
        // Pin ������Ʈ�� Target�� �ڽ����� ����
        pin.SetParent(targetTransform);
        // Pin�� square ���̵��� ����
        pin.GetComponent<Pin>().SetInPinStuckToTarget();
    }

    // Target�� ���� Pin�� ��ġ ��ȯ
    private Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = Mathf.PI * angle / 180;

        position.x = Mathf.Cos(angle) * radius;
        position.y = Mathf.Sin(angle) * radius;

        return position;
    }
}