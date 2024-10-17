using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRoom : SpawnRoom
{
    [SerializeField]
    private GameObject m_hpUIPrefab;

    private ElementObjScript m_elementObj;
    private ElementUIScript m_hpUI;

    private EGimicMaterial CurMat { get; set; }

    private const float ShowHPDelay = 5f;

    private const string EnterInfo =
        "전에 못 보던 물체가 있다. 위험해 보이는데 내가 파괴할 수 있을까?";
    private const string DestInfo =
        "이런 게 숨겨져 있었다니, 무슨 일이 일어나고 있는 거지?";

    public override void EnterRoom()
    {
        base.EnterRoom();
        SetGimicMaterial();
        CreateRoomInfoUI(EnterInfo);
        StartCoroutine(ShowGimicHP());
    }

    public override void ExitRoom()
    {
        if(m_hpUI != null)
        {
            Destroy(m_hpUI.gameObject); 
            m_hpUI = null;
        }
    }

    private void SetGimicMaterial()
    {
        CurMat = (EGimicMaterial)Random.Range(0, (int)EGimicMaterial.LAST);
        m_elementObj.SetMaterial(CurMat);
    }

    private IEnumerator ShowGimicHP()
    {
        yield return new WaitForSeconds(ShowHPDelay);
        GameObject ui = Instantiate(m_hpUIPrefab, PlayMgr.MainCanvasTrans);
        m_hpUI = ui.GetComponent<ElementUIScript>();
        m_hpUI.InitUI(CurMat, m_elementObj.MaxHP);
    }

    public void SetHP(int _hp)
    {
        if (m_hpUI != null)
            m_hpUI.SetHP(_hp);
    }

    public void DestObj()
    {
        Destroy(m_hpUI.gameObject);
        m_hpUI = null;
        CreateRoomInfoUI(DestInfo);
    }




    public override void Awake()
    {
        base.Awake();
        m_elementObj = GetComponentInChildren<ElementObjScript>();
        m_elementObj.SetParent(this);
    }
}
