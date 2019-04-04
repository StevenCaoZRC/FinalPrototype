using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    Vector3 m_spawnPoint = Vector3.zero;

    bool m_helmetActive = false;
    bool m_chestActive = false;
    bool m_armCuffsActive = false;
    bool m_getaActive = false;
    bool m_brokenKatanaActive = false;
    bool m_CompleteKatanaActive = false;


    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            m_spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetArmourVariables(ArmourManager _manager)
    {
        m_helmetActive = _manager.IsHelmetActive();
        m_chestActive = _manager.IsChestActive();
        m_armCuffsActive = _manager.IsArmCuffActive();
        m_getaActive = _manager.IsBootActive();
        m_brokenKatanaActive = _manager.IsBrokenKatanaActive();
        Debug.Log("katana is active !!: " + m_brokenKatanaActive);

        //m_CompleteKatanaActive = _manager.IsCompleteKatanaActive();
    }

    public void SetUpArmourVariables(ArmourManager _manager)
    {
        _manager.ActivateHelmet(m_helmetActive);
        _manager.ActivateChest(m_chestActive);
        _manager.ActivateArmCuffs(m_armCuffsActive);
        _manager.ActivateBoots(m_getaActive);
        _manager.ActivateBrokenKatana(m_brokenKatanaActive);
        //_manager.ActivateCompleteKatana(m_CompleteKatanaActive);

        Debug.Log("katana: " + m_brokenKatanaActive + " geta: " + m_getaActive);
    }

    static public GameManager GetInstance()
    {
        return instance;
    }
    private void Update()
    {

    }

    static public bool GetAxisOnce(ref bool _pressedAlready, string _name)
    {
        bool current = Input.GetAxisRaw(_name) > 0;
        if (current && _pressedAlready)
        {
            return false;
        }

        _pressedAlready = current;

        return current;
    }
    
    public void SetSpawnPos(Vector3 _pos)
    {
        m_spawnPoint = _pos;
    }
    public Vector3 GetSpawnPos()
    {
        return m_spawnPoint;
    }
}
