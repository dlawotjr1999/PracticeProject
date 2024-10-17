using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    private Slider m_hpSlider;      // HP�� �����̴�
    private TextMeshProUGUI m_hpTxt;

    public void SetmaxHP(int _max) { m_hpSlider.maxValue = _max; }        // �ִ� ����
    public void SetCurHP(int _hp) { m_hpSlider.value = _hp; m_hpTxt.text 
            = (_hp + " / " + m_hpSlider.maxValue); }             // �÷��̾� HP ���� �ø��� ����




    private void Awake()
    {
        m_hpSlider = GetComponentInChildren<Slider>();
        m_hpTxt = GetComponentInChildren<TextMeshProUGUI>();
    }
}
