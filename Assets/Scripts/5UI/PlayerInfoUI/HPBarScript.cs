using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    private Slider m_hpSlider;      // HP바 슬라이더
    private TextMeshProUGUI m_hpTxt;

    public void SetmaxHP(int _max) { m_hpSlider.maxValue = _max; }        // 최댓값 설정
    public void SetCurHP(int _hp) { m_hpSlider.value = _hp; m_hpTxt.text 
            = (_hp + " / " + m_hpSlider.maxValue); }             // 플레이어 HP 변경 시마다 실행




    private void Awake()
    {
        m_hpSlider = GetComponentInChildren<Slider>();
        m_hpTxt = GetComponentInChildren<TextMeshProUGUI>();
    }
}
