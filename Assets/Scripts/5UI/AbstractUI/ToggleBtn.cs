using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_toggleUI;

    public void SetToggle()
    {
        if (m_toggleUI == null) { Debug.LogError("������ ��� ����"); return; }

        m_toggleUI.SetActive(!m_toggleUI.activeSelf);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SetToggle);
    }
}
