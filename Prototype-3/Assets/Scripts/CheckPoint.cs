using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    ArmourManager m_armourManager;
    CheckPointManager m_checkPointManager;
    public bool m_activeCheckpoint = false;
    public Color m_inactiveColor;
    public Color m_activeColor;
    public Material m_activeMaterial;
    public Material m_inactiveMaterial;
    public GameObject m_currentMaterial;
    public GameObject m_particles;
    public GameObject m_particlePos;
    public GameObject m_spawnPos;

    void Start()
    {
        m_checkPointManager = transform.parent.transform.parent.GetComponent<CheckPointManager>();
        m_inactiveMaterial.SetColor("_EmissionColor", m_inactiveColor);
        m_activeMaterial.SetColor("_EmissionColor", m_activeColor);
        m_currentMaterial.GetComponent<Renderer>().material = m_inactiveMaterial;
        m_armourManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ArmourManager>();
    }

    //private void Update()
    //{
    //    m_inactiveMaterial.SetColor("_EmissionColor", m_inactiveColor);
    //    m_activeMaterial.SetColor("_EmissionColor", m_activeColor);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_checkPointManager.SetAllInactive();
            m_armourManager = other.gameObject.GetComponent<ArmourManager>();
            GameObject particles = Instantiate(m_particles, transform.position, transform.rotation);
            Destroy(particles, 2);

            m_activeCheckpoint = true;
            m_currentMaterial.GetComponent<Renderer>().material = m_activeMaterial;
            GameManager.GetInstance().SetCheckPoint(this);
            GameManager.GetInstance().SetSpawnPos(transform.position);
            Debug.Log(GameManager.GetInstance().GetCheckPoint().name);
        }
    }

    public void SetInactive()
    {
        m_activeCheckpoint = false;
        m_currentMaterial.GetComponent<Renderer>().material = m_inactiveMaterial;

    }
}
