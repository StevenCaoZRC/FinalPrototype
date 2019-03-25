using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObjects : MonoBehaviour
{
    public List<GameObject> m_toBeFaded;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject g in m_toBeFaded)
            {
                if (g != null)
                {
                    SetMaterialTransparent();
                    iTween.FadeTo(g, 0, 1);
                }
            }
          
           
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           
            foreach (GameObject g in m_toBeFaded)
            {
                if (g != null)
                {
                    iTween.FadeTo(g, 1, 1);
                    Invoke("SetMaterialOpaque", 1.0f);
                }
            }
        }
    }

    private void SetMaterialTransparent()

    {
        foreach (GameObject g in m_toBeFaded)
        {
             
                foreach (Material m in g.GetComponent<Renderer>().materials)
                {
                    m.SetFloat("_Mode", 2);

                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    m.SetInt("_ZWrite", 0);

                    m.DisableKeyword("_ALPHATEST_ON");

                    m.EnableKeyword("_ALPHABLEND_ON");

                    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                    m.renderQueue = 3000;

                }
           
        }
    }
    private void SetMaterialOpaque()
    {
        foreach (GameObject g in m_toBeFaded)
        {
            
                foreach (Material m in g.GetComponent<Renderer>().materials)

                {

                    m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    m.SetInt("_ZWrite", 1);

                    m.DisableKeyword("_ALPHATEST_ON");

                    m.DisableKeyword("_ALPHABLEND_ON");

                    m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                    m.renderQueue = -1;
                }
            
            
        }
    }
    
}
