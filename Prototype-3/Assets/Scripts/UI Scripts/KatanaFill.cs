using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatanaFill : MonoBehaviour
{
    public Image m_katanaProgressBar;
    public float m_maxFill = 100f;
    public float m_fill = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_fill = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_katanaProgressBar.fillAmount = m_fill / m_maxFill; 
    }
}
