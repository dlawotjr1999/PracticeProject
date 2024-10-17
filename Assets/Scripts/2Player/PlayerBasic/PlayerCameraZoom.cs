using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;
    float cameraDistance;
    [SerializeField] float sensitivity = 10f;

    private void Update()
    {
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body); 
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
            if(virtualCamera.m_Lens.FieldOfView < 60) { // FOV가 너무 커지지 않게 하는 조건식 구현해야 함
                {
                    cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                    if (componentBase is CinemachineFramingTransposer)
                    {
                        (componentBase as CinemachineFramingTransposer).m_CameraDistance -= cameraDistance;
                    }
                }
        }
    }
}
