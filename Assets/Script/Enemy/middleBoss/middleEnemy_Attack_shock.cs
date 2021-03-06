using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middleEnemy_Attack_shock : MonoBehaviour {
    Rigidbody2D playerRigidbody;
    public Transform main;//要跟随英雄
    private middleEnemy_movement Enemymovement;
    public bool state = false;
    float speed = 100f;//衝擊速度
    float shockRange = 10f;
    public float timer = 0;
    public bool State_right = false;
    public bool State_left = false;
    PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && middleEnemy_movement.active)
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
            


        }
    }

    public void normalAttack_hit_right()
    {

        timer += Time.deltaTime;

        if (timer >=0.4f && timer <= 1f)//暫停時間
        {
            Vector2 transformValue = new Vector2(0, 0);
            playerRigidbody.velocity = transformValue;

        }
        else if (timer > 1f)//返回時間
        {
            
            State_right = false;
            timer = 0;

        }

        else

        {
          
            transform.Translate(Vector3.right * speed * Time.deltaTime);


        }



    }


    public void normalAttack_hit_left()
    {

        timer += Time.deltaTime;

       if (timer >= 0.4f && timer <= 1f)//暫停時間
        {
            Vector2 transformValue = new Vector2(0, 0);
            playerRigidbody.velocity = transformValue;

        }
        else if (timer > 1f)//返回時間
        {
          
            State_left = false;
            
            timer = 0;

        }

        else
        {
          
            transform.Translate(Vector3.left * speed * Time.deltaTime);


       }



    }


    private float initialposition;
    private void Start()
    {
        initialposition = transform.position.x;
        playerRigidbody = GetComponent<Rigidbody2D>();
        
        Enemymovement = GetComponent<middleEnemy_movement>(); //與外部判斷是否fire做連結
        playerHealth = null;
    }



}
