using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public GameObject StartMenu;
    public GameObject CreditMenu;
    public GameObject SettingsMenu;

    private void Start()
    {
        StartMenu.SetActive(true);
        CreditMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    public void Level()
    {
        
    }

    public void Credits()
    {
        StartMenu.SetActive(false);
        CreditMenu.SetActive(true);
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        StartMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
}
