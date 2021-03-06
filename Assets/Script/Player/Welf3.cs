using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welf3 : SkillSet {
    //public static bool isSkill = false;
    private float clearTime = 3;
    private float timer = 0;
    //主角相關
    GameObject player;
    Animator playAnim;
    PlayerHealth playerHealth;
    Rigidbody2D playerRigidbody;
    Transform playerTrans;
    public GameObject welfuiobject;
    private SkillCoolDown skillcooldown;

    Animator welfAnim;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        playAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
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

            //PlayerMovement.canMove = false;
            //timer += Time.deltaTime;

            skillcooldown.UseSkill(WelfSelect.iWelfCount);
        }

        if (isSkill)
        {
            PlayerHealth.isProtect = true;

            timer += Time.deltaTime;
        }

        if (timer >= clearTime)
        {
            PlayerHealth.isProtect = false;

            //PlayerMovement.canMove = true;
            isSkill = false;

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
        playAnim.SetBool("isSkill", isSkill);

        welfAnim.SetBool("isSkill", isSkill);
    }
}
