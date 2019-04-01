using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DestructibleType
{
    NONE, WALL, BOX, TRAP
}

public class DestructibleObject : MonoBehaviour
{
    

    public int m_iHealth = 1;
    public bool m_isDestroyed = false;
    public bool m_countdown = false;
    public DestructibleType m_wallType = DestructibleType.NONE;

    public TextMeshProUGUI m_speechText;

    float m_textFadeTimer = 0.0f;
    float m_textFadeTotal = 0.5f;

    private void Start()
    {
        m_countdown = false;
        m_isDestroyed = false;
        if(m_speechText != null)
            m_speechText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(m_countdown)
        {
            m_textFadeTimer += Time.deltaTime;

            if (m_textFadeTimer >= m_textFadeTotal)
            {
                m_countdown = false;
                m_textFadeTimer = 0.0f;
                m_speechText.gameObject.SetActive(false);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(m_wallType == DestructibleType.WALL)
            {
                m_speechText.gameObject.SetActive(true);

                m_speechText.text = "I might be able to break this wall...";
                Debug.Log("I might be able to break this wall...");
            }
            else if (m_wallType == DestructibleType.BOX)
            {
                m_speechText.text = "I might be able to break this box...";
                Debug.Log("I might be able to break this box...");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_countdown = true;
        }
    }
}
