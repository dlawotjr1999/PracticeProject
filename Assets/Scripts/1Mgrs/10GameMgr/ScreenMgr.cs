using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMgr : MonoBehaviour
{
    private const int CanvusWidth = 1920;
    private const int CanvusHeight = 1080;

    private void SetResolution()
    {
        Screen.SetResolution(CanvusWidth, CanvusHeight, true);
    }




    private void Awake()
    {
        SetResolution();
    }
}
