using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField]
    List<Button> m_closeButtons = new();

    void Awake()
    {
        // 閉じる
        m_closeButtons.ForEach(_ => _.onClick.AddListener(Hide));
    }

    public void Show()
    {
        gameObject.SetActive(transform);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
