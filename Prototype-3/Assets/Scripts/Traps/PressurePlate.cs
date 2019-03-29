using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Variables
    [System.Serializable]
    public class ObjectTrigger
    {
        [SerializeField]
        GameObject m_objectToTrigger;
        [SerializeField]
        Transform m_objectStartPos;
        [SerializeField]
        Transform m_objectEndPos;
    }
    [Header("Object You want to trigger with Plate")]
    public ObjectTrigger m_TriggeredByPlate;
    [Space]
    [Header("Pressure Plate Properties")]
    [SerializeField]
    bool m_triggered = false;
    [SerializeField]
    Transform m_startPos;
    [SerializeField]
    Transform m_endPos;
    // Start is called before the first frame update
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_triggered = true;


            transform.position = new Vector3(transform.position.x, m_endPos.position.y, transform.position.z);

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_triggered = false;

            transform.position = new Vector3(transform.position.x, m_startPos.position.y, transform.position.z);


        }

    }
}
