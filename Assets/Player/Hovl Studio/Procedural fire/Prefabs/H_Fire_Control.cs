using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Fire_Control : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bomb;
    public Vector3    Move_dir;
    void Start()
    {
        Player     = GameObject.FindWithTag("Player");
        Move_dir   = Player.transform.forward.normalized;
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

        if (Move_dir.x < 0)  Move_dir.x = -1  * Move_dir.x;
        if (Move_dir.y < 0)  Move_dir.y  = -1 * Move_dir.y;
        if (Move_dir.z < 0)  Move_dir.z = -1  * Move_dir.z;
        transform.Translate(Move_dir * Time.deltaTime * 7);


        //攻击判定
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //伤害判断
        foreach (GameObject enemy in enemys)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                // 小于0.5米，炸到敌人，对敌人进行扣除血量
                GhostScript attack = enemy.GetComponent<GhostScript>();
                attack.Damage();
            }
        }
    }
}
