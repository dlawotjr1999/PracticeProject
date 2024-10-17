using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    private Camera m_mainCam;
    public void SetMainCamera(Camera _camera) { m_mainCam = _camera; }


    private void ChkOverlap()
    {
        Vector3 camPos = m_mainCam.transform.position;
        Vector3 playerPos = PlayMgr.PlayerPos;
        Vector3 dir = (playerPos - camPos).normalized;

        RaycastHit[] hits = Physics.RaycastAll(camPos, dir, Mathf.Infinity, ValueDefine.HideWallLayerIdx);

        foreach (RaycastHit hit in hits)
        {
            HideWallScript hide = hit.transform.GetComponent<HideWallScript>();
            if (hide != null)
            {
                hide.HideWall();
            }
        }
    }




    private void LateUpdate()
    {
        ChkOverlap();
    }
}
