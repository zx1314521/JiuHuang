using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_player_control : MonoBehaviour
{
    //������ֵ����
    public  float HP = 5;
    private float speed = 10f;
    private float rotationSpeed = 700f;
//----------------------------------------------------------------------------------
    //����ٿ�����
    public float      time = 0;
    public GameObject Fire;
    public GameObject Fire_skill;
//-----------------------------------------------------------------------------------    
    //���ﶯ������
    private void Player_Move()   //�����ƶ�
    {
        // ��ȡ������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ��������
        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        if (dir != Vector3.zero)
        {
            // �ƶ���ɫ
            transform.Translate(dir * speed * Time.deltaTime, Space.World);

            // ��ת��ɫ
            Quaternion toRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void Player_attack() //��ͨ����
    {
        //�չ�:���������������ӵ�
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Fire, transform.position, transform.rotation);
        }
    }

    private void Player_skill_1()//�ͷ�һ����
    {
        //����1:��������Ҽ�����ս������
        if (Input.GetMouseButtonDown(1))
        {
            float angleStep = 360f / 3; // ����ÿ���Դ֮��ĽǶ�
            for (int i = 0; i < 3; i++)
            {
                float angle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Instantiate(Fire_skill, transform.position + rotation * Vector3.forward * 2f, rotation);
                time = i;
            }
        }
    }

    public void Damage()        //�ܵ��˺��ж�����
    {
        HP--;
    }

//-------------------------------------------------------------------------------------
    void Update()
    {
        Damage();
        Player_Move();
        Player_attack();
        Player_skill_1();
    }
}
