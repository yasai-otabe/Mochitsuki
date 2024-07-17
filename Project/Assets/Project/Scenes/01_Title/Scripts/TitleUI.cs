using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    Button m_homeButton = null;
    public Button homeButton => m_homeButton;

    [SerializeField]
    Button m_optionButton = null;

    [SerializeField]
    Option m_option = null;

    void Awake()
    {
        // オプションポップを開く
        m_optionButton.onClick.AddListener(m_option.Show);
    }
}
