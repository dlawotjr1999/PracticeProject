using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasScript : MonoBehaviour
{
    private void Awake()
    {
        PlayMgr.SetMainCanvas(this);
    }
}
