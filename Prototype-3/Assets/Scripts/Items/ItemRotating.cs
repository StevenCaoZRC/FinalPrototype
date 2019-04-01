using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotating : MonoBehaviour
{
    public bool m_rotate = true;
    float m_amplitude = 0.005f;
    float m_frequency = 0.3f;

    [SerializeField]
    private float m_rotSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_rotate)
            transform.Rotate(new Vector3(0, m_rotSpeed * Time.deltaTime, 0));
        else
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.fixedTime * Mathf.PI * m_frequency) * m_amplitude, transform.position.z);

    }
}
