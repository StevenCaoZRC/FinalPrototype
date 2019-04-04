using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]CheckPoint m_lastCheckpoint;
    [SerializeField] Vector3 m_spawnPoint;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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

    public void SetCheckPoint(CheckPoint _cp)
    {
        m_lastCheckpoint = _cp;
    }

    public CheckPoint GetCheckPoint()
    {
        if (m_lastCheckpoint != null)
            Debug.Log(m_lastCheckpoint.gameObject.name);
        return m_lastCheckpoint;
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
