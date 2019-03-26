using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactables
{
    public Item m_item;
    public GameObject m_gear;

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
        gameObject.SetActive(false);
    }
}
