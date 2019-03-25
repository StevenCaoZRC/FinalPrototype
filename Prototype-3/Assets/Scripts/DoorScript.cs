using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool m_atDoorEnd = false;
    public bool m_setTransparent = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //if()

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(m_atDoorEnd)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerMovement>().SetPlayerAtDoor(true);
               
                
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerMovement>().SetPlayerAtDoorPath(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_atDoorEnd)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerMovement>().SetPlayerAtDoor(false);
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerMovement>().SetPlayerAtDoorPath(false);
            }
        }
    }
}
