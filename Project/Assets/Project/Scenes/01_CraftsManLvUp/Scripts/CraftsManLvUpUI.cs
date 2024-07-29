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
    TextMeshProUGUI m_craftsManName = null;

    [SerializeField]
    TextMeshProUGUI m_totalMochitsukiCountText = null;
    int m_totalCounts = 0;
    int m_additiveCounts = 0;

    public void SetCraftsManName(string name)
        => m_craftsManName.text = name;

    public void SetTotalMochitsukiCount(int totalCounts)
        => m_totalMochitsukiCountText.text = totalCounts.ToString();

    public Tweener GetCountUpTweener()
    {
        var duration = m_additiveCounts * 0.1f;
        return DOVirtual.Int(m_totalCounts, m_totalCounts + m_additiveCounts, 3f,
            _ => SetTotalMochitsukiCount(_))
            .SetEase(Ease.InOutSine);
    }

    public void Init(int totalCounts, int additiveCounts)
    {
        m_totalCounts = totalCounts;
        m_additiveCounts = additiveCounts;
        SetTotalMochitsukiCount(m_totalCounts);
    }
}
