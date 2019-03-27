using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject m_katana;
    public GameObject m_particles;
    public Transform m_slashingPoint;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_katana.activeSelf)
        {
          
               GameObject temp1 = Instantiate(m_particles, m_slashingPoint.position, m_slashingPoint.rotation);
               Destroy(temp1, 1);

         
          
           
         
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DestructibleObject" && Input.GetKeyDown(KeyCode.E) && m_katana.activeSelf)
        {
            
            other.gameObject.GetComponent<DestructibleObject>().m_iHealth -= 2;
            if (other.gameObject.GetComponent<DestructibleObject>().m_iHealth <= 0)
            {
                Destroy(other.gameObject);
            }
            
        }
    }
}
