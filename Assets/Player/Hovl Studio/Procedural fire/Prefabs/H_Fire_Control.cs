using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Fire_Control : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bomb;
    public Vector3 Move_dir;
    void Start()
    {
        Player     = GameObject.FindWithTag("Player");
        Move_dir   = Player.transform.forward; 
    }

    float CD_Fire = 0;
    void Update()
    {
        if(CD_Fire > 1)
        {
            GameObject effect = Instantiate(Bomb,transform.position,transform.rotation);
            
            Destroy(gameObject);
            Destroy(effect, 2f);
        }
        CD_Fire += Time.deltaTime;
        transform.Translate(Move_dir * Time.deltaTime * 7);
    }
}
