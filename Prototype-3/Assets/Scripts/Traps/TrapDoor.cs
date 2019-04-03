using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : PressurePlate
{
    [Space]
    [SerializeField]
    private GameObject m_leftHinge;
    [SerializeField]
    private Transform m_leftEnd;

    [SerializeField]
    private GameObject m_rightHinge;
    [SerializeField]
    private Transform m_rightEnd;
    private float m_timer = 8f;
    private bool m_startTimer = false;
    private float m_speed = 5f;
    public override void PerformAction()
    {
        base.PerformAction();
        m_startTimer = true;
    }

    private void Update()
    {
        StartCoroutine(OpenTrap());
        if (m_timer > 0.0f && m_startTimer)
        {
            m_timer -= Time.deltaTime;
        }
    }       
    IEnumerator OpenTrap()
    {
        if (m_triggered)
        {
            m_leftHinge.transform.rotation = Quaternion.Lerp(m_leftHinge.transform.rotation, m_leftEnd.rotation, m_speed * Time.deltaTime);
            m_rightHinge.transform.rotation = Quaternion.Lerp(m_rightHinge.transform.rotation, m_rightEnd.rotation, m_speed * Time.deltaTime);
           
        }
       
        if (!m_triggered)
        {
            yield return new WaitForSeconds(8);
            m_leftHinge.transform.rotation = Quaternion.Lerp(m_leftHinge.transform.rotation, Quaternion.identity, m_speed * Time.deltaTime);
            m_rightHinge.transform.rotation = Quaternion.Lerp(m_rightHinge.transform.rotation, Quaternion.identity, m_speed * Time.deltaTime);
        }
        
        yield return null;
    }
}
