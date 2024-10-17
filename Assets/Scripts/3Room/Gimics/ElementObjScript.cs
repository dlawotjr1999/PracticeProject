using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGimicMaterial
{
    ICE,
    IRON,
    LAST
}

public class ElementObjScript : MonoBehaviour
{
    private ElementRoom m_parent;
    public void SetParent(ElementRoom _parent) { m_parent = _parent; }

    [SerializeField]
    private EGimicMaterial m_material;

    [SerializeField]
    private GameObject m_rewardPrefab;

    public readonly int MaxHP = 100;
    private int CurHP { get; set; }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag(ValueDefine.PlayerSkillTag))
        {
            PlayerSkill skill = _other.GetComponent<PlayerSkill>();
            ChkElement(skill);
        }
    }

    public void SetMaterial(EGimicMaterial _mat)
    {
        m_material = _mat;
    }

    private void ChkElement(PlayerSkill _skill)
    {
        if (m_material == EGimicMaterial.ICE && _skill.Element == ESkillElement.FIRE
            || m_material == EGimicMaterial.IRON && _skill.Element == ESkillElement.ELECTRIC)
        {
            float damage = _skill.Damage;
            GetDamage(damage);
        }
    }

    private void GetDamage(float _damage)
    {
        CurHP -= (int)_damage;
        if(CurHP <= 0)
        {
            CurHP = 0;
            DestObj();
        }
        m_parent.SetHP(CurHP);
    }

    private void DestObj()
    {
        m_parent.DestObj();
        Instantiate(m_rewardPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        CurHP = MaxHP;
    }
}
