using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GhostScript : MonoBehaviour
{
    private Animator Anim;
    private CharacterController Ctrl;
    private Vector3 MoveDirection = Vector3.zero;
    private GameObject Player;

    // Cache hash values
    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int AttackTag = Animator.StringToHash("Attack");
    // dissolve
    [SerializeField] private SkinnedMeshRenderer[] MeshR;
    private float Dissolve_value = 1;
    private bool DissolveFlg = false;
    private const int maxHP = 3;
    private GameObject Fire;

    public int HP = maxHP;
    public GameObject Enemy_Fire; 
    // moving speed
    [SerializeField] private float Speed = 4;

    void Start()
    {
        Player = GameObject.FindWithTag("fuck");
        Anim = this.GetComponent<Animator>();
        Ctrl = this.GetComponent<CharacterController>();
    }

    float time = 0;
    float time_fire = 0;
    float nowtime = 0;
    bool Attacknow = false;
    float Atdist = 5;
    void Update(){
        STATUS(); // 如果字典是true就能做出动作
        GRAVITY(); // 
        //Respawn();
        Debug.Log(HP);
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        if(DissolveFlg && time - nowtime >= 2)
        {
            Destroy(gameObject);
        }
        else if (HP <= 0 && !DissolveFlg)
        {
            Anim.CrossFade(DissolveState, 0.1f, 0, 0);
            DissolveFlg = true;
            nowtime = time;
        }
        else if (dist < 100f && dist > Atdist && HP > 0)
        {
            // move att
            transform.LookAt(Player.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        else if (dist <= Atdist && time >= 2 && HP > 0 && Attacknow == false)
        {
            transform.LookAt(Player.transform);
            PlayerAttack();
            time = 0;
        }
        time += Time.deltaTime;

        if(Attacknow == true)
        {
            Fire.transform.Translate(Vector3.forward * 4 * Time.deltaTime);
            
            float dist_Fire = Vector3.Distance(Player.transform.position, Fire.transform.position);
            Debug.Log(dist_Fire);
            if(dist_Fire <= 0.5f)
            {
                H_player_control attack = Player.GetComponent<H_player_control>();
                attack.Damage();
                Attacknow = false;
                Destroy(Fire);
                time_fire = 0;
            }
            time_fire += Time.deltaTime;
            if(time_fire >= 1)
            {
                Attacknow = false;
                Destroy(Fire);
                time_fire = 0;
            }
        }

        // this character status
        //if(!PlayerStatus.ContainsValue( true ))
        //{
        //    MOVE();
        //    PlayerAttack();
        //    Damage();
        //}
        //else if(PlayerStatus.ContainsValue( true ))
        //{
        //    int status_name = 0;
        //    foreach(var i in PlayerStatus)
        //    {
        //        if(i.Value == true)
        //        {
        //            status_name = i.Key;
        //            break;
        //        }
        //    }
        //    if(status_name == Dissolve)
        //    {
        //        PlayerDissolve();
        //    }
        //    else if(status_name == Attack)
        //    {
        //        PlayerAttack();
        //    }
        //    else if(status_name == Surprised)
        //    {
        //        // nothing method
        //    }
        //}
        //// Dissolve
        //if(HP <= 0 && !DissolveFlg)
        //{
        //    Anim.CrossFade(DissolveState, 0.1f, 0, 0);
        //    DissolveFlg = true;
        //}
        //// processing at respawn
        //else if(HP == maxHP && DissolveFlg)
        //{
        //    DissolveFlg = false;
        //}
    }

    //---------------------------------------------------------------------
    // character status
    //---------------------------------------------------------------------
    private const int Dissolve = 1;
    private const int Attack = 2;
    private const int Surprised = 3;
    private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
    {
        {Dissolve, false },
        {Attack, false },
        {Surprised, false },
    };
    //------------------------------
    private void STATUS () // 设置动作
    {
        // during dissolve
        if(DissolveFlg && HP <= 0)
        {
            PlayerStatus[Dissolve] = true;
        }
        else if(!DissolveFlg)
        {
            PlayerStatus[Dissolve] = false;
        }
        // during attacking
        if(Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
        {
            PlayerStatus[Attack] = true;
        }
        else if(Anim.GetCurrentAnimatorStateInfo(0).tagHash != AttackTag)
        {
            PlayerStatus[Attack] = false;
        }
        // during damaging
        if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
        {
            PlayerStatus[Surprised] = true;
        }
        else if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash != SurprisedState)
        {
            PlayerStatus[Surprised] = false;
        }
    }
    // dissolve shading
    private void PlayerDissolve ()
    {
        Dissolve_value -= Time.deltaTime;
        for(int i = 0; i < MeshR.Length; i++)
        {
            MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
        }
        if(Dissolve_value <= 0)
        {
            Ctrl.enabled = false;
        }
    }
    // play a animation of Attack
    private void PlayerAttack ()
    {
        Fire = Instantiate(Enemy_Fire, transform.position, transform.rotation);
        Attacknow = true;
        Anim.CrossFade(AttackState,0.1f,0,0);
    }
    //---------------------------------------------------------------------
    // gravity for fall of this character
    //---------------------------------------------------------------------
    private void GRAVITY ()
    {
        if(Ctrl.enabled)
        {
            if(CheckGrounded())
            {
                if(MoveDirection.y < -0.1f)
                {
                    MoveDirection.y = -0.1f;
                }
            }
            MoveDirection.y -= 0.1f;
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
    }
    //---------------------------------------------------------------------
    // whether it is grounded
    //---------------------------------------------------------------------
    private bool CheckGrounded()
    {
        if (Ctrl.isGrounded && Ctrl.enabled)
        {
            return true;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float range = 0.2f;
        return Physics.Raycast(ray, range);
    }
    //---------------------------------------------------------------------
    // for slime moving
    //---------------------------------------------------------------------
    private void MOVE ()
    {
        // velocity
        if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
        {
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, 270, 0));
            }
        }
        KEY_DOWN();
        KEY_UP();
    }
    //---------------------------------------------------------------------
    // value for moving
    //---------------------------------------------------------------------
    private void MOVE_Velocity (Vector3 velocity, Vector3 rot)
    {
        MoveDirection = new Vector3 (velocity.x, MoveDirection.y, velocity.z);
        if(Ctrl.enabled)
        {
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
        MoveDirection.x = 0;
        MoveDirection.z = 0;
        this.transform.rotation = Quaternion.Euler(rot);
    }
    //---------------------------------------------------------------------
    // whether arrow key is key down
    //---------------------------------------------------------------------
    private void KEY_DOWN ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
    }
    //---------------------------------------------------------------------
    // whether arrow key is key up
    //---------------------------------------------------------------------
    private void KEY_UP ()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if(!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }
    //---------------------------------------------------------------------
    // damage
    //---------------------------------------------------------------------
    public bool Damage ()
    {
        //Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
        if(HP <= 0) { return false; }
        HP --;
        return true;
    }
    //---------------------------------------------------------------------
    // respawn
    //---------------------------------------------------------------------
    private void Respawn ()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // 重置鬼
        {
            // player HP
            HP = maxHP;
            
            Ctrl.enabled = false;
            this.transform.position = Vector3.zero; // player position
            this.transform.rotation = Quaternion.Euler(Vector3.zero); // player facing
            Ctrl.enabled = true;
            
            // reset Dissolve
            Dissolve_value = 1;
            for(int i = 0; i < MeshR.Length; i++)
            {
                MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
            }
            // reset animation
            Anim.CrossFade(IdleState, 0.1f, 0, 0);
        }
    }
}
