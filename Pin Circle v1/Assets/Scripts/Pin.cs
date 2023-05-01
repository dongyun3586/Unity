using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject square; // pin�� ���� �κ�
    [SerializeField] private float moveTime = 0.2f; // Pin �̵� �ð�
    private StageManager stageManager;  // GameOver() ������ ���� StageManager ������Ʈ

    public void Setup(StageManager stageManager)
    {
        this.stageManager = stageManager;
    }

    public void SetInPinStuckToTarget()
    {
        // Throwable Pin�� ��� �����̰� �ֱ� ������ "MoveTo" �ڷ�ƾ ����
        StopCoroutine("MoveTo");
        square.SetActive(true); // pin�� square Ȱ��ȭ
    }

    public void MoveOneStep(float moveDistance)
    {
        StartCoroutine("MoveTo", moveDistance);
    }

    private IEnumerator MoveTo(float moveDistance)
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.up * moveDistance;

        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print($"OnTriggerEnter2D: {collision.tag}");
        if (collision.tag.Equals("Pin"))
        {
            //Debug.Log("Game Over");
            stageManager.GameOver();
        }
    }
}