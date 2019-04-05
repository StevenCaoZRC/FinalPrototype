using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public GameObject m_landParticles;
    public GameObject m_jumpParticles;

    public float m_speed = 10.0f;
    public float m_jumpSpeed = 1.0f;
    public float m_jumpFallSpeed = 0.05f;
    public float m_jumpFallSpeedPlatform = 0.4f;
    public float m_pushForce = 5.0f;
    [SerializeField] float m_frontZPos = 0.0f;
    [SerializeField] float m_backZPos = 15.0f;
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

    bool m_normalJumpPressed = false;
    bool m_doubleJumping = false;
    bool m_wallJumpingOnce = false;
    bool m_wallJumping = false;
    bool m_facingLeft = false;
    bool m_lastWallLeft = false;
    bool m_freeTurning = true;
    bool m_walkOffPlatform = false;
    bool m_wallTouch = false;
    int m_firstGroundTouch = 0;
    bool m_jumped = false;
    bool m_airJumpPressed = false;
    bool m_pushingObject = false;
    bool m_endLevel = false;

    float m_wallTouchFallSpeed = 0.1f;
    float m_wallTouchTimer = 0.0f;
    float m_wallTouchTotal = 0.5f;

    float m_initJumpTimer = 0.0f;
    float m_initJumpTotal = 0.3f;

    float m_lastFreeTurnTimer = 0.0f;
    float m_lastFreeTurnTotal = 0.75f;

    [SerializeField] bool m_pausePlayer = false;

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
        
        PlayGOParticles(m_landParticles, false);
        PlayGOParticles(m_jumpParticles, false);
        m_distToGround = GetComponent<Collider>().bounds.extents.y;
        m_lastFreeTurnTimer = 0.0f;

    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("ArmCuffsGained") 
            || m_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("GetaGained")
            || m_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("BodyArmourGained")
            || m_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            m_pausePlayer = true;
        }
        else
        {
            m_pausePlayer = false;
        }

        if(m_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("HelmetGained") && !m_endLevel)
        {
            m_endLevel = true;
            m_pausePlayer = true;
            Invoke("MainMenu", 3.0f);
        }

        Move();
    }
    bool sideTouch = false;
    private void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (!m_pausePlayer)
        {
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
                m_playerAnim.SetBool("Pushing", false);

            }
            else if (hor != 0.0f || ver != 0.0f && !m_wallJumping && !m_jumped)
            {
                m_playerAnim.SetBool("WallTouch", false);
                m_playerAnim.SetBool("Run", true);
                if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().PlayOnce("Running");

            }

            if (m_wallJumping)
            {
                m_playerAnim.SetBool("Run", false);
                m_playerAnim.SetBool("Pushing", false);

            }

        }
        else
        {
            m_playerAnim.SetBool("Run", false);
            m_playerAnim.SetBool("Pushing", false);

        }


        if (m_pushingObject)
        {
            m_playerAnim.SetBool("Pushing", true);
            //m_playerAnim.SetBool("Pushing", true);
        }
        else
        {
            m_playerAnim.SetBool("Pushing", false);

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

        if (Input.GetAxis("Jump") == 0.0f)
        {
            m_wallJumpingOnce = false;
            m_normalJumpPressed = false;
        }
        

        if (m_controller.isGrounded)
        {
            if (m_firstGroundTouch == 2)
                m_firstGroundTouch = 0;
            else
                m_firstGroundTouch = 1;
            
            if (Input.GetAxis("Jump") == 0.0f)
            {
                m_airJumpPressed = false;
                m_jumped = false;
                m_wallTouchTimer = 0.0f;
                m_initJumpTimer = 0.0f;
            }

            m_playerAnim.SetBool("Grounded", true);
            m_playerAnim.SetBool("WallTouch", false);

            m_wallJumping = false;
            m_walkOffPlatform = false;

            if (m_velocity.x != 0.0f)
            {
                m_velocity = Vector3.zero;
            }
            if (!m_pausePlayer)
            {
                if (Input.GetAxis("Jump") != 0.0f && !m_jumped && !m_airJumpPressed && !m_wallJumpingOnce)
                {

                    Debug.Log("----------------------------------Nrom");
                    m_normalJumpPressed = true;
                    m_playerAnim.SetTrigger("Jump");
                    m_playerAnim.SetBool("Grounded", false);
                    JumpMotion();
                    m_jumped = true;
                    //m_doubleJumping = false;
                }
            }
        }
        else
        {
            if (Input.GetAxis("Jump") != 0.0f && !m_airJumpPressed && !m_wallJumping && !m_jumped && !m_pausePlayer)
            {
                Debug.Log("----------------------------------Nrom");

                m_airJumpPressed = true;
                m_playerAnim.SetTrigger("Jump");
                m_playerAnim.SetBool("Grounded", false);

                JumpMotion();
                //m_doubleJumping = false;
            }
            else
            {
                m_firstGroundTouch = 2;
                m_velocity += Physics.gravity.y * (m_jumpFallSpeed) * Vector3.up * Time.deltaTime;

                m_playerAnim.SetBool("Grounded", false);
            }
        }

        //If not on ground
        if (m_firstGroundTouch == 0)
            PlayGOParticles(m_landParticles, true);


        if (m_jumped)
            m_initJumpTimer += Time.deltaTime;
        else
            m_initJumpTimer = 0.0f;

        //Change start gravity after walking off platform to prevent lightspeed falling
        if (!m_walkOffPlatform && !m_jumped && !m_wallJumping && !m_airJumpPressed)
        {
            m_walkOffPlatform = true;
            m_velocity += (Physics.gravity.y * (m_jumpFallSpeedPlatform) * Vector3.up * Time.deltaTime);
        }

        if (m_wallJumping)
        {
            if (!m_freeTurning)
                m_lastFreeTurnTimer += Time.deltaTime;
            else
                m_lastFreeTurnTimer = m_lastFreeTurnTotal;
            if (m_lastFreeTurnTimer >= m_lastFreeTurnTotal)
            {
                m_freeTurning = true;
                m_lastFreeTurnTimer = 0.0f;

                m_playerAnim.SetBool("WallTouch", false);
                m_wallTouch = false;
            }

            //    m_moveVector = m_moveVector * m_speed/2 * Time.deltaTime;
        }

        if ((m_controller.transform.position.z != 0.0f || m_controller.transform.position.z != 15.0f) && !m_playerAtDoorPath)
        {
            //m_controller.enabled = false;
            if (transform.position.z < m_backZPos/2)
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, m_frontZPos);
                Vector3 move = new Vector3(transform.position.x, transform.position.y, m_frontZPos) - transform.position;
                m_controller.Move(move);

            }
            else
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, m_backZPos);
                //m_controller.transform.position = new Vector3(transform.position.x, transform.position.y, m_backZPos);
                Vector3 move = new Vector3(transform.position.x, transform.position.y, m_backZPos) - transform.position;
                m_controller.Move(move);

            }
            //m_controller.enabled = true;
        }
        //else
        //{
        m_moveVector = m_moveVector * m_speed * Time.deltaTime;
        //}

        //m_controller.Move(m_velocity);

        CollisionFlags flags = m_controller.Move(m_moveVector + m_velocity);
        bool headTouch = (flags & CollisionFlags.CollidedAbove) != 0;
        sideTouch = (flags & CollisionFlags.CollidedSides) != 0;
        bool floorTouch = (flags & CollisionFlags.CollidedBelow) != 0;
        //if (!CheckOnGround() && !sideTouch)
        //{
        //    m_wallTouch = false;
        //    m_playerAnim.SetBool("WallTouch", false);
        //}

        CheckHeadTouched(headTouch);
    }

    void JumpMotion()
    {
        if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().PlayOnce("Jump");
        PlayGOParticles(m_jumpParticles, true);
        m_velocity.y = -m_jumpSpeed * Physics.gravity.y * Time.deltaTime;
        m_wallTouch = false;
        m_playerAnim.SetBool("WallTouch", false);
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
            m_wallTouch = true;
            m_playerAnim.SetBool("WallTouch", m_wallTouch);

            if (!m_wallJumpingOnce && m_armourManager.IsArmCuffActive())
            {
                m_wallTouchTimer += Time.deltaTime;
                if (m_wallTouchTimer >= m_wallTouchTotal)
                {
                    m_playerAnim.SetBool("WallTouch", false);
                }
                else
                {
                    if (m_jumped && m_initJumpTimer >= m_initJumpTotal)
                    {
                        m_wallTouch = true;
                        m_velocity = (Physics.gravity.y * (m_wallTouchFallSpeed) * Vector3.up * Time.deltaTime);
                    }
                    else if (!m_jumped)
                    {
                        m_wallTouch = true;
                        m_velocity = (Physics.gravity.y * (m_wallTouchFallSpeed) * Vector3.up * Time.deltaTime);
                    }
                }
            }

            if (Input.GetAxis("Jump") != 0.0f && !m_wallJumpingOnce && !m_normalJumpPressed && !m_airJumpPressed && m_armourManager.IsArmCuffActive())
            {
                m_wallJumpingOnce = true;
                m_wallTouchTimer = 0.0f;
                m_wallTouch = false;
                m_jumped = false;

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
                m_playerAnim.SetBool("Pushing", false);

                JumpMotion();
            }

        }
        else
        {
            m_wallTouch = false;
            m_playerAnim.SetBool("WallTouch", false);

        }



        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null || rigidbody.isKinematic)
        {
            return;
        }

        if (hit.collider.tag == "Pushable" && sideTouch) 
        {
            m_playerAnim.SetBool("Pushing", true);
            if (m_armourManager.IsBootActive())
            {
                m_pushingObject = true;
                hit.collider.gameObject.GetComponent<PushableObject>().m_pushed = true;
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
                rigidbody.velocity = pushDir * m_pushForce / rigidbody.mass;
            }
                
            //Animation
        }
        else
        {
            m_pushingObject = false;
            m_playerAnim.SetBool("Pushing", false);
        }
    }
}
