using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactables
{
    public Item m_item;
    public GameObject m_gear;
    public GameObject m_particles;

    public override void Interact()
    {
     
        base.Interact();
        
        PickUpItem();
       
        // m_particleSystem.Play();
    }

    void PickUpItem()
    {
        Debug.Log("Picked Up: " + m_item.name);
        m_gear.SetActive(true);
        GameObject particles = Instantiate(m_particles, transform.position, transform.rotation);
        
        Destroy(gameObject);

        Destroy(particles, 2);


    }
}
