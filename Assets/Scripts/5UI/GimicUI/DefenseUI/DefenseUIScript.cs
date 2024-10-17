using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenseUIScript : MonoBehaviour
{
    private Slider m_hpSlider;                  // HP바
    private TextMeshProUGUI m_hpTxt;            // HP 텍스트

    private int MaxHP { get; set; }             // 최대 체력

    public void InitHP(int _maxHP)              // HP 초기 설정
    {
        MaxHP = _maxHP;
        m_hpSlider.maxValue = MaxHP;
        SetHP(MaxHP);
    }

    public void SetHP(int _hp)                  // HP 변경
    {
        m_hpSlider.value = _hp;
        m_hpTxt.text = _hp + "/" + MaxHP;
    }

    private void Awake()
    {
        m_hpSlider = GetComponentInChildren<Slider>();
        m_hpTxt = GetComponentInChildren<TextMeshProUGUI>();
    }
}
