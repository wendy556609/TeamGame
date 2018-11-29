﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomTalk : MonoBehaviour {
    private bool isclickZ=false;
    [SerializeField]
    private GameObject talkimage;
    [SerializeField]
    private float fhidespeed = 300f;
    [SerializeField]
    private Vector3 vr0;
    [SerializeField]
    private Vector3 vr1;
    // Use this for initialization
    void Start () {
        vr0 = talkimage.transform.position;
        vr1 = talkimage.transform.position + new Vector3(-fhidespeed, 0f, 0f);
        talkimage.transform.position = vr1; //隱藏talkimage
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z"))
        {
            isclickZ = true;
        }
	}
    void OnTriggerStay2D(Collider2D collision)
    {
        if (isclickZ == false)
        {
            talkimage.transform.position = vr0; //顯示talkimage
        }
        else
        {
            talkimage.transform.position = vr1; //隱藏talkimage
            Fungus.Flowchart.BroadcastFungusMessage("MOMTALK");
        }
    }
}
