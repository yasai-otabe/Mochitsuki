using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMeshPro = null;
    public TextMeshProUGUI TextMeshPro => m_TextMeshPro;
}
