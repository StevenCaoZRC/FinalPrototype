using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    [SerializeField] private GameObject m_leftPivot;
    [SerializeField] private GameObject m_rightPivot;
    [SerializeField] private Transform m_leftEndRot;
    [SerializeField] private Transform m_rightEndRot;
    [SerializeField] private float m_speed = 5;
    private bool m_broken = false;
    // Start is called before the first frame update
    void Start()
    {
        m_broken = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (m_broken)
        {
            m_leftPivot.transform.rotation = Quaternion.Lerp(m_leftPivot.transform.rotation, m_leftEndRot.rotation, m_speed * Time.deltaTime);
            m_rightPivot.transform.rotation = Quaternion.Lerp(m_rightPivot.transform.rotation, m_rightEndRot.rotation, m_speed * Time.deltaTime);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            m_broken = true;
            
        }
    }
    IEnumerator OpenTrap()
    {
        yield return new WaitForSeconds(1);
        m_broken = true;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        m_broken = false;
        yield return null;
    }
}
