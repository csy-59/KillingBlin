using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T _instance;
    public T Instance
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
                _instance =  go.AddComponent<T>();
            }

            return _instance;
        }
    }
}
