using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCanvasScript : MonoBehaviour
{
    private void Awake()
    {
        PlayMgr.SetControlCanvas(this);
    }
}
