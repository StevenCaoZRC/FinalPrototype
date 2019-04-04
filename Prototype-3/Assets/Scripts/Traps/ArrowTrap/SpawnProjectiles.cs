﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField]
    GameObject m_projectile;
    [SerializeField]
    GameObject m_firePoint;
    public bool m_trapActive = true;
    private float timeToFire = 0;
    Animator m_bowAnim;

    void Start()
    {
        m_bowAnim = GetComponent<Animator>();
        //Uncomment this and use this if Time.time breaks in reloading scene
        //InvokeRepeating("Spawn", 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        //Comment all of this if your gonna use InvokeRepeating.
        if(m_trapActive && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / m_projectile.GetComponent<ArrowMove>().m_fireRate;
            Spawn();
        }
        
    }
    void Spawn()
    {
        GameObject projectile;

        if (m_firePoint != null)
        {
            if(gameObject.tag == "ArrowTrap")
            m_bowAnim.SetTrigger("Shoot");
            projectile = Instantiate(m_projectile, m_firePoint.transform.position, m_firePoint.transform.rotation);
            if(gameObject.tag == "ArrowTrap")
            if(FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().PlayOnce("ArrowShoot");
            else if(gameObject.tag == "FireballTrap")
                if(FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().PlayOnce("FireballShoot");
        }
        else
        {
            Debug.LogError("The Fire Point is Missing");
        }

    }
  
}
