using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenseUIScript : MonoBehaviour
{
    private Slider m_hpSlider;                  // HP��
    private TextMeshProUGUI m_hpTxt;            // HP �ؽ�Ʈ

    private int MaxHP { get; set; }             // �ִ� ü��

    public void InitHP(int _maxHP)              // HP �ʱ� ����
    {
        MaxHP = _maxHP;
        m_hpSlider.maxValue = MaxHP;
        SetHP(MaxHP);
    }

    public void SetHP(int _hp)                  // HP ����
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
