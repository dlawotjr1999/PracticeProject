using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementUIScript : MonoBehaviour
{
    private Slider m_hpSlider;
    private TextMeshProUGUI m_hpTxt;

    private int MaxHP { get; set; }


    public void InitUI(EGimicMaterial _mat, int _max)
    {
        MaxHP = _max;
        m_hpSlider.maxValue = _max;
        SetHP(MaxHP);
    }

    public void SetHP(int _hp)
    {
        m_hpTxt.text = _hp + "/" + MaxHP;
        m_hpSlider.value = _hp;
    }


    private void Awake()
    {
        m_hpSlider = GetComponentInChildren<Slider>();
        m_hpTxt = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }
}
