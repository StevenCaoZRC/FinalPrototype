using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowMove : MonoBehaviour
{
    //This script is assigned to the prefab of the arrow
    [SerializeField]
    private float m_speed;
    [SerializeField]
    public float m_fireRate;
    bool m_isHit = false;

    // Update is called once per frame
    void Update()
    {
        if (m_speed != 0)
        {
            transform.position += transform.forward * (m_speed * Time.deltaTime);
        
        }
        else
        {
            Debug.Log("Arrow is Not Moving");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !m_isHit && 
            !other.gameObject.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            m_isHit = true;
            other.gameObject.GetComponentInChildren<Animator>().SetTrigger("Dead");
        }
        else if (other.gameObject.tag != "ArrowTrap" || other.gameObject.tag != "FireballTrap")
        {
            m_speed = 0;
            Destroy(gameObject);
        }       
    }
}
