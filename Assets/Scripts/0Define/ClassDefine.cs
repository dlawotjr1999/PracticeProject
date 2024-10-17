using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst;

    public virtual void Awake()
    {
        if (Inst != null) { Destroy(gameObject); }

        Inst = GetComponent<T>();
        if(Inst == null)
            Inst = gameObject.AddComponent<T>();

        DontDestroyOnLoad(gameObject);
    }
}