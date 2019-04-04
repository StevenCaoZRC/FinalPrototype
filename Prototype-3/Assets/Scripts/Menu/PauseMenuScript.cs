using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject m_controlsMenu;
    public GameObject m_pauseMenu;
    public bool m_isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        m_controlsMenu.SetActive(false);
        m_pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        m_pauseMenu.SetActive(true);
        m_controlsMenu.SetActive(false);
        Time.timeScale = 0;
        m_isPaused = true;
    }
    public void Resume()
    {
        m_pauseMenu.SetActive(false);
        m_controlsMenu.SetActive(false);
        Time.timeScale = 1;
        m_isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Controls()
    {
        m_pauseMenu.SetActive(false);
        m_controlsMenu.SetActive(true);
    }
    public void Back()
    {
        m_pauseMenu.SetActive(true);
        m_controlsMenu.SetActive(false);
    }
}
