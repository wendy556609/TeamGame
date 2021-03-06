using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welf2 : SkillSet {
    //public static bool isSkill = false;
    public float moveSpeed = 2f;
    private float clearTime = 1f;
    private float timer = 0;

    //主角相關
    GameObject player;
    Animator playAnim;
    PlayerMovement playerMovement;
    Rigidbody2D playerRigidbody;
    Transform playerTrans;
    public GameObject welfuiobject;
    private SkillCoolDown skillcooldown;

    //水精靈相關
    private bool isFast = false;
    Animator welfAnim;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        playAnim = player.GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTrans = player.transform;

        welfAnim = GetComponent<Animator>();

        welfuiobject = GameObject.FindWithTag("WelfUI");
        skillcooldown = welfuiobject.GetComponent<SkillCoolDown>();
    }
	
	// Update is called once per frame
	void Update () {
        GetKey();

        if (keySkill && !isSkill && !PlayerMovement.isJumping && !skillcooldown.iscoolskill[WelfSelect.iWelfCount - 1])
        {
            isSkill = true;

            isFast = true;
            //timer += Time.deltaTime;

            skillcooldown.UseSkill(WelfSelect.iWelfCount);
        }

        if (isSkill)
        {
           // playerMovement.transformValue= new Vector2(moveSpeed, playerRigidbody.velocity.y);
           if(playerMovement.state== PlayerMovement.State.playerLeft)
            {
                player.transform.Translate(new Vector2(-1,playerRigidbody.velocity.y) * moveSpeed *0.1f);
            }
            if (playerMovement.state == PlayerMovement.State.playerRight)
            {
                player.transform.Translate(new Vector2(1, playerRigidbody.velocity.y) * moveSpeed * 0.1f);
            }

            PlayerHealth.isProtect = true;
            //playerRigidbody.velocity = playerMovement.transformValue;

            timer += Time.deltaTime;
        }

        if (timer >= clearTime)
        {
            PlayerHealth.isProtect = false;

            isSkill = false;

            isFast = false;

            timer = 0;
        }
        Animation();
    }
    void GetKey()
    {
        keySkill = Input.GetButtonDown("Skill");
    }
    void Animation()
    {
        playAnim.SetBool("isFast", isFast);

        welfAnim.SetBool("isSkill", isFast);
    }
}
