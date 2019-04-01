using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactables
{
    public Item m_item;
    
    public GameObject m_particles;
    KatanaFill m_katanaBar;
    private void Start()
    {
        m_katanaBar = GetComponent<KatanaFill>();
    }
    public override void Interact()
    {
        base.Interact();
    
        PickUpItem();
        // m_particleSystem.Play();
    }

    void PickUpItem()
    {
        Debug.Log("Picked Up: " + m_item.name);
        if (m_katanaBar.m_fill <= m_katanaBar.m_maxFill)
            m_katanaBar.m_fill += 20;
        else
            Debug.Log("You Have Collected all the items");
        ActivateArmour();
        
        GameObject particles = Instantiate(m_particles, transform.position, transform.rotation);
        
        Destroy(gameObject);

        Destroy(particles, 2);
    }

    void ActivateArmour()
    {
        switch(gameObject.tag)
        {
            case("HelmetPickup"):
            {
                 m_armourManager.ActivateHelmet(true);
                 break;
            }
            case ("ChestPickup"):
            {
                m_armourManager.ActivateChest(true);
                break;
            }
            case ("ArmPickup"):
            {
                m_armourManager.ActivateArmCuffs(true);
                break;
            }
            case ("GetaPickup"):
            {
                m_armourManager.ActivateBoots(true);
                break;
            }
            case ("BrokenKatanaPickup"):
            {
                m_armourManager.ActivateBrokenKatana(true);
                break;
            }
            case ("CompleteKatanaPickup"):
            {
                m_armourManager.ActivateCompleteKatana(true);
                break;
            }
        }
    }
}
