using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class H_Fire_Control2 : MonoBehaviour
{
    GameObject Player;
    float CD = 0;
    float time = 0;
    public float speed = 5f;
    public float rotationSpeed = 700f;
    Vector3 box;
    // Start is called before the first frame updatse
    void Start()
    {
        //��ȡ��ɫ�������е�time����
       
        Player = GameObject.FindWithTag("fuck");
        time = Player.GetComponent<H_player_control>().time;
        box = Player.transform.position;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (CD > 3)
        {
            Destroy(gameObject);
        }

        // ��ȡ������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(-1 * horizontal, 0, -1 * vertical).normalized;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        Quaternion toRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        if (time == 2)
        {
            transform.RotateAround(Player.transform.position, Vector3.up, 0.8f);
        }
        CD += Time.deltaTime;

        // ��������
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemys.Length;
        bool[] enemyHit = new bool[enemyCount];

        // �˺��ж�
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = enemys[i];
            if (Vector3.Distance(transform.position, enemy.transform.position) < 1f)
            {
                // С��1�ף�ը�����ˣ��Ե��˽��п۳�Ѫ��
                GhostScript attack = enemy.GetComponent<GhostScript>();
                attack.Damage();
                enemyHit[i] = true; // ��ǵ����ѱ�����
            }
            else
            {
                enemyHit[i] = false; // ��ǵ���δ������
            }
        }
    }
}
