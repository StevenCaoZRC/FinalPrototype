using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject m_katana;
    public GameObject m_particles;
    public Transform m_slashingPoint;
    private Animator m_playeranim;

    private void Start()
    {
        m_playeranim = transform.parent.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_katana.activeSelf)
        {
            m_playeranim.SetTrigger("Slash");
        }
    }

    public void Slash()
    {
        GameObject slashParticles = Instantiate(m_particles, m_slashingPoint.position, m_slashingPoint.rotation);
        slashParticles.transform.parent = transform;
        Destroy(slashParticles, 1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DestructibleObject" && Input.GetKeyDown(KeyCode.E) 
            && m_katana.activeSelf)
        {
            other.gameObject.GetComponent<DestructibleObject>().m_iHealth -= 2;
            if (other.gameObject.GetComponent<DestructibleObject>().m_iHealth <= 0)
            {
                other.gameObject.GetComponent<DestructibleObject>().m_isDestroyed = true;
                //Destroy(other.gameObject);
            }
        }
    }
}
