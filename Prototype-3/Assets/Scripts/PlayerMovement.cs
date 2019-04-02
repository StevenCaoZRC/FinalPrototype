﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem m_landParticles;
    public GameObject m_jumpParticles;

    public float m_speed = 10.0f;
    public float m_jumpSpeed = 1.0f;
    public float m_jumpFallSpeed = 0.05f;
    public float m_jumpFallSpeedPlatform = 0.4f;
    public float m_pushForce = 5.0f;

    [SerializeField] bool m_playerAtDoorEnd = false;
    [SerializeField] bool m_playerAtDoorPath = false;

    public Animator m_playerAnim;
    Rigidbody m_rigidbody;
    Vector3 m_moveVector;
    Vector3 m_velocity;
    Vector3 m_lastMove;

    Quaternion targetRot;
    CharacterController m_controller;
    ArmourManager m_armourManager;

    float m_distToGround = 0.0f;
    float m_verticalVelocity = -1.0f;

    bool m_jumping = false;
    bool m_normalJumpPressed = false;
    bool m_doubleJumping = false;
    bool m_wallJumpingOnce = false;
    bool m_wallJumping = false;
    bool m_facingLeft = false;
    bool m_lastWallLeft = false;
    bool m_freeTurning = true;
    bool m_walkOffPlatform = false;

    float m_lastFreeTurnTimer = 0.0f;
    float m_lastFreeTurnTotal = 0.6f;

    [SerializeField] bool m_allowDoubleJump = false;
    [SerializeField] bool m_allowWallJump = true;
    [SerializeField] bool m_allowBoxPush = true;

    private void Awake()
    {
        //m_playerAnim = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_controller = GetComponent<CharacterController>();
        m_controller.detectCollisions = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_armourManager = GetComponent<ArmourManager>();
        m_landParticles.Stop();
        PlayGOParticles(m_jumpParticles, false);
        m_distToGround = GetComponent<Collider>().bounds.extents.y;
        m_lastFreeTurnTimer = 0.0f;
        //m_playerAnim.GetComponent<Animator>();
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

        if(Input.GetAxis("Jump") == 0.0f)
            m_normalJumpPressed = false;

        if (hor != 0.0f) //If horizontal
        {
            if (((hor < 0.0f && m_lastWallLeft && m_wallJumping) 
                || (hor > 0.0f && !m_lastWallLeft && m_wallJumping)) && !m_freeTurning)
            {
                //Stop player from wall jumping on same left wall
            }
            else
            {
                if (hor > 0.0f)
                {
                    m_facingLeft = false;
                }
                else if (hor < 0.0f)
                {
                    m_facingLeft = true;
                }

                if (!m_playerAtDoorPath && !m_playerAtDoorEnd)
                {
                    //If not at a doorway let player move left and right
                    SetDirection(new Vector3(hor, 0.0f, 0.0f));
                    m_freeTurning = true;
                }
                else if (m_playerAtDoorEnd || (m_playerAtDoorEnd && m_playerAtDoorPath))
                {
                    //If at the end of a doorway let player move left and right
                    SetDirection(new Vector3(hor, 0.0f, 0.0f));
                    m_freeTurning = true;
                    

                }
            }

        }
        else if (ver != 0.0f && m_playerAtDoorPath) //If vertical
        {
            //if in doorway, let player walk forwards and backwards
            SetDirection(new Vector3(0.0f, 0.0f, ver));
        }
        else if (transform.rotation != targetRot)
        {
            transform.rotation = targetRot;
        }

        if (hor == 0.0f && ver == 0.0f && !m_wallJumping)
        {
            m_playerAnim.SetBool("Run", false);
        }
        else if (hor != 0.0f || ver != 0.0f && !m_wallJumping && !m_jumping)
        {
            m_playerAnim.SetBool("WallTouch", false);
            m_playerAnim.SetBool("Run", true);
        }
        if(m_wallJumping)
        {
            m_playerAnim.SetBool("Run", false);
        }


        //Double Jump
        //if (m_jumping && !m_doubleJumping && m_allowDoubleJump)
        //{
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        m_playerAnim.SetTrigger("Jump");

        //        PlayGOParticles(m_jumpParticles, true);

        //        m_doubleJumping = true;
        //        m_velocity = Vector3.zero;
        //        m_velocity.y = -m_jumpSpeed * Physics.gravity.y * Time.deltaTime;

        //        //m_rigidbody.velocity = Vector3.zero;
        //        //m_rigidbody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
        //    }
        //}

        //If on ground
        if (m_velocity.x == 0.0f && m_velocity.z == 0.0f && m_controller.isGrounded)
        {
            m_velocity = Vector3.zero;
        }
        if (m_controller.isGrounded)
        {
            m_playerAnim.SetBool("Grounded", true);
            m_playerAnim.SetBool("WallTouch", false);

            m_wallJumpingOnce = false;
            m_wallJumping = false;
            m_walkOffPlatform = false;

            if (m_velocity.x != 0.0f)
            {
                m_velocity = Vector3.zero;
            }
            
            if (GameManager.GetAxisOnce(ref m_jumping, "Jump"))
            {
                m_normalJumpPressed = true;
                m_playerAnim.SetTrigger("Jump");
                m_playerAnim.SetBool("Grounded", false);
                JumpMotion();
                //m_doubleJumping = false;
            }
        }
        else
        {

            //If not on ground
            m_landParticles.Play();
            
            m_velocity += Physics.gravity.y * (m_jumpFallSpeed) * Vector3.up * Time.deltaTime;
            
            m_playerAnim.SetBool("Grounded", false);
        }

        //Change start gravity after walking off platform to prevent lightspeed falling
        if (!m_walkOffPlatform && !m_jumping)
        {
            m_walkOffPlatform = true;
            m_velocity += (Physics.gravity.y * (m_jumpFallSpeedPlatform) * Vector3.up * Time.deltaTime);
        }
        //if(m_wallJumping)
        //{
        //    if(!m_freeTurning)
        //        m_lastFreeTurnTimer += Time.deltaTime;
        //    if(m_lastFreeTurnTimer > m_lastFreeTurnTotal)
        //    {
        //        m_freeTurning = true;
        //        m_lastFreeTurnTimer = 0.0f;
        //    }

        //    m_moveVector = m_moveVector * m_speed/2 * Time.deltaTime;
        //}
        //else
        //{
        m_moveVector = m_moveVector * m_speed * Time.deltaTime;
        //}

        //m_controller.Move(m_velocity);

        CollisionFlags flags = m_controller.Move(m_moveVector + m_velocity);
        bool headTouch = (flags & CollisionFlags.CollidedAbove) != 0;
        CheckHeadTouched(headTouch);
    }

    void JumpMotion()
    {
        PlayGOParticles(m_jumpParticles, true);
        m_velocity.y = -m_jumpSpeed * Physics.gravity.y * Time.deltaTime;
    }

    void SetDirection(Vector3 _dir)
    {
        m_moveVector = Vector3.zero;
        if (_dir.x > 0.0f) m_facingLeft = false;
        else if (_dir.x < 0.0f) m_facingLeft = true;

        m_moveVector.Set(_dir.x, _dir.y, _dir.z);
        if (m_moveVector != Vector3.zero)
        {
            targetRot = Quaternion.LookRotation(m_moveVector);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * m_speed);
        }

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
        if (Physics.SphereCast(transform.position, 0.4f, -Vector3.up, out hit, m_distToGround))
        {
            return true;
        }
        return false;
    }

    public void CheckHeadTouched(bool _headTouched)
    {
        if (_headTouched && m_velocity.y > 0)
        {
            Debug.Log("head youch");
            m_velocity.y = 0;
        }
    }

    //Play particles part of a gameobject
    void PlayGOParticles(GameObject _gameObject, bool _play)
    {
        ParticleSystem[] children = _gameObject.GetComponentsInChildren<ParticleSystem>();

        if (_play)
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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If hitting jumpable wall + in air + wall is not sloped
        if (!CheckOnGround() && hit.normal.y < 0.1f && hit.collider.tag == "JumpableWall")
        {
            if (GameManager.GetAxisOnce(ref m_wallJumpingOnce, "WallJump") && !m_normalJumpPressed && m_armourManager.IsArmCuffActive())
            {

                m_wallJumping = true;
                m_freeTurning = false;
                if (m_facingLeft)
                    m_lastWallLeft = true;
                else
                    m_lastWallLeft = false;
                Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                //m_velocity = hit.normal * m_speed / 3 * Time.deltaTime;
                //m_velocity.y = -m_jumpSpeed * Physics.gravity.y * Time.deltaTime;
                m_playerAnim.SetTrigger("Climbing");
                m_playerAnim.SetBool("WallTouch", false);

                m_playerAnim.SetBool("Grounded", false);

                JumpMotion();
            }
            m_playerAnim.SetBool("WallTouch", true);
        }
        else
        {
            m_playerAnim.SetBool("WallTouch", false);
        }

        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null || rigidbody.isKinematic)
        {
            return;
        }

        if (hit.collider.tag == "Pushable" && m_armourManager.IsBootActive())
        {
            hit.collider.gameObject.GetComponent<PushableObject>().m_pushed = true;
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
            rigidbody.velocity = pushDir * m_pushForce / rigidbody.mass;
            //Animation
        }


    }

}
