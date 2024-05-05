using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Fade fade;
    public CameraMovement cam;

    public void Resume()
    {
        cam.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1.0f;

        Time.timeScale = 1.0f;

        GetComponent<RectTransform>().localScale = Vector3.zero;

    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        fade.FadeIn = true;
        StartCoroutine(sceneLoad());
    }

    IEnumerator sceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
