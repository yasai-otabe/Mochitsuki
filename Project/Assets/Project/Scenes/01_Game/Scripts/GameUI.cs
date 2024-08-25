using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMeshPro = null;
    public TextMeshProUGUI TextMeshPro => m_TextMeshPro;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProTutorial = null;
    public TextMeshProUGUI TextMeshProTutorial => m_TextMeshProTutorial;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProPenalty = null;
    public TextMeshProUGUI TextMeshProPenalty => m_TextMeshProPenalty;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProCount = null;
    public TextMeshProUGUI TextMeshProCount => m_TextMeshProCount;

    [SerializeField]
    private SpriteRenderer m_UIGuide1P = null;
    public SpriteRenderer UIGuide1P => m_UIGuide1P;

    [SerializeField]
    private SpriteRenderer m_UIGuide2P = null;
    public SpriteRenderer UIGuide2P => m_UIGuide2P;
}
