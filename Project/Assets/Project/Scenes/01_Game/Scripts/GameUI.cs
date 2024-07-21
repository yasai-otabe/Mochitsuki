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
    private TextMeshProUGUI m_TextMeshProA = null;
    public TextMeshProUGUI TextMeshProA => m_TextMeshProA;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProL = null;
    public TextMeshProUGUI TextMeshProL => m_TextMeshProL;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProPenalty = null;
    public TextMeshProUGUI TextMeshProPenalty => m_TextMeshProPenalty;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshProCount = null;
    public TextMeshProUGUI TextMeshProCount => m_TextMeshProCount;
}
