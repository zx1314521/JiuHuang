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
        // ��ȡ������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        // ��������
        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir != Vector3.zero)
        {
            // �ƶ���ɫ
            transform.Translate(dir * speed * Time.deltaTime, Space.World);

            // ��ת��ɫ
            Quaternion toRotation = Quaternion.LookRotation(dir);
            transform.rotation    = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // ���������������ӵ�
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Fire, transform.position, transform.rotation);
        }
    }
}
