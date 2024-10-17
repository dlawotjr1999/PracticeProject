using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : IngameTrackUI
{
    private Slider m_slider;
    private TextMeshProUGUI m_hpTxt;
    private Image m_fillImg;

    private const float HPHeight = 2;

    private readonly Color ArangeCol = new Color(255f, 228f, 0f, 1);

    private int MaxHP { get; set; }

    public override void TrackPos(Vector3 _pos)
    {
        Vector3 hpPos = _pos + Vector3.up * HPHeight;
        base.TrackPos(hpPos);
    }

    public void InitHPBar(int _max)
    {
        m_slider.maxValue = _max;
        MaxHP = _max;
        SetHP(_max);
    }

    public void SetHP(int _hp)
    {
        m_slider.value = _hp;
        m_hpTxt.text = _hp+"/"+MaxHP;
    }

    public void ArangeOn()
    {
        m_fillImg.color = ArangeCol;
    }

    public override void Awake()
    {
        base.Awake();
        m_slider = GetComponent<Slider>();
        m_hpTxt = GetComponentInChildren<TextMeshProUGUI>();
        m_fillImg = GetComponentsInChildren<Image>()[1];
    }
}
