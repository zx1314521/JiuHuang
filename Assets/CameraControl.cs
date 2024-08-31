using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // 指向玩家的Transform
    public float smoothing = 5f; // 平滑跟随的速度

    private Vector3 offset;

    void Start()
    {
        // 计算相机与玩家之间的偏移量
        offset = transform.position - player.position;
    }

    void Update()
    {
        // 设置相机的位置为玩家的当前位置加上偏移量
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}

