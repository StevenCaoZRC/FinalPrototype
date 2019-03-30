using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    PlayerAttack m_playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        m_playerAttack = transform.parent.GetComponentInChildren<PlayerAttack>();
    }

    // Update is called once per frame
    void Slash()
    {
        m_playerAttack.Slash();
    }
}
