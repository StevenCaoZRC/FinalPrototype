using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    public Transform m_targetLocation;
  
    private float m_panSpeed = 10f;
    private float m_panLimit = 8f;
 
    // Update is called once per frame
    void Update()
    {
        //m_panLimit = m_targetLocation.position.x + 10;
        Vector3 camPos = transform.position;


        if (Input.GetAxis("CamHorizontal") > 0)
        {
            camPos.x += m_panSpeed * Time.deltaTime;
        }
        else if(Input.GetAxis("CamHorizontal") < 0)
        {
            camPos.x -= m_panSpeed * Time.deltaTime;
        }
        
        camPos.x = Mathf.Clamp(camPos.x, m_targetLocation.position.x - m_panLimit, m_targetLocation.position.x + m_panLimit);
        
   
        transform.position = camPos;
    }
}
