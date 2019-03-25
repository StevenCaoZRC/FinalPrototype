using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;
    public float m_smoothing = 5.0f;

    Vector3 m_offset;

    // Start is called before the first frame update
    void Start()
    {
        m_offset = transform.position - m_target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCameraPos = m_target.position + m_offset;

        transform.position = Vector3.Lerp(transform.position, targetCameraPos, Time.deltaTime * m_smoothing);
    }
}
