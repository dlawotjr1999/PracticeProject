using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameCanvasScript : MonoBehaviour
{
    private void Awake()
    {
        PlayMgr.SetIngameCanvas(this);
    }
}
