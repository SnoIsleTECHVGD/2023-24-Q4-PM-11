using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public GameObject StartMenu;
    public GameObject CreditMenu;
    public GameObject SettingsMenu;
    public GameObject DylanPanel;
    public GameObject MarcusPanel;
    public GameObject SophiaPanel;
    public GameObject FinchPanel;

    private void Start()
    {
        StartMenu.SetActive(true);
        CreditMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    public void Level()
    {
        SceneManager.LoadScene("Game");
    }

    public void Credits()
    {
        StartMenu.SetActive(false);
        CreditMenu.SetActive(true);
        
    }

    public void ClearCredits()
    {
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }
    public void Dylan()
    {
        DylanPanel.SetActive(true);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }

    public void Marcus()
    {
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(true);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }

    public void Sophia()
    {
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(true);
        FinchPanel.SetActive(false);
    }

    public void Finch()
    {
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(true);
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

    public void Main()
    {
        StartMenu.SetActive(true);
        CreditMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
}
