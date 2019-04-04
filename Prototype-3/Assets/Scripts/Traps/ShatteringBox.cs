using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteringBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayOnce("BreakBox");
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
