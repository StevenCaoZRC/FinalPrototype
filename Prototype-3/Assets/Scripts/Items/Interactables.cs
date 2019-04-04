using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactables : MonoBehaviour
{
    public float m_radius = 3f; //within range before you can interact with object
    GameObject m_player;
    public Transform m_interactableTransform;
    protected ArmourManager m_armourManager;
    KatanaFill m_katanaBar;
    protected Animator m_playerAnim;
    public TextMeshProUGUI m_speechText;
    protected float textTimer = 0.0f;
    float textTotal = 5.0f;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_armourManager = m_player.GetComponent<ArmourManager>();
        m_interactableTransform = GetComponent<Transform>();
        m_katanaBar = FindObjectOfType<KatanaFill>();
        m_playerAnim = m_player.GetComponentInChildren<Animator>();
        if (m_speechText != null)
            m_speechText.gameObject.SetActive(false);
    }
    public virtual void Interact() {
        Debug.Log("Interacting with: " + transform.name);
        if(m_katanaBar != null)
        {
            if (m_katanaBar.m_fill <= m_katanaBar.m_maxFill)
                m_katanaBar.m_fill += 20;
            else
                Debug.Log("You Have Collected all the items");
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        IsItemAlreadyCollected();
        float distance = Vector3.Distance(m_player.transform.position, m_interactableTransform.position);
        if(distance < m_radius) // if they are within the radius
        {
            Interact();
        }

        if(m_speechText.gameObject.activeSelf)
        {
            textTimer += Time.deltaTime;
            if (textTimer >= textTotal)
            {
                m_speechText.gameObject.SetActive(false);
                textTimer = 0.0f;

            }
        }
        else
        {
            m_speechText.gameObject.SetActive(false);

            textTimer = 0.0f;

        }

    }

    private void IsItemAlreadyCollected()
    {
        switch (gameObject.tag)
        {
            case ("HelmetPickup"):
            {
                if (m_armourManager.IsHelmetActive())
                    gameObject.SetActive(false);
                break;
            }
            case ("ChestPickup"):
            {
                if (m_armourManager.IsChestActive())
                    gameObject.SetActive(false);
                break;
            }
            case ("ArmPickup"):
            {
                if (m_armourManager.IsArmCuffActive())
                    gameObject.SetActive(false);
                break;
            }
            case ("GetaPickup"):
            {
                if (m_armourManager.IsBootActive())
                    gameObject.SetActive(false);
                break;
            }
            case ("BrokenKatanaPickup"):
            {
                if (m_armourManager.IsBrokenKatanaActive())
                    gameObject.SetActive(false);
                break;
            }
            case ("CompleteKatanaPickup"):
            {
                if (m_armourManager.IsCompleteKatanaActive())
                    gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_interactableTransform == null)
            m_interactableTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_interactableTransform.position, m_radius);
    }
}
