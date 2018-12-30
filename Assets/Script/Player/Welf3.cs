﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welf3 : SkillSet {
    private float clearTime = 1;
    private float timer = 0;
    //主角相關
    GameObject player;
    Animator playAnim;
    PlayerHealth playerHealth;
    Rigidbody2D playerRigidbody;
    Transform playerTrans;

    Animator welfAnim;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        playAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerTrans = player.transform;

        welfAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        GetKey();

        if (keySkill && !isSkill)
        {
            isSkill = true;

            //timer += Time.deltaTime;
        }

        if (isSkill)
        {
            timer += Time.deltaTime;
        }

        if (timer >= clearTime)
        {
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
