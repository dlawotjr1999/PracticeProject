using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaStateScript : MonoBehaviour
{
    private RectTransform m_rect;
    private Image m_img;

    private const float DefaultSize = 36;

    private Color FullColor = new(41 / 255f, 222/ 255f, 224 / 255f, 1);
    private Color EmptyColor = new(224 / 255f, 86 / 255f, 41 / 255f, 1);

    private float RestoreTime { get { return PlayerController.m_staminaRestoreTime; } }

    public void FullStamina()
    {
        SetStaminaPer(RestoreTime);
        m_img.color = FullColor;
    }

    public void EmptyStamina()
    {
        SetStaminaPer(0);
        m_img.color = EmptyColor;
    }

    public void SetStaminaPer(float _per)      // 스태미나 백분율 표시
    {
        float height = DefaultSize * (_per / RestoreTime);
        m_rect.sizeDelta = new(DefaultSize, height);
    }

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        m_img = GetComponent<Image>();
    }
}
