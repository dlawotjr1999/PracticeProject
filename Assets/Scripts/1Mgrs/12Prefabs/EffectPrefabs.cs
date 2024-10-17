using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerEffect
{
    HEALED,
    STAT_UP,
    BUFF,
    LAST
}

public enum EMonsterEffect
{
    DISAPPEAR,
    ARANGED,
    LAST
}

public enum ESkillEffect
{

    LAST
}

public enum EGimicEffect
{
    HEAL_AREA,

    LAST
}


public class EffectPrefabs : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_playerEffectPrefabs = new GameObject[(int)EPlayerEffect.LAST];
    [SerializeField]
    private GameObject[] m_monsterEffectPrefabs = new GameObject[(int)EMonsterEffect.LAST];
    [SerializeField]
    private GameObject[] m_skillEffectPrefabs = new GameObject[(int)ESkillEffect.LAST];
    [SerializeField]
    private GameObject[] m_gimicEffectPrefabs = new GameObject[(int)EGimicEffect.LAST];

    private GameObject GetPlayerEffect(EPlayerEffect _effect) { return m_playerEffectPrefabs[(int)_effect]; }
    private GameObject GetMonsterEffect(EMonsterEffect _effect) { return m_monsterEffectPrefabs[(int)_effect]; }
    private GameObject GetSkillEffect(ESkillEffect _effect) { return m_skillEffectPrefabs[(int)_effect]; }
    private GameObject GetGimicEffect(EGimicEffect _effect) { return m_gimicEffectPrefabs[(int)_effect]; }

    private void CreateEffect(GameObject _effect, Vector3 _pos) { Instantiate(_effect, _pos, Quaternion.identity); }
    private void CreateEffect(GameObject _effect, Transform _trans) { Instantiate(_effect, _trans); }

    public void CreatePlayerEffect(EPlayerEffect _effect, Vector3 _pos) { CreateEffect(GetPlayerEffect(_effect), _pos); }
    public void CreatePlayerEffect(EPlayerEffect _effect, Transform _trans) { CreateEffect(GetPlayerEffect(_effect), _trans); }
    public void CreateMonsterEffect(EMonsterEffect _effect, Vector3 _pos) { CreateEffect(GetMonsterEffect(_effect), _pos); }
    public void CreateSkillEffect(ESkillEffect _effect, Vector3 _pos) { CreateEffect(GetSkillEffect(_effect), _pos); }
    public void CreateGimicEffect(EGimicEffect _effect, Vector3 _pos) { CreateEffect(GetGimicEffect(_effect), _pos); }
}
