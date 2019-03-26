using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotating : MonoBehaviour
{
    [SerializeField]
    private float m_rotSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, m_rotSpeed * Time.deltaTime, 0));
    }
}
