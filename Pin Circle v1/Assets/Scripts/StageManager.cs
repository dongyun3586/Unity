using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PinSpawner pinSpawner; // Pin ���� ��ü
    [SerializeField] private int throwablePinCount; // �� �������� Pin ����
    [SerializeField] private int stuckPinCount; // Target�� �����Ǿ� �ִ� Pin ����
    [SerializeField] private Camera mainCamera; // ��� ���� ������ ���� Camera ������Ʈ
    [SerializeField] private CircleRotate circleRotate; // Pin�� ��ġ�Ǵ� Target ������Ʈ ȸ�� ��ũ��Ʈ 
    private Color failBackgroundColor = new Color(0.4f, 0.1f, 0.1f); // Game Over �Ǿ��� �� ��� ����
    public bool IsGameOver { set; get; } = false;   // ���� ��� ���� ����

    private Vector3 firstPinPosition = Vector3.down * 2;    // ù ��° Pin ��ġ
    public float PinDistance { private set; get; } = 1;     // pin ������ ��ġ �Ÿ� ������Ƽ

    private void Awake()
    {
        // ȭ�� �ϴܿ� ��ġ�Ǵ� pin ���� ������Ʈ ����
        for (int i = 0; i < throwablePinCount; i++)
        {
            pinSpawner.SpawnThrowblePin(firstPinPosition + Vector3.down * PinDistance * i, throwablePinCount-i);
        }

        // Stage�� ���۵� �� Target Circle�� ��ġ�Ǿ� �ִ� Pin ������Ʈ ����
        for (int i = 0; i < stuckPinCount; i++)
        {
            // ��ġ�Ǵ� Pin�� ������ ���� ������ �������� ��ġ�Ǵ� ����
            float angle = (360 / stuckPinCount) * i;

            pinSpawner.SpawnStuckPin(angle, throwablePinCount+1+i);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;

        // ��� ���� ����
        mainCamera.backgroundColor = failBackgroundColor;

        // Target ������Ʈ ȸ�� ����
        circleRotate.Stop();
    }
}