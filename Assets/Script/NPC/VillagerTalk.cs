﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class VillagerTalk : MonoBehaviour {
    private bool isclickZ;
    private bool isfungus;
    [SerializeField]
    private GameObject talkimage;
    [SerializeField]
    private float fhidespeed = 300f;
    [SerializeField]
    private Vector3 vr0;
    [SerializeField]
    private Vector3 vr1;
    [SerializeField]
    private Flowchart flowchart;
    bool hastalk;
    // Use this for initialization
    void Start()
    {
        vr0 = talkimage.transform.position;
        vr1 = talkimage.transform.position + new Vector3(-fhidespeed, 0f, 0f);
        talkimage.transform.position = vr1; //隱藏talkimage
    }

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isend"+ gameObject.name) == true)
        {
            if (hastalk != true)
            {
                isfungus = false;
                isclickZ = false;
                talkimage.transform.position = vr0;
            }
            flowchart.SetBooleanVariable("isend" + gameObject.name, false);
        }
        if (flowchart.GetBooleanVariable("isplayer")){
            PlayerMovement.isMenu = false;
        }
        else if (!flowchart.GetBooleanVariable("isplayer"))
        {
            PlayerMovement.isMenu = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown("z") && isfungus == false && isclickZ == false)  //未對話時才可按下z
        {
            isclickZ = true;
            if (hastalk != true&&isclickZ)
            {
                talkimage.transform.position = vr1; //隱藏talkimage
                Fungus.Flowchart.BroadcastFungusMessage(this.gameObject.name);
                isfungus = true;
                if (gameObject.name == "Odeliaspecial")
                    hastalk = true;
                isclickZ = false;
            }
        }
        else if (isclickZ == false && isfungus == false&& flowchart.GetBooleanVariable("isplayer") == true)  //進入未動作
        {
            if (gameObject.tag!= "specialodelia")
            {
                talkimage.transform.position = vr0; //顯示talkimage
            }
        }
        
        else if(isclickZ==true&&isfungus==false)  //對話
        {
            //if (hastalk != true)
            //{
            //    talkimage.transform.position = vr1; //隱藏talkimage
            //    Fungus.Flowchart.BroadcastFungusMessage(this.gameObject.name);
            //    isfungus = true;
            //    if(gameObject.name == "Odeliaspecial")
            //        hastalk = true;
            //    isclickZ = false;
            //    Debug.Log("S");
            //}
        }
        else if (isfungus == true && isclickZ == false && flowchart.GetBooleanVariable("isplayer") == true)
        {
            if (gameObject.name != "Odeliaspecial")
            {
                talkimage.transform.position = vr0;
                isfungus = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isclickZ = false;
        talkimage.transform.position = vr1; //隱藏talkimage
    }
}
