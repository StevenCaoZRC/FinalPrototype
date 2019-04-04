using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public List<CheckPoint> m_checkPoints;
    // Start is called before the first frame update
    public void SetAllInactive()
    {
        if(m_checkPoints !=null)
        {
            foreach (CheckPoint c in m_checkPoints)
            {
                c.SetInactive();
            }
        }
        
    }
}
