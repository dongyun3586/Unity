using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private float scrollSpeed = 0.1f;
    Renderer backgroundRenderer;

    private void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Ư�� ��� �̹����� scrollData.Speed��ŭ �����̵��� �ϴ� �Լ� 
        backgroundRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
    }
}
