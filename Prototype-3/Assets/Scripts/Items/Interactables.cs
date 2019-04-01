using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public float m_radius = 3f; //within range before you can interact with object
    public Transform m_player;
    Transform m_interactableTransform;
    protected ArmourManager m_armourManager;

    private void Start()
    {
        m_armourManager = m_player.GetComponent<ArmourManager>();
        m_interactableTransform = GetComponent<Transform>();
    }
    public virtual void Interact() {
        Debug.Log("Interacting with: " + transform.name);
    }
    
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(m_player.position, m_interactableTransform.position);
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
