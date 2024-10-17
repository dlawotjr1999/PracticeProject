using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIScript : MonoBehaviour
{
    private Image[] m_imgs;
    private TextMeshProUGUI[] m_txts;

    [SerializeField]
    protected float m_fadeTime = 0.5f;

    private float CurAlpha { get; set; }

    private void SetUIAlpha(float _alpha) { SetImgsAlpha(_alpha); SetTxtsAlpha(_alpha); }
    private void SetImgsAlpha(float _alpha) { foreach (Image img in m_imgs) { img.color = new Color(1, 1, 1, _alpha); } }
    private void SetTxtsAlpha(float _alpha) { foreach (TextMeshProUGUI txt in m_txts) { txt.color = new Color(1, 1, 1, _alpha); } }

    protected void FadeInStart()
    {
        float adj = 1 / m_fadeTime;
        CurAlpha = adj * Time.deltaTime;
        SetUIAlpha(CurAlpha);
        StartCoroutine(Fading(adj, FadeInDone));
    }

    protected void FadeOutStart()
    {
        float adj = -1 / m_fadeTime;
        CurAlpha = 1 + adj * Time.deltaTime;
        SetUIAlpha(CurAlpha);
        StartCoroutine(Fading(adj, FadeOutDone));
    }

    private IEnumerator Fading(float _adj, FPointer _function)
    {
        while(CurAlpha > 0 && CurAlpha < 1)
        {
            CurAlpha += _adj * Time.deltaTime;
            if (CurAlpha < 0) CurAlpha = 0;
            else if (CurAlpha > 1) CurAlpha = 1;
            SetUIAlpha(CurAlpha);
            yield return null;
        }
        _function();
    }

    public virtual void FadeInDone()
    {

    }

    public virtual void FadeOutDone()
    {

    }


    public virtual void Awake()
    {
        m_imgs = GetComponentsInChildren<Image>();
        m_txts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public virtual void Start()
    {
        FadeInStart();
    }
}
