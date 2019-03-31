using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrows : MonoBehaviour
{
    [SerializeField]
    GameObject m_arrow;
    [SerializeField]
    GameObject m_firePoint;
    public bool m_trapActive = true;
    private float timeToFire = 0;
    void Start()
    {
        //Uncomment this and use this if Time.time breaks in reloading scene
        //InvokeRepeating("Spawn", 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        //Comment all of this if your gonna use InvokeRepeating.
        if(m_trapActive && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / m_arrow.GetComponent<ArrowMove>().m_fireRate;
            Spawn();
        }
        
    }
    void Spawn()
    {
        GameObject arrow;

        if (m_firePoint != null)
        {
            arrow = Instantiate(m_arrow, m_firePoint.transform.position, m_firePoint.transform.rotation);

        }
        else
        {
            Debug.LogError("The Fire Point is Missing");
        }

    }
  
}
