using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Interactables
{
    public GameObject m_box;
    
    public override void Interact()
    {
        base.Interact();
        Slash();
    }
    void Slash()
    {
        Debug.Log("Slashed Object: " );
      
        Destroy(m_box);
        

    }
}
