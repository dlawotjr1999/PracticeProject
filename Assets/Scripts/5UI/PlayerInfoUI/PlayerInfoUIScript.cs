using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUIScript : MonoBehaviour
{
    private PlayerController Player { get { return PlayMgr.Player; } }      // 플레이어 스크립트

    public HPBarScript HPBar { get; private set; }                          // HP바 스크립트
    public StaminaContScript StaminaCont { get; private set; }

    public void InitiateValues()                // HP바 초기 세팅
    {
        HPBar.SetmaxHP(Player.MaxHP);
        StaminaCont.SetMaxStamina(PlayerController.MaxStamina);
        SetCurHP(Player.CurHP);
    }
    public void SetMaxHP(int _max)
    {
        HPBar.SetmaxHP(_max);
    }
    public void SetCurHP(int _hp)         // 플레이어 HP 변경 시마다 실행
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
