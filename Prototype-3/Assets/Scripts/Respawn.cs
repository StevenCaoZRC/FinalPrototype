using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    ArmourManager m_armourManager;

    // Start is called before the first frame update
    void Start()
    {
        m_armourManager = GetComponent<ArmourManager>();

        GetComponent<CharacterController>().enabled = false;
        transform.position = GameManager.GetInstance().GetSpawnPos();

        GetComponent<CharacterController>().enabled = true;

        Debug.Log("Respawned");
    }

    // Update is called once per frame
    void Update()
    {
        

        Debug.Log("Broken katana : " + GetComponent<ArmourManager>().IsBrokenKatanaActive());
    }
}
