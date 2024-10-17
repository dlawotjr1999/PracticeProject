using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    [SerializeField]
    private float m_lastTime;

    private void Start()
    {
        Destroy(gameObject, m_lastTime);
    }
}
