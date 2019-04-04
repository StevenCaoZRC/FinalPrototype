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
        if(FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().PlayOnce("Collection");
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
                 m_playerAnim.SetTrigger("HelmetGained");
                    
                    if(m_speechText != null)
                    {
                        m_speechText.text = "HELMET: CONGRATULATIONS";
                        m_speechText.gameObject.SetActive(true);
                    }
                 
                    break;
            }
            case ("ChestPickup"):
            {
                 m_armourManager.ActivateChest(true);
                m_playerAnim.SetTrigger("BodyArmourGained");
                    if (m_speechText != null)
                    {
                        m_speechText.text = "BODY: No skill defined yet";
                        m_speechText.gameObject.SetActive(true);
                    }

                    break;
            }
            case ("ArmPickup"):
            {
                 m_armourManager.ActivateArmCuffs(true);
                 m_playerAnim.SetTrigger("ArmCuffsGained");
                    if (m_speechText != null)
                    {
                        m_speechText.text = "ARM CUFFS: You can now Wall jump !";
                        m_speechText.gameObject.SetActive(true);
                    }

                    break;
            }
            case ("GetaPickup"):
            {
                m_armourManager.ActivateBoots(true);
                m_playerAnim.SetTrigger("GetaGained");
                    if (m_speechText != null)
                    {
                        m_speechText.text = "GETA: You may now push wooden drawers";
                        m_speechText.gameObject.SetActive(true);
                    }
                break;
            }
            case ("BrokenKatanaPickup"):
            {
                m_armourManager.ActivateBrokenKatana(true);
                m_playerAnim.SetTrigger("ArmCuffsGained");
                    if (m_speechText != null)
                    {
                        m_speechText.text = "Press 'E' to slash. Paper ? Wooden Boxes ?";
                        m_speechText.gameObject.SetActive(true);
                    }

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
