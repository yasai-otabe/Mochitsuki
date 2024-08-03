using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CraftsManLvUpUI : MonoBehaviour
{
    [SerializeField]
    Button m_nextButton = null;
    public Button nextButton => m_nextButton;

    [SerializeField]
    PixelArtGameTextController m_craftsManName = null;
    public PixelArtGameTextController craftsManName => m_craftsManName;

    [SerializeField]
    PixelArtGameTextController m_totalMochitsukiCountText = null;
    int m_totalCounts = 0;
    int m_additiveCounts = 0;

    public Tweener GetCountUpTweener()
    {
        var duration = m_additiveCounts * 0.1f;
        return DOVirtual.Int(m_totalCounts, m_totalCounts + m_additiveCounts, 3f,
            _ => m_totalMochitsukiCountText.SetNumber(_))
            .SetEase(Ease.InOutSine);
    }

    public void Init(int totalCounts, int additiveCounts)
    {
        m_totalCounts = totalCounts;
        m_additiveCounts = additiveCounts;
        m_totalMochitsukiCountText.SetNumber(m_totalCounts);
    }
}
