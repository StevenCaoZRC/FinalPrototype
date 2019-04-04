using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public float m_radius = 3f; //within range before you can interact with object
    GameObject m_player;
    public Transform m_interactableTransform;
    protected ArmourManager m_armourManager;
    KatanaFill m_katanaBar;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_armourManager = m_player.GetComponent<ArmourManager>();
        m_interactableTransform = GetComponent<Transform>();
        m_katanaBar = FindObjectOfType<KatanaFill>();
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
        float distance = Vector3.Distance(m_player.transform.position, m_interactableTransform.position);
        if(distance < m_radius) // if they are within the radius
        {
            Interact();
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
