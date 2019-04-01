using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int m_iHealth = 4;
    public bool m_isDestroyed = false;
    public GameObject m_brokenBox;
    private void Start()
    {
        m_isDestroyed = false;
    }
    private void Update()
    {
        if (m_isDestroyed)
        {
            StartCoroutine(BreakingBox());
            
        }
    }
    IEnumerator BreakingBox()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(m_brokenBox, transform.position, transform.rotation);
        Destroy(gameObject);
        
        //Destroy(box);
        m_isDestroyed = false;
        yield return null;
    }
}
