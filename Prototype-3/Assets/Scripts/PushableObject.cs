﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    Rigidbody m_rigidBody;
    Vector3 m_vel;
    public float m_fallSpeed = 1.0f;
    float m_distToGround = 0.0f;
    public bool m_pushed = true;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void FixedUpdate()
    {
        //m_vel.y += Physics.gravity.y * 0.1f * Time.deltaTime;
        if(CheckGrounded())
        {
            transform.rotation = new Quaternion();
            m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
            //m_rigidBody.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("not on ground");
            m_rigidBody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            m_rigidBody.velocity += Physics.gravity.y * (m_fallSpeed) * Vector3.up * Time.deltaTime;
        }


    }

    public bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, m_distToGround+0.1f);
    }

    public void SetPushed(bool _pushed)
    {
        m_pushed = _pushed;
    }
}
