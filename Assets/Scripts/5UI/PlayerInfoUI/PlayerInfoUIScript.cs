using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUIScript : MonoBehaviour
{
    private PlayerController Player { get { return PlayMgr.Player; } }      // �÷��̾� ��ũ��Ʈ

    public HPBarScript HPBar { get; private set; }                          // HP�� ��ũ��Ʈ
    public StaminaContScript StaminaCont { get; private set; }

    public void InitiateValues()                // HP�� �ʱ� ����
    {
        HPBar.SetmaxHP(Player.MaxHP);
        StaminaCont.SetMaxStamina(PlayerController.MaxStamina);
        SetCurHP(Player.CurHP);
    }
    public void SetMaxHP(int _max)
    {
        HPBar.SetmaxHP(_max);
    }
    public void SetCurHP(int _hp)         // �÷��̾� HP ���� �ø��� ����
    {
        HPBar.SetCurHP(_hp);
    }
    public void SetCurStamina(int _stamina)
    { 
        StaminaCont.SetCurStamina(_stamina); 
    }
    public void SetStaminaPer(float _per)
    {
        StaminaCont.SetStaminaPer(_per);
    }

    private void SetComps()
    {
        HPBar = GetComponentInChildren<HPBarScript>();
        StaminaCont = GetComponentInChildren<StaminaContScript>();
    }

    private void Awake()
    {
        SetComps();
    }
}
