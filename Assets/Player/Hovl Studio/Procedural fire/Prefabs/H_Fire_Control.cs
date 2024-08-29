using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Fire_Control : MonoBehaviour
{
//----------------------------------------------------------------------
    //物体属性
    float CD_Fire = 0;
    public Vector3 Move_dir;
    //----------------------------------------------------------------------
    //预设体引用
    public GameObject Player;
    public GameObject Bomb;
//----------------------------------------------------------------------
    //物体方法
    void Start()               //预处理角色预制体和移动反方向
    {
        Player = GameObject.FindWithTag("Player");
        Move_dir = Player.transform.forward.normalized;
    }
   
    private void Fire_Move()   //物体的移动
    {
        if (Move_dir.x < 0) Move_dir.x = -1 * Move_dir.x;
        if (Move_dir.y < 0) Move_dir.y = -1 * Move_dir.y;
        if (Move_dir.z < 0) Move_dir.z = -1 * Move_dir.z;
        transform.Translate(Move_dir * Time.deltaTime * 7);
    }
    
    private void Fire_Cd()     //物体的释放间隔与存在时间
    {
        if (CD_Fire > 1)
        {
            GameObject effect = Instantiate(Bomb, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(effect, 2f);
        }
        CD_Fire += Time.deltaTime;
    }

    private void Fire_attack() // 物体对敌人造成伤害
    {
        // 敌人属性
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        // 伤害判断
        foreach(GameObject enemy in enemys)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 1f)
            {
                // 小于1米，炸到敌人，对敌人进行扣除血量 
                GhostScript attack = enemy.GetComponent<GhostScript>();
                bool judge = attack.Damage();

                //如果怪物还活着,那么进行火焰销毁
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
