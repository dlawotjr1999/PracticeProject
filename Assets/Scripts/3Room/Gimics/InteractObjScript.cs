using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjScript : MonoBehaviour
{
    private GameObject m_interactUI;

    [SerializeField]
    protected float NearDist = 4;
    [SerializeField]
    protected float UIHeight = 2;
    
    protected float DistToPlayer { get { return (PlayMgr.PlayerPos - transform.position).magnitude; } }
    protected bool NearPlayer { get { return DistToPlayer <= NearDist; } }
    protected bool Interacting { get; set; }

    protected void ShowUI() { m_interactUI.SetActive(true); }
    protected void HideUI() { m_interactUI.SetActive(false); }

    private void CreateUI()
    {
        m_interactUI = Instantiate(PlayMgr.InteractUIPrefab, PlayMgr.IngameCanvasTrans);
    }

    private void SetUIPos()
    {

    }

    public virtual void StartInteract()
    {
        Interacting = true;
    }

    private void Awake()
    {
        CreateUI();
    }

    private void Start()
    {
        HideUI();   
    }

    private void Update()
    {
        if(!Interacting && Input.GetKeyDown(ValueDefine.InteractKey))
        {
            StartInteract();
        }

        if (NearPlayer && !m_interactUI.activeSelf)
        {
            ShowUI();
        }
        else if (NearPlayer && m_interactUI.activeSelf)
        {
            SetUIPos();
        }
        else if (!NearPlayer && m_interactUI.activeSelf)
        {
            HideUI();
        }
    }
}
