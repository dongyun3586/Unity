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
        // 특정 배경 이미지가 scrollData.Speed만큼 움직이도록 하는 함수 
        backgroundRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
    }
}
