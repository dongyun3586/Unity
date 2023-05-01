using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject square; // pin의 막대 부분
    [SerializeField] private float moveTime = 0.2f; // Pin 이동 시간
    private StageManager stageManager;  // GameOver() 실행을 위한 StageManager 컴포넌트

    public void Setup(StageManager stageManager)
    {
        this.stageManager = stageManager;
    }

    public void SetInPinStuckToTarget()
    {
        // Throwable Pin인 경우 움직이고 있기 때문에 "MoveTo" 코루틴 중지
        StopCoroutine("MoveTo");
        square.SetActive(true); // pin의 square 활성화
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