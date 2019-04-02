using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    [SerializeField] private GameObject m_leftPivot;
    [SerializeField] private GameObject m_rightPivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator OpenTrap()
    {

        m_leftPivot.transform.rotation = Quaternion.Lerp(m_leftPivot.transform.rotation, m_leftEnd.rotation, m_speed * Time.deltaTime);
        m_rightPivot.transform.rotation = Quaternion.Lerp(m_rightPivot.transform.rotation, m_rightEnd.rotation, m_speed * Time.deltaTime);
        
      

        yield return null;
    }
}
