using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEvent : MonoBehaviour
{
    PlayerAttack m_playerAttack;
    public Transform m_playerHandParent;
    public Transform m_playerHandPos;
    public Transform m_katanaRestParent;
    public Transform m_katanaRestPos;

    // Start is called before the first frame update
    void Start()
    {
        m_playerAttack = transform.parent.GetComponentInChildren<PlayerAttack>();
    }

    // Update is called once per frame
    void Slash()
    {
        m_playerAttack.Slash();
        m_playerAttack.GetKatana().transform.parent = m_playerHandParent;
        m_playerAttack.GetKatana().transform.position = m_playerHandPos.position;
        m_playerAttack.GetKatana().transform.rotation = m_playerHandPos.rotation;
    }

    //void ParentKatana()
    //{

    //}

    void ReleaseKatana()
    {
        m_playerAttack.GetKatana().transform.parent = m_katanaRestParent;
        m_playerAttack.GetKatana().transform.position = m_katanaRestPos.position;
        m_playerAttack.GetKatana().transform.rotation = m_katanaRestPos.rotation;
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
