using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Camera m_camera;





    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Start()
    {
        PlayMgr.SetMainCamera(m_camera);
    }
}
