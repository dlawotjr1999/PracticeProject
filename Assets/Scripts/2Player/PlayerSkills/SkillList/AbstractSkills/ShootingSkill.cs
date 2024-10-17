using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : PlayerSkill
{
    [SerializeField]
    protected GameObject m_hitEffect;

    private float m_moveSpeed;              // 이동 속도
    private float m_shootingRange;          // 최대 도달 가능 범위
        
    public override void SetSkillInfo(SkillInfo _info)
    {
        ShootingSkillInfo info = (ShootingSkillInfo)_info;
        base.SetSkillInfo(info);
        m_moveSpeed = info.Speed;
        m_shootingRange = info.Range;
    }

    public override void SetAim(Vector3 _aim)
    {
        base.SetAim(_aim);
        float yRotation = FunctionDefine.VecToDeg(new Vector2(_aim.x, _aim.z));
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }

    private void OnTriggerEnter(Collider _other)
    {
        switch (_other.tag)
        {
            case ValueDefine.WallTag:
            case ValueDefine.MonsterTag:
            case ValueDefine.DefenseObjTag:
            case ValueDefine.CollideObjTag:
                DestroySkill();
                break;
        }
    }

    public virtual void DestroySkill()
    {
        Instantiate(m_hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Start()
    {
        float time = m_shootingRange / m_moveSpeed;
        Destroy(gameObject, time);
    }

    private void FixedUpdate()
    {
        transform.Translate(m_moveSpeed*Time.fixedDeltaTime*Vector3.forward);
    }
}
