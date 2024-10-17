using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWallScript : MonoBehaviour
{
    private MeshRenderer[] m_meshRenderers;

    private const float HideAlpha = 0.25f;
    private const float MaxTimer = 0.5f;

    private const float HideSpeed = 2;
    private const float ShowSpeed = 5;

    public bool Hiding { get; private set; }
    private bool Reseting { get; set; }
    private float HideCnt { get; set; }

    private Coroutine m_hideCoroutine;
    private Coroutine m_showCoroutine;
    private Coroutine m_timerCoroutine;


    public void HideWall()
    {
        if (Hiding)
        {
            HideCnt = 0;
            return;
        }

        if (m_showCoroutine != null && Reseting)
        {
            Reseting = false;
            StopCoroutine(m_showCoroutine);
        }

        foreach (MeshRenderer mr in m_meshRenderers)
        {
            AbleHide(mr);
        }

        Hiding = true;
        m_hideCoroutine = StartCoroutine(HideCoroutine());
    }

    private void ShowWall()
    {
        Hiding = false;
        m_showCoroutine = StartCoroutine(ShowCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        while (true)
        {
            bool hideDone = true;

            foreach (MeshRenderer mr in m_meshRenderers)
            {
                foreach (Material mat in mr.materials)
                {
                    if (mat.color.a > HideAlpha)
                        hideDone = false;

                    Color col = mat.color;
                    col.a -= Time.deltaTime * HideSpeed;
                    if (col.a < 0)
                        col.a = 0;
                    mat.color = col;
                }
            }

            if (hideDone)
            {
                ChkTimer();
                break;
            }

            yield return new WaitForSeconds(0.001f);
        }
    }
    private IEnumerator ShowCoroutine()
    {
        while (true)
        {
            bool showDone = true;

            foreach (MeshRenderer mr in m_meshRenderers)
            {
                foreach (Material mat in mr.materials)
                {
                    if (mat.color.a < 1f)
                        showDone = false;

                    Color col = mat.color;
                    col.a += Time.deltaTime * ShowSpeed;
                    if (col.a > 1)
                        col.a = 1;
                    mat.color = col;
                }
            }

            if (showDone)
            {
                foreach (MeshRenderer mr in m_meshRenderers)
                {
                    DisableHide(mr);
                }
                Reseting = false;
                break;
            }

            yield return new WaitForSeconds(0.005f);
        }
    }

    public void ChkTimer()
    {
        if (m_timerCoroutine != null)
        {
            StopCoroutine(m_timerCoroutine);
        }
        m_timerCoroutine = StartCoroutine(ChkTimerCoroutine());
    }
    private IEnumerator ChkTimerCoroutine()
    {
        HideCnt = 0;

        while(true)
        {
            HideCnt += Time.deltaTime;
            if(HideCnt > MaxTimer)
            {
                Reseting = true;
                ShowWall();
                break;
            }

            yield return null;
        }
    }

    private void AbleHide(MeshRenderer _mr)
    {
        foreach (Material mat in _mr.materials)
        {
            FunctionDefine.SetTransparent(mat);
        }
    }
    private void DisableHide(MeshRenderer _mr)
    {
        foreach (Material mat in _mr.materials)
        {
            FunctionDefine.SetObaque(mat);
        }
    }


    private void Awake()
    {
        m_meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }
}
