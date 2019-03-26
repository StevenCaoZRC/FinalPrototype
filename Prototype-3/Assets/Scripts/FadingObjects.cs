using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObjects : MonoBehaviour
{
    public Transform m_toBeFaded;
    private bool m_isTransparent = false;
    // Start is called before the first frame update
    void Start()
    {
        m_toBeFaded.GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!m_isTransparent)
            {
                foreach (Transform g in m_toBeFaded)
                {
                    if (g != null)
                    {
                        SetMaterialTransparent(g.gameObject);
                        iTween.FadeTo(g.gameObject, 0, 1);

                    }
                }
                m_isTransparent = true;
            }
            else if (m_isTransparent)
            {
                foreach (Transform g in m_toBeFaded)
                {
                    if (g != null)
                    {
                        iTween.FadeTo(g.gameObject, 1, 1);
                        Invoke("SetMaterialOpaque", 1.0f);

                    }
                }
                m_isTransparent = false;
            }
           
        }
    }
 
    private void SetMaterialTransparent(GameObject _setTransparent)
    { 
        foreach (Material m in _setTransparent.GetComponent<Renderer>().materials)
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
    private void SetMaterialOpaque(GameObject _setOpaque)
    {
        foreach (Material m in _setOpaque.GetComponent<Renderer>().materials)
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
