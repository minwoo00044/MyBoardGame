using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonBaseMB<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject newInstance = new GameObject(typeof(T).Name);
                    _instance = newInstance.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    void Start()
    {
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(this);
    }
}
