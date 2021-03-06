using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject StoryMenu;
    [SerializeField]
    private GameObject StoryMenuBtn;
    [SerializeField]
    private bool isstorymenu = false;
    [SerializeField]
    private int count;
    enum SCENE
    {
        START = 0,
        HomeMama,
        Forest,
        VillageOne,
        HomeDad,
        VillageTwo,
        LaboratoryOne,
        LaboratoryTwo,
        LaboratoryThree,
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        StoryMenu.SetActive(false); //隱藏StoryMenu
        StoryMenuBtn.SetActive(false); //隱藏StoryMenuBtn
    }

    void Update()
    {
        if (AllSceneController.iscenenumber < (int)SCENE.LaboratoryOne&&!VillageTwo.getwelf)
        {
            StoryMenuBtn.SetActive(false); //隱藏StoryMenuBtn
        }
        else
        {
            StoryMenuBtn.SetActive(true); //顯示StoryMenuBtn

        }
    }
    private UnityEvent onStart;
    public void OnStoryMenuBtnClick()
    {
        isstorymenu = !isstorymenu;
        if (isstorymenu)
        {
            Time.timeScale = 0f;
            StoryMenu.SetActive(true); //顯示StoryMenu
        }
        else
        {
            Time.timeScale = 1f;
            StoryMenu.SetActive(false); //隱藏StoryMenu
        }
        
    }
}
