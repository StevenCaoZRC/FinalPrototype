using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    //This script is assigned to the prefab of the arrow
    [SerializeField]
    private float m_speed;
    [SerializeField]
    public float m_fireRate;

    private Vector3 m_pos;
    private Vector3 m_direction;
    private Quaternion m_rotation;
    // Update is called once per frame
    void Update()
    {
        if (m_speed != 0)
        {
            transform.position += transform.forward * (m_speed * Time.deltaTime);
        
        }
        else {
            Debug.Log("Arrow is Not Moving");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "ArrowTrap")
        {
            m_speed = 0;
            Destroy(gameObject);
        }
       
    }


}
