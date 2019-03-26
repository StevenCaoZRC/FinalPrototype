using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject m_katana;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DestructibleObject" && Input.GetKeyDown(
           KeyCode.E) && m_katana.activeSelf)
        {
            other.gameObject.GetComponent<DestructibleObject>().m_iHealth -= 2;
            if (other.gameObject.GetComponent<DestructibleObject>().m_iHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
