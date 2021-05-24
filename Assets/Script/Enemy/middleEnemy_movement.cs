﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleEnemy_movement : MonoBehaviour
{
    
    public Animator anim;
    private middleEnemy_Attack_shock shock;
    private middleEnemy_Attack_hit hit;
    
    rightBulletController right;
    leftBulletController left;
    private EnemyAttack_manager bighit;
    Rigidbody2D playerRigidbody;
    public Transform main;//要跟随英雄
    public Transform count;
    public rightBulletController rightBullet;
    public leftBulletController leftBullet;

    EnemyAttack_manager manager;
    public int check = 1;
    bool border_tag = false;//是否碰到限制範圍
    public bool bighit_state = false;
    float followDis = 30f;//達到此距離開始跟隨
    float attackDis = 22f;
    public float idle_speed = 20f;//移動速度
    public float follow_speed = 15f;//跟隨速度
    private middleEnemy_Health enemyHealth;

    float attackidle_speed = 5f;//移動速度
    public float timer = 0;
    public float timer_bighit = 0;
    float timer_dead = 0;
    bool detectCharac;
    public static bool active = true;
    float attackidle_timer=0;

    Animator playerAnim;
    PlayerHealth playerHealth;
    public Enemy_count countAllEnemy1;

    float timer_fire = 0f;
    public static bool faceright;
    private void Start()
    {
        faceright = false;
    }
    void detectCharacFunc()//判斷是否位於同一平台
    {
        if ((main.position.y - transform.position.y) <= 3f && (main.position.y - transform.position.y) >= -10f)
        {

            detectCharac = true;
        }
        else
        {
            detectCharac = false;

        }

    }
    
    void detectScale()
    {
        if (main.position.x - transform.position.x > 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            faceright = false;
        }

        else
        {

            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            faceright = true;


        }

    }

    void detectIdle()
    {
        if (idle_speed > 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            faceright = true;
        }
        else
        {

            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            faceright = false;
        }


    }
    //跟隨函式
    void follow()
    {
        detectScale();
        //當於怪物的右側
        if (Vector2.Distance(transform.position, main.position) <= followDis && (main.position.x - transform.position.x > 0))//跟随距离
        {

            Vector2 transformValue = new Vector2(10, 0);
            playerRigidbody.velocity = transformValue;

        }

        //當於怪物的左側

        if (Vector2.Distance(transform.position, main.position) <= followDis && (main.position.x - transform.position.x < 0))//跟随距离
        {

            Vector2 transformValue = new Vector2(-10, 0);
            playerRigidbody.velocity = transformValue;


        }
    }


    //怪物idle狀態
    int i = 0;
    void idle()
    {
        detectIdle();
        timer += Time.deltaTime;
        Vector2 transformValue = new Vector2(idle_speed, 0);
        i++;
        playerRigidbody.velocity = transformValue;
        if (i == 100)
        {
            Vector2 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            idle_speed = idle_speed * -1;
            i = 0;
        }

        /*
        Vector2 transformValue = new Vector2(idle_speed, 0);

        playerRigidbody.velocity = transformValue;
        if (border_tag == true)
        {

            idle_speed = idle_speed * -1;

            border_tag = false;
        }


    */

    }


    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        playerRigidbody = GetComponent<Rigidbody2D>();
        main = GameObject.FindGameObjectWithTag("Player").transform;
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealth = GetComponent<middleEnemy_Health>();
        shock = GetComponent<middleEnemy_Attack_shock>();
        hit = GetComponent<middleEnemy_Attack_hit>();
       
        bighit = GetComponent<EnemyAttack_manager>();
        anim = GetComponent<Animator>();
        count = GameObject.FindGameObjectWithTag("count").transform;
        countAllEnemy1 = count.GetComponent<Enemy_count>();
        manager = GetComponent<EnemyAttack_manager>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        rightBullet = GameObject.FindGameObjectWithTag("rightbullet").GetComponent<rightBulletController>();
        leftBullet = GameObject.FindGameObjectWithTag("leftbullet").GetComponent<leftBulletController>();
    }


    float initialx;
    bool stat_dead = false;
    int attack_sytle=5;
    float attack_timer = 0;
    float hurt_timer = 0;
    public bool gethurt = false;



    int triggercount = 0;
    void OnTriggerEnter2D(Collider2D border)
    {

        if (border.gameObject.CompareTag("bullet"))
        {
            //anim.SetBool("middleEnemy_hurt", true);
            gethurt = true;
           
            hurt_timer += Time.deltaTime;
            Invoke("shotdowm", 0.5f);

        }

        if (border.gameObject.CompareTag("Player")&& active==true)
        {
            triggercount++;
            playerHealth = border.GetComponent<PlayerHealth>();

            if (triggercount == 1)
            {
              
                playerHealth.TakeDamage(1);
            }
            if (triggercount == 3)
                triggercount = 0;
        }
    }

    void shotdowm()
    {
        //anim.SetBool("middleEnemy_hurt", false);
      
        CancelInvoke();

    }
    void resetanim()
    {
        countAllEnemy1.middleEnemy_count--;
        CancelInvoke("resetanim");
    }


    public void animatestate(int state)
    {


        switch (state)
        {

           

            case 1:  //shock anim
                manager.bighit_state_left = false;
                manager.bighit_state_right = false;
                anim.SetBool("middle_enemy_shock_start", true);
                anim.SetBool("middle_enemy_normalhit_start", false);
                anim.SetBool("middle_enemy_bighit_start", false);
                anim.SetBool("middleEnemy_idle", false);
                anim.SetBool("middleEnemy_stay_idle", false);
                break;
            case 2:  //bithit anim
                anim.SetBool("middle_enemy_shock_start", false);
                anim.SetBool("middle_enemy_normalhit_start", false);
                anim.SetBool("middle_enemy_bighit_start", true);
                anim.SetBool("middleEnemy_idle", false);
                anim.SetBool("middleEnemy_stay_idle", false);
                break;
            case 3:  //normalhit anim
                manager.bighit_state_left = false;
                manager.bighit_state_right = false;
                anim.SetBool("middle_enemy_shock_start", false);
                anim.SetBool("middle_enemy_normalhit_start", true);
                anim.SetBool("middle_enemy_bighit_start", false);
                anim.SetBool("middleEnemy_idle", false);
                anim.SetBool("middleEnemy_stay_idle", false);
                break;
            case 4:  //idle anim
                manager.bighit_state_left = false;
                manager.bighit_state_right = false;
                anim.SetBool("middle_enemy_shock_start", false);
                anim.SetBool("middle_enemy_normalhit_start", false);
                anim.SetBool("middle_enemy_bighit_start", false);
                anim.SetBool("middleEnemy_idle", true);
                anim.SetBool("middleEnemy_stay_idle", false);
                break;

            case 5:  //stay_idle anim
                manager.bighit_state_left = false;
                manager.bighit_state_right = false;
                anim.SetBool("middle_enemy_shock_start", false);
                anim.SetBool("middle_enemy_normalhit_start", false);
                anim.SetBool("middle_enemy_bighit_start", false);
                anim.SetBool("middleEnemy_idle", false);
                anim.SetBool("middleEnemy_stay_idle", true);
                break;

        }


    }

    void Update()
    {
        detectCharacFunc();
        if (enemyHealth.isDead)
        {
            enemyHealth.isDead = false;

            active = false;
            anim.SetBool("middle_enemy_shock_start", false);
            anim.SetBool("middle_enemy_normalhit_start", false);
            anim.SetBool("middle_enemy_bighit_start", false);
            anim.SetBool("middleEnemy_idle", false);
            anim.SetBool("middleEnemy_stay_idle", false);
            anim.SetBool("middleEnemy_hurt",false);
            anim.SetTrigger("middleEnemy_Dead");
            
            /*timer_dead += Time.deltaTime;*/
            Vector2 transformValue = new Vector2(0, 0);
            //countAllEnemy1.middleEnemy_count--;
            playerRigidbody.velocity = transformValue;
            followDis = 0;
            attackDis = 0;
            Invoke("resetanim",1.5f);
            

        }

        


        else if(active)
        {

            if ((Vector2.Distance(transform.position, main.position) <= followDis) && detectCharac == true)
            {
                detectScale();
                if ((Vector2.Distance(transform.position, main.position) <= attackDis) )
                {
                    detectScale();
                   
                    Vector2 transformValue = new Vector2(0, 0);
                    playerRigidbody.velocity = transformValue;
                    attack_timer += Time.deltaTime;
                    if ((int)attack_timer> 4 && shock.State_left == false && shock.State_right == false && /*hit.normalhit_state == false &&*/ bighit_state == false)
                    {
                        attack_sytle ++;

                       
                        if (attack_sytle==7)
                        {
                            attack_sytle = 1;

                        }
                        attack_timer = 0;
                       
                    }
                    

                    //自動轉換攻擊
                    switch (attack_sytle)
                    {
                        case 1: //普攻

                            if (shock.State_left == false && shock.State_right == false && bighit_state == false)
                            {

                                if (Vector2.Distance(transform.position, main.position) <= attackDis/* && (main.position.x - transform.position.x > 0)*/)
                                {

                                    animatestate(3);
                                    hit.middleBoss_hit();



                                }

                                break;

                            }


                            break;
                       /* case 2://休息
                            animatestate(5);


                            transformValue = new Vector2(0, 0);
                          

                            break;
                            */

                        case 2: //前腳踏地衝擊波
                            timer_fire += Time.deltaTime;
                            if (shock.State_left == false && shock.State_right == false)
                            {
                                if ((main.position.x - transform.position.x > 0))
                                {
                                    animatestate(2);

                                    if (timer_fire > 2f)
                                    {
                                        rightBullet.fire();
                                        timer_fire = 0;
                                    }





                                }
                                else if ((main.position.x - transform.position.x < 0))//跟随距离
                                {
                                    animatestate(2);

                                    if (timer_fire > 2f)
                                    {
                                        leftBullet.fire();

                                        timer_fire = 0;
                                    }


                                }
                                break;
                            }


                            break;
                        case 3://休息
                            animatestate(5);

                            transformValue = new Vector2(0, 0);
                          

                            break;

                        case 4: //普攻

                            if (shock.State_left == false && shock.State_right == false && bighit_state == false)
                            {

                                if (Vector2.Distance(transform.position, main.position) <= attackDis/* && (main.position.x - transform.position.x > 0)*/)
                                {

                                    animatestate(3);
                                    hit.middleBoss_hit();



                                }

                                break;

                            }


                            break;
                        /*case 6: //休息
                            animatestate(5);

                            transformValue = new Vector2(0, 0);
                           

                            break;*/

                        case 5://衝擊

                            if (((main.position.x > transform.position.x) || shock.State_right == true) && shock.State_left == false/* && hit.normalhit_state == false*/ && bighit_state == false)
                            {
                                animatestate(1);
                                shock.State_right = true;
                                timer += Time.deltaTime;
                                shock.normalAttack_hit_right();





                            }


                            else if (((main.position.x < transform.position.x) || shock.State_left == true) && shock.State_right == false /*&& hit.normalhit_state == false*/ && bighit_state == false)
                            {
                                animatestate(1);
                                shock.State_left = true;
                                timer += Time.deltaTime;
                                shock.normalAttack_hit_left();





                            }



                            break;

                        case 6: //休息
                            animatestate(5);

                            transformValue = new Vector2(0, 0);
                          


                            break;


                       
                       

                        
                      



                    }




                }

                else if ((Vector2.Distance(transform.position, main.position) > attackDis))
                {

                    follow();
                    animatestate(4);
                }
            }


            else if ((Vector2.Distance(transform.position, main.position) > attackDis))
            {
                
               
                idle();
                animatestate(4);

            }





        }

    }
}