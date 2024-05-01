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

    public AudioSource onClick;

    private void Start()
    {
        StartMenu.SetActive(true);
        CreditMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    public void Level()
    {
        onClick.Play();
        SceneManager.LoadScene("Game");
    }

    public void Credits()
    {
        onClick.Play();
        StartMenu.SetActive(false);
        CreditMenu.SetActive(true);
        
    }

    public void ClearCredits()
    {
        onClick.Play();
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }
    public void Dylan()
    {
        onClick.Play();
        DylanPanel.SetActive(true);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }

    public void Marcus()
    {
        onClick.Play();
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(true);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(false);
    }

    public void Sophia()
    {
        onClick.Play();
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(true);
        FinchPanel.SetActive(false);
    }

    public void Finch()
    {
        onClick.Play();
        DylanPanel.SetActive(false);
        MarcusPanel.SetActive(false);
        SophiaPanel.SetActive(false);
        FinchPanel.SetActive(true);
    }

    public void Exit()
    {
        onClick.Play();
        Application.Quit();
    }

    public void Settings()
    {
        onClick.Play();
        StartMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void Main()
    {
        onClick.Play();
        StartMenu.SetActive(true);
        CreditMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
}
