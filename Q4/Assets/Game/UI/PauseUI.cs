using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseRoot;
    public CameraMovement cam;
    public SwordController swordController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseRoot.activeInHierarchy)
            {
                cam.isActive = true;
                swordController.enabled = true;
                pauseRoot.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {

                cam.isActive = false;
                swordController.enabled = false;
                pauseRoot.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }


    public void Resume()
    {
        cam.isActive = true;
        swordController.enabled = true;
        pauseRoot.SetActive(false);
        Time.timeScale = 1;
    }
}
