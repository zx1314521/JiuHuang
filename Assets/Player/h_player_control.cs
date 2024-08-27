using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_player_control : MonoBehaviour
{
    public float time = 0;
    public GameObject Fire;
    public GameObject Fire_skill;
    private float speed = 10f; 
    private float rotationSpeed = 700f; 

    void Update()
    {
        //移动
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

        //普攻:按下鼠标左键发射子弹
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Fire, transform.position, transform.rotation);
        }

        //技能1:按下鼠标右键产生战争旋风
        if (Input.GetMouseButtonDown(1))
        {
            float angleStep = 360f / 3; // 计算每朵火源之间的角度
            for (int i = 0; i < 3; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Instantiate(Fire_skill, transform.position + rotation * Vector3.forward * 2f, rotation);
                time = i;
            }
        }

    }
}
