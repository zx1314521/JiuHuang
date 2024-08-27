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
        //获取角色控制类中的time变量
       
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

        // 获取虚拟轴
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        Quaternion toRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        if (time == 2)
        {
            transform.RotateAround(Player.transform.position, Vector3.up, 0.8f);
        }
        CD += Time.deltaTime;        
    }
}
