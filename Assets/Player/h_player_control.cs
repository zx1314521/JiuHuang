using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_player_control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ������
        float horizontal = Input.GetAxis("Horizontal");

        //��ȡˮƽ��
        float vertical = Input.GetAxis("Vertical");

        //��������
        Vector3 dir = new Vector3(horizontal, 0, vertical);

        if(dir != Vector3.zero)
        {
            transform.Translate(dir * Time.deltaTime);
        }
    }
}
