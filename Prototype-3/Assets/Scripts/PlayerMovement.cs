using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator m_playerAnim;
    public ParticleSystem m_landParticles;
    public GameObject m_jumpParticles;

    public float m_speed = 10.0f;
    public float m_jumpSpeed = 20.0f;
    public float m_jumpFallSpeed = 10.0f;

    [SerializeField] bool m_playerAtDoorEnd = false;
    [SerializeField] bool m_playerAtDoorPath = false;
    [SerializeField] bool m_isGrounded = false;

    Rigidbody m_rigidbody;
    Vector3 m_direction;
    Quaternion targetRot;
   
    float m_distToGround = 0.0f;
    bool m_jumping = false;
    bool m_doubleJumping = false;

    private void Awake()
    {
        //m_playerAnim = GetComponent<Animator>();   
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_landParticles.Stop();
        PlayGOParticles(m_jumpParticles, false);
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

        if (hor != 0.0f) //If horizontal
        {
            if(!m_playerAtDoorPath && ! m_playerAtDoorEnd)
            {
                //If not at a doorway let player move left and right
                SetDirection(new Vector3(hor, 0.0f, 0.0f));
            }
            else if(m_playerAtDoorEnd)
            {
                //If at the end of a doorway let player move left and right
                SetDirection(new Vector3(hor, 0.0f, 0.0f));
            }
        }
        else if (ver != 0.0f && m_playerAtDoorPath) //If vertical
        {
            //if in doorway, let player walk forwards and backwards
            SetDirection(new Vector3(0.0f, 0.0f, ver));
        }
        else if(transform.rotation != targetRot)
        {
            transform.rotation = targetRot;
        }
      //  Debug.Log("jump: " + m_jumping + "double: " + m_doubleJumping);

        if (CheckOnGround())
        { 
            if (GameManager.GetAxisOnce(ref m_jumping, "Jump"))
            {
                PlayGOParticles(m_jumpParticles, true);
                m_doubleJumping = false;

                m_rigidbody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
            }
        }
        else
        {
            if(m_jumping && !m_doubleJumping)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    PlayGOParticles(m_jumpParticles, true);

                    m_doubleJumping = true;
                    m_rigidbody.velocity = Vector3.zero;
                    m_rigidbody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
                }
            }
            m_landParticles.Play();

            m_rigidbody.velocity += Physics.gravity.y * (m_jumpFallSpeed) * Vector3.up * Time.deltaTime;

        }

        m_direction = m_direction * m_speed * Time.deltaTime;

        m_rigidbody.MovePosition(transform.position + m_direction);
    }

    void SetDirection(Vector3 _dir)
    {
        m_direction.Set(_dir.x, -_dir.y, _dir.z);

        targetRot = Quaternion.LookRotation(m_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * m_speed);
    }

    public void SetPlayerAtDoor(bool _atDoor)
    {
        m_playerAtDoorEnd = _atDoor;
    }

    public void SetPlayerAtDoorPath(bool _atDoorPath)
    {
        m_playerAtDoorPath = _atDoorPath;
    }

    public bool CheckOnGround()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out hit, m_distToGround))
        {
           // Debug.Log("Hit name: " + hit.collider.gameObject.name);

            return true;
        }
        return false;
    }

    void PlayGOParticles(GameObject _gameObject, bool _play)
    {
        ParticleSystem[] children = _gameObject.GetComponentsInChildren<ParticleSystem>();

        if(_play)
        {
            foreach (ParticleSystem p in children)
            {
                p.Play();
            }
        }
        else
        {
            foreach (ParticleSystem p in children)
            {
                p.Stop();
            }
        }
    }
}
