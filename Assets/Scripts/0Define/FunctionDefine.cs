using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void FPointer();
public delegate void EventPointer(PointerEventData _data);

// 게임 내 공통적으로 쓰이는 함수
public static class FunctionDefine
{
    public static void AddEvent(EventTrigger _trigger, EventTriggerType _type, EventPointer _function)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = _type;
        entry.callback.AddListener(data => { _function((PointerEventData)data); });
        _trigger.triggers.Add(entry);
    }

    public static float VecToDeg(Vector2 _vec)
    {
        float degree = 90 - Mathf.Atan2(_vec.y, _vec.x) * Mathf.Rad2Deg;
        return degree;
    }

    public static bool MouseOnUI { get { return EventSystem.current.IsPointerOverGameObject(); } }

    public static void SetTransparent(Material _mat)
    {
        _mat.SetFloat("_Surface", 1f);
        _mat.SetFloat("_Blend", 0f);

        _mat.SetOverrideTag("RenderType", "Transparent");
        _mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _mat.SetInt("_ZWrite", 0);
        _mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        _mat.SetShaderPassEnabled("ShadowCaster", false);
    }
    public static void SetObaque(Material _mat)
    {
        _mat.SetFloat("_Surface", 0f);

        _mat.SetOverrideTag("RenderType", "");
        _mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _mat.SetInt("_ZWrite", 1);
        _mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _mat.renderQueue = -1;
        _mat.SetShaderPassEnabled("ShadowCaster", true);
    }
    
}
