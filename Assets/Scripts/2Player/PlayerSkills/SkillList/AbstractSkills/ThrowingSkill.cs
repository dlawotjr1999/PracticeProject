using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSkill : PlayerSkill
{
    [SerializeField]
    protected GameObject m_hitEffect;

    private float m_moveSpeed;                  // 이동 속도
    private float m_range;                      // 최대 조준 범위 (일단 이동 기준점으로만 삼고 아직 제한은 없음)
    private float m_creationHeight;             // 생성 높이
    private float m_maxHeight;                  // 최대 도달 높이 (거리가 짧으면 낮아짐)

    private Vector2 m_lauchPos;                 // 발사되는 지점
    private Vector2 m_targetPos;                // 목표 지점

    private float Slope { get; set; }           // 포물선 기울기
    private float MidX { get; set; }            // 포물선 중간점
    private Vector2 CurPos { get { return new(transform.position.x, transform.position.z); } }  // 현재 위치
    private float CurX { get; set; }            // 현재 위치에 따른 상대적 x값
    private float CurY { get; set; }            // 포물선 상에서 x값에 따른 y값

    public override void SetTarget(Vector3 _target)
    {
        m_targetPos = new(_target.x, _target.z);
    }

    public override void SetSkillInfo(SkillInfo _info)      // 스킬 기본 정보 설정
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

    public override void SetAim(Vector3 _aim)   // 조준점 설정
    {
        base.SetAim(_aim);
        float yRotation = FunctionDefine.VecToDeg(new(_aim.x, _aim.z));
        transform.eulerAngles = new(transform.eulerAngles.x, yRotation, 0);
    }

    private void SetParams()                    // 포물선 공식을 위한 파라미터 설정
    {
        float dist = (m_targetPos - m_lauchPos).magnitude;
        if(dist < m_range)
            m_maxHeight *= ((dist / m_range) * (dist / m_range));
        if (m_maxHeight < m_creationHeight)
            m_maxHeight = m_creationHeight + 0.1f;
        MidX = (dist / (1 + Mathf.Sqrt(m_maxHeight/(m_maxHeight - m_creationHeight))));
        Slope = (m_maxHeight - m_creationHeight) / (MidX * MidX);
    }

    private void SetYPos()      // 위치 이동 결과 변화한 x 값을 포물선 공식에 대입하여 y 값 도출
    {
        CurX = (CurPos - m_lauchPos).magnitude;
        CurY = -Slope * (CurX - MidX) * (CurX - MidX) + m_maxHeight;
        transform.position = new(CurPos.x, CurY, CurPos.y);
        SetRotation();
    }
    private void SetRotation()  // 현재 가속도에 따라 모델 기울기 변경
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
        Destroy(gameObject);        // 임시 삭제 함수
    }

    private void FixedUpdate()
    {
        transform.Translate(m_moveSpeed*Time.fixedDeltaTime*Vector3.forward);       // 직선 이동
        SetYPos();                                                                  // 직선 이동에 따른 포물선 궤도 조정
        if (transform.position.y <= 0)
        {
            DestroySkill();
        }
    }
}
