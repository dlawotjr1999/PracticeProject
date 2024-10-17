using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArangeUIScript : MonoBehaviour
{
    private TextMeshProUGUI m_cntNumTxt;

    private const float DoneTime = 5;

    public void InitUI()
    {
        SetCurNum(0);
    }

    public void MonsterAranged(int _arange)
    {
        SetCurNum(_arange);
    }

    public void ArangeDone()
    {
        m_cntNumTxt.color = Color.green;
        StartCoroutine(UIDone());
    }

    private void SetCurNum(int _num)
    {
        m_cntNumTxt.text = _num.ToString();
    }

    private IEnumerator UIDone()
    {
        yield return new WaitForSeconds(DoneTime);
        Destroy(gameObject);
    }


    private void Awake()
    {
        m_cntNumTxt = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }
}
