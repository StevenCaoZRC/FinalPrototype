using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourManager : MonoBehaviour
{
    public GameObject m_helmet;
    public GameObject m_hair;
    public GameObject m_chest;
    public GameObject m_leftArmCuffs;
    public GameObject m_rightArmCuffs;
    public GameObject m_leftBoots;
    public GameObject m_rightBoots;
    public GameObject m_brokenKatana;
    public GameObject m_completeKatana;

    // Start is called before the first frame update
    void Start()
    {
        if(m_helmet != null) m_helmet.SetActive(false);
        if (m_hair != null) m_hair.SetActive(true);
        if (m_chest != null) m_chest.SetActive(false);
        if (m_leftArmCuffs != null) m_leftArmCuffs.SetActive(false);
        if (m_rightArmCuffs != null) m_rightArmCuffs.SetActive(false);
        if (m_leftBoots != null) m_leftBoots.SetActive(false);
        if (m_rightBoots != null) m_rightBoots.SetActive(false);
        if (m_brokenKatana != null) m_brokenKatana.SetActive(false);
        if (m_completeKatana != null) m_completeKatana.SetActive(false);
    }

    public void ActivateHelmet(bool _activate)
    {
        m_hair.SetActive(false);
        m_helmet.SetActive(_activate);
    }

    public bool IsHelmetActive()
    {
        return m_helmet.activeSelf;
    }

    public void ActivateChest(bool _activate)
    {
        m_chest.SetActive(_activate);
    }

    public bool IsChestActive()
    {
        return m_chest.activeSelf;
    }

    public void ActivateArmCuffs(bool _activate)
    {
        m_leftArmCuffs.SetActive(_activate);
        m_rightArmCuffs.SetActive(_activate);
    }

    public bool IsArmCuffActive()
    {
        return m_leftArmCuffs.activeSelf;
    }

    public void ActivateBoots(bool _activate)
    {
        m_leftBoots.SetActive(_activate);
        m_rightBoots.SetActive(_activate);
    }

    public bool IsBootActive()
    {
        return m_leftBoots.activeSelf;
    }

    public void ActivateBrokenKatana(bool _activate)
    {
        if (_activate)
        {
            m_completeKatana.SetActive(false);
            m_brokenKatana.SetActive(true);
        }
        else
        {
            m_completeKatana.SetActive(true);
            m_brokenKatana.SetActive(false);
        }
    }
     
    public bool IsBrokenKatanaActive()
    {
        return m_brokenKatana.activeSelf;
    }

    public void ActivateCompleteKatana(bool _activate)
    {
        if(_activate)
        {
            m_completeKatana.SetActive(true);
            m_brokenKatana.SetActive(false);
        }
        else
        {
            m_completeKatana.SetActive(false);
            m_brokenKatana.SetActive(true);
        }
    }

    public bool IsCompleteKatanaActive()
    {
        return m_completeKatana.activeSelf;
    }
}
