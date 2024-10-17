using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomInfoUIScript : FadeUIScript
{
    private TextMeshProUGUI m_infoTxt;

    private const float DisplayTime = 5;

    public void SetInfoTxt(string _info)
    {
        m_infoTxt.text = _info;
    }

    public override void FadeInDone()
    {
        StartCoroutine(DisplayDone());
    }

    private IEnumerator DisplayDone()
    {
        yield return new WaitForSeconds(DisplayTime);
        FadeOutStart();
    }

    public override void FadeOutDone()
    {
        Destroy(gameObject);
    }

    public override void Awake()
    {
        base.Awake();
        m_infoTxt = GetComponentInChildren<TextMeshProUGUI>();
    }
}
