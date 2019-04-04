using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : PressurePlate
{
    [Space]
    [SerializeField]
    GameObject m_slidingDoor;
    [SerializeField]
    Transform m_startDoorPos;
    [SerializeField]
    Transform m_endDoorPos;
    [SerializeField]
    float m_doorSpeed = 2.0f;

    public override void PerformAction()
    {
        base.PerformAction();
        if (m_triggered)
            FindObjectOfType<AudioManager>().PlayOnce("OpenSlideDoor");
        else if (!m_triggered)
        {
            FindObjectOfType<AudioManager>().PlayOnce("CloseSlideDoor");
        }
    }
    private void Update()
    {
        if (m_triggered)
        {
            
            m_slidingDoor.transform.position = new Vector3(m_slidingDoor.transform.position.x, m_slidingDoor.transform.position.y, Mathf.Lerp(m_slidingDoor.transform.position.z, m_endDoorPos.position.z, Time.deltaTime * m_doorSpeed));
        }
        else if (!m_triggered)
        {
            
            m_slidingDoor.transform.position = new Vector3(m_slidingDoor.transform.position.x, m_slidingDoor.transform.position.y, Mathf.Lerp(m_slidingDoor.transform.position.z, m_startDoorPos.position.z, Time.deltaTime / m_doorSpeed));
        }
    }
  
}
