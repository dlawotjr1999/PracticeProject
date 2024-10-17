using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMgr : MonoBehaviour               // 플레이어의 마우스 조준점 관리자
{
    [SerializeField]
    private GameObject m_throwMarker;
    [SerializeField]
    private GameObject m_aroundMarker;
    [SerializeField]
    private GameObject m_sectorMarker;

    private LineRenderer m_lineRenderer;
    [Range(0, 80)] private int lineSpotCnt = 70;        // 라인 그릴 포인트 개수

    public void AimOn(ESkillName _skill)
    {
        AimOff();
        if (_skill < ESkillName.THROW_NONE) { Debug.LogError("잘못된 에임 호출"); return; }
        if (_skill < ESkillName.AROUND_NONE)
            ThrowAimOn(_skill);
        else if (_skill < ESkillName.SECTOR_FIRE)
            AroundAimOn(_skill);
        else if (_skill < ESkillName.SUMMON)
            SectorAimOn(_skill);
    }

    public void AimOff() { m_throwMarker.SetActive(false); m_aroundMarker.SetActive(false); m_sectorMarker.SetActive(false); StopShowLine(); }   // 조준점 off

    private void ThrowAimOn(ESkillName _skill) { m_throwMarker.SetActive(true); ShowThrowLine(_skill); }
    private void AroundAimOn(ESkillName _skill) { m_aroundMarker.SetActive(true); m_aroundMarker.GetComponent<RangeMarker>().SetRadius(_skill); }
    private void SectorAimOn(ESkillName _skill) { m_sectorMarker.SetActive(true); m_sectorMarker.GetComponent<RangeMarker>().SetRadius(_skill); }

    private void TrackTarget()                      // 마우스 따라가기
    {
        Vector3 target = PlayMgr.PlayerTarget;
        transform.position = target;
    }

    private void ShowThrowLine(ESkillName _skill)
    {
        SkillInfo info = SkillValues.GetSkillInfo(_skill);
        ThrowSkillInfo throwInfo = (ThrowSkillInfo)info;
        m_lineRenderer.widthMultiplier = 0.3f;
        StartCoroutine(DrawLine(throwInfo));
    }
    public void StopShowLine()
    {
        m_lineRenderer.widthMultiplier = 0;
        StopAllCoroutines();
    }
    private IEnumerator DrawLine(ThrowSkillInfo _info)
    {
        while (true)
        {
            m_lineRenderer.positionCount = 0;
            m_lineRenderer.positionCount = lineSpotCnt;

            Vector3 playerPos = PlayMgr.PlayerPos;
            Vector3 targetPos = PlayMgr.PlayerTarget;
            Vector3 targetDir = (targetPos - playerPos).normalized;
            Vector3 startPos = playerPos + targetDir * _info.Offset + Vector3.up * _info.CreationHeight;

            float dist = (targetPos - startPos).magnitude;
            float range = _info.Range;
            float maxHeight = _info.MaxHeight;
            float creationHeight = _info.CreationHeight;

            if (dist < range)
                maxHeight *= ((dist / range) * (dist / range));
            if (maxHeight < creationHeight)
                maxHeight = creationHeight + 0.1f;
            float midX = (dist / (1 + Mathf.Sqrt(maxHeight/(maxHeight - creationHeight))));
            float slope = (maxHeight - creationHeight) / (midX * midX);

            float curX; 
            float curY;
            for (int i = 0; i<lineSpotCnt; i++)
            {
                curX = i * (dist / lineSpotCnt);
                curY = -slope * (curX - midX) * (curX - midX) + maxHeight - creationHeight;
                Vector3 spotPos = startPos + targetDir * curX + Vector3.up * curY;
                m_lineRenderer.SetPosition(i, spotPos);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        AimOff();
    }

    private void Update()
    {
        TrackTarget();
    }
}
