using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed = 10.0f;
    public Animator m_playerAnim;
    Rigidbody m_rigidbody;
    Vector3 m_direction;
    Quaternion targetRot;
    public float m_jumpSpeed = 0.0f;
    public bool m_playerAtDoor = false;
    public bool m_isGrounded = false;
    float m_distToGround = 0.0f;

    private void Awake()
    {
        //m_playerAnim = GetComponent<Animator>();   
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move();
    }

    private void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        float jump = 0.0f;
        

        if (hor != 0.0f) //If horizontal
        {
            m_direction.Set(hor, 0.0f, 0.0f);

            targetRot = Quaternion.LookRotation(m_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * m_speed);
        }
        else if (ver != 0.0f && m_playerAtDoor) //If vertical
        {
            m_direction.Set(0.0f, 0.0f, ver);

            targetRot = Quaternion.LookRotation(m_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * m_speed);
        }
        else if(transform.rotation != targetRot)
        {
            transform.rotation = targetRot;
        }

        if (Input.GetAxisRaw("Jump") != 0.0f && CheckOnGround())
        {
            m_rigidbody.AddForce(Vector3.up * Input.GetAxisRaw("Jump") * m_jumpSpeed, ForceMode.Impulse);
        }
        
        //m_direction.Set(m_direction.x, jump, m_direction.z);



        m_direction = m_direction * m_speed * Time.deltaTime;

        m_rigidbody.MovePosition(transform.position + m_direction);
    }

    public void SetPlayerAtDoor(bool _atDoor)
    {
        m_playerAtDoor = _atDoor;
    }

    public bool CheckOnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, m_distToGround + 0.1f);
    }
}
