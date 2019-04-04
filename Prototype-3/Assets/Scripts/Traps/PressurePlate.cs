using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Variables
    [Header("Pressure Plate Properties")]
    [SerializeField]
    public bool m_triggered = false;
    [SerializeField]
    Transform m_startPos;
    [SerializeField]
    Transform m_endPos;
    [SerializeField]
    GameObject m_pressurePlate;
   
    #endregion

    public virtual void PerformAction()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Pushable" )
        {
            m_triggered = true;
            PerformAction();
            FindObjectOfType<AudioManager>().PlayOnce("PressurePlate");
            m_pressurePlate.transform.position = new Vector3(m_pressurePlate.transform.position.x, m_endPos.position.y, transform.position.z);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit: " + other.gameObject.tag);

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Pushable")
        {
            m_triggered = false;
            PerformAction();
            m_pressurePlate.transform.position = new Vector3(m_pressurePlate.transform.position.x, m_startPos.position.y, transform.position.z);
        }

    }
}
