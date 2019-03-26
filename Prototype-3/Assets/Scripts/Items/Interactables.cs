using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public float m_radius = 3f; //within range before you can interact with object
    public Transform m_player;
    public Transform m_interactableTransform;
    public ParticleSystem m_particleSystem;
    private void Start()
    {
        m_particleSystem.Stop();
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

            StartCoroutine(Interacting());
        }
    }

    private void OnDrawGizmos()
    {
        if (m_interactableTransform == null)
            m_interactableTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_interactableTransform.position, m_radius);
    }
    IEnumerator Interacting()
    {

        m_particleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        Interact();
        //Destroy(gameObject);
        yield return null;
    }
}
