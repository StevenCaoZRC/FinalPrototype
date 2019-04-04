using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourUI : MonoBehaviour
{
    ArmourManager m_armourManager;
    public GameObject Katana;
    public GameObject Chest;
    public GameObject Arms;
    public GameObject Boots;
    public GameObject Helmet;
    // Start is called before the first frame update
    void Start()
    {
        m_armourManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ArmourManager>();
        Helmet.SetActive(false);
        Katana.SetActive(false);
        Chest.SetActive(false);
        Arms.SetActive(false);
        Boots.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_armourManager.IsHelmetActive())
        {
            Helmet.SetActive(true);
        }
        if (m_armourManager.IsBrokenKatanaActive())
        {
            Katana.SetActive(true);
        }
        if (m_armourManager.IsChestActive())
        {
            Chest.SetActive(true);
        }
        if (m_armourManager.IsArmCuffActive())
        {
            Arms.SetActive(true);
        }
        if (m_armourManager.IsBootActive())
        {
            Boots.SetActive(true);
        }

    }
}
