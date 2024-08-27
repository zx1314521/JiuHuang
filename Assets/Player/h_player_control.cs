using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_player_control : MonoBehaviour
{
    public GameObject Fire;
    public float speed = 5f; // Movement speed
    public float rotationSpeed = 700f; // Rotation speed

    void Update()
    {
        // 获取虚拟轴
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        // 构建向量
        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir != Vector3.zero)
        {
            // 移动角色
            transform.Translate(dir * speed * Time.deltaTime, Space.World);

            // 旋转角色
            Quaternion toRotation = Quaternion.LookRotation(dir);
            transform.rotation    = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // 按下鼠标左键发射子弹
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Fire, transform.position, transform.rotation);
        }
    }
}
