using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _isThereInstance = false;
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<T>();
            }

            if(_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<T>();
            }

            return _instance;
        }
    }

    public virtual void Start()
    {
        Init();
    }

    private void Init()
    {
        if(_isThereInstance)
        {
            Destroy(gameObject);
        }

        _isThereInstance = true;
        OnInit();
    }

    protected virtual void OnInit()
    {
        
    }
}
