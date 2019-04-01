using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactables
{
    public Item m_item;
    
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
