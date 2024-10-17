using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSkill : PlayerSkill
{
    [SerializeField]
    protected GameObject m_hitEffect;

    private float m_moveSpeed;                  // �̵� �ӵ�
    private float m_range;                      // �ִ� ���� ���� (�ϴ� �̵� ���������θ� ��� ���� ������ ����)
    private float m_creationHeight;             // ���� ����
    private float m_maxHeight;                  // �ִ� ���� ���� (�Ÿ��� ª���� ������)

    private Vector2 m_lauchPos;                 // �߻�Ǵ� ����
    private Vector2 m_targetPos;                // ��ǥ ����

    private float Slope { get; set; }           // ������ ����
    private float MidX { get; set; }            // ������ �߰���
    private Vector2 CurPos { get { return new(transform.position.x, transform.position.z); } }  // ���� ��ġ
    private float CurX { get; set; }            // ���� ��ġ�� ���� ����� x��
    private float CurY { get; set; }            // ������ �󿡼� x���� ���� y��

    public override void SetTarget(Vector3 _target)
    {
        m_targetPos = new(_target.x, _target.z);
    }

    public override void SetSkillInfo(SkillInfo _info)      // ��ų �⺻ ���� ����
    {
        ThrowSkillInfo info = (ThrowSkillInfo)_info;
        base.SetSkillInfo(info);
        m_moveSpeed = info.Speed;
        m_range = info.Range;
        m_creationHeight = info.CreationHeight;
        m_maxHeight = info.MaxHeight;

        m_lauchPos = CurPos;
        SetParams();
        SetYPos();
    }

    public override void SetAim(Vector3 _aim)   // ������ ����
    {
        base.SetAim(_aim);
        float yRotation = FunctionDefine.VecToDeg(new(_aim.x, _aim.z));
        transform.eulerAngles = new(transform.eulerAngles.x, yRotation, 0);
    }

    private void SetParams()                    // ������ ������ ���� �Ķ���� ����
    {
        float dist = (m_targetPos - m_lauchPos).magnitude;
        if(dist < m_range)
            m_maxHeight *= ((dist / m_range) * (dist / m_range));
        if (m_maxHeight < m_creationHeight)
            m_maxHeight = m_creationHeight + 0.1f;
        MidX = (dist / (1 + Mathf.Sqrt(m_maxHeight/(m_maxHeight - m_creationHeight))));
        Slope = (m_maxHeight - m_creationHeight) / (MidX * MidX);
    }

    private void SetYPos()      // ��ġ �̵� ��� ��ȭ�� x ���� ������ ���Ŀ� �����Ͽ� y �� ����
    {
        CurX = (CurPos - m_lauchPos).magnitude;
        CurY = -Slope * (CurX - MidX) * (CurX - MidX) + m_maxHeight;
        transform.position = new(CurPos.x, CurY, CurPos.y);
        SetRotation();
    }
    private void SetRotation()  // ���� ���ӵ��� ���� �� ���� ����
    {
        float curSlope = 2 * Slope * (MidX - CurX);
        float angle = -Mathf.Atan(curSlope) * Mathf.Rad2Deg;
        transform.eulerAngles = new(angle, transform.eulerAngles.y, transform.eulerAngles.z);
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
        Destroy(gameObject);        // �ӽ� ���� �Լ�
    }

    private void FixedUpdate()
    {
        transform.Translate(m_moveSpeed*Time.fixedDeltaTime*Vector3.forward);       // ���� �̵�
        SetYPos();                                                                  // ���� �̵��� ���� ������ �˵� ����
        if (transform.position.y <= 0)
        {
            DestroySkill();
        }
    }
}
