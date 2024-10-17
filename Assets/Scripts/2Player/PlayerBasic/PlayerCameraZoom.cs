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
            if(virtualCamera.m_Lens.FieldOfView < 60) { // FOV�� �ʹ� Ŀ���� �ʰ� �ϴ� ���ǽ� �����ؾ� ��
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
