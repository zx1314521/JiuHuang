using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Fire_Control : MonoBehaviour
{
//----------------------------------------------------------------------
    //��������
    float CD_Fire = 0;
    public Vector3 Move_dir;
    //----------------------------------------------------------------------
    //Ԥ��������
    public GameObject Player;
    public GameObject Bomb;
//----------------------------------------------------------------------
    //���巽��
    void Start()               //Ԥ�����ɫԤ������ƶ�������
    {
        Player = GameObject.FindWithTag("Player");
        Move_dir = Player.transform.forward.normalized;
    }
   
    private void Fire_Move()   //������ƶ�
    {
        if (Move_dir.x < 0) Move_dir.x = -1 * Move_dir.x;
        if (Move_dir.y < 0) Move_dir.y = -1 * Move_dir.y;
        if (Move_dir.z < 0) Move_dir.z = -1 * Move_dir.z;
        transform.Translate(Move_dir * Time.deltaTime * 7);
    }
    
    private void Fire_Cd()     //������ͷż�������ʱ��
    {
        if (CD_Fire > 1)
        {
            GameObject effect = Instantiate(Bomb, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(effect, 2f);
        }
        CD_Fire += Time.deltaTime;
    }

    private void Fire_attack() // ����Ե�������˺�
    {
        // ��������
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // �˺��ж�
        foreach(GameObject enemy in enemys)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 1f)
            {
                // С��1�ף�ը�����ˣ��Ե��˽��п۳�Ѫ�� 
                GhostScript attack = enemy.GetComponent<GhostScript>();
                bool judge = attack.Damage();

                //������ﻹ����,��ô���л�������
                if(judge == true)
                {
                    GameObject effect = Instantiate(Bomb, transform.position, transform.rotation);
                    Destroy(gameObject);
                    Destroy(effect, 2f);
                }
            }
        }
    }
    //------------------------------------------------------------------------
    void Update()
    {
        Fire_Cd();
        Fire_Move();
        Fire_attack();
    }
}
