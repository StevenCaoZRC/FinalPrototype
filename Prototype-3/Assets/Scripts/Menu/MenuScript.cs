using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_controlsMenu;
    public GameObject m_creditsMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        m_mainMenu.SetActive(true);
        m_controlsMenu.SetActive(false);
        m_creditsMenu.SetActive(false);
      
    }
    public void CreditBtn()
    {
        m_mainMenu.SetActive(false);
        m_controlsMenu.SetActive(false);
        m_creditsMenu.SetActive(true);
    }
    public void ControlsBtn()
    {
        m_mainMenu.SetActive(false);
        m_controlsMenu.SetActive(true);
        m_creditsMenu.SetActive(false);
    }
    public void BackBtn()
    {
        m_mainMenu.SetActive(true);
        m_controlsMenu.SetActive(false);
        m_creditsMenu.SetActive(false);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void StartBtn()
    {
        GameManager.GetInstance().ResetVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
        if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Stop("MenuMusic");
        if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Play("InGameMusic");
    }


   
}
