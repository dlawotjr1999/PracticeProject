using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaContScript : MonoBehaviour
{
    private StaminaStateScript[] m_staminas;

    private int m_maxStamina = 5;
    private int RestoringIdx { get; set; }

    public void SetMaxStamina(int _max)         // �ִ� ����
    {
        m_maxStamina = _max;
    }

    public void SetStaminaPer(float _per)
    {
        m_staminas[RestoringIdx].SetStaminaPer(_per);
    }

    public void SetCurStamina(int _stamina)     // ���¹̳� ���� �ø��� ȣ��
    {
        for (int i = 0; i<_stamina; i++) 
        {
            m_staminas[i].FullStamina();
        }
        for (int i = _stamina; i<m_maxStamina; i++)
        {
            m_staminas[i].EmptyStamina();
        }
        RestoringIdx = _stamina;
    }

    private void Awake()
    {
        m_staminas = GetComponentsInChildren<StaminaStateScript>();
    }
}
