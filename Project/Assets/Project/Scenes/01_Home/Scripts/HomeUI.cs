using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField]
    Button m_gameButton = null;
    public Button gameButton => m_gameButton;

    [SerializeField]
    Button m_titleButton = null;
    public Button titleButton => m_titleButton;

    [SerializeField]
    PixelArtGameTextController m_craftsManLv = null;
    public PixelArtGameTextController craftsManLv => m_craftsManLv;

    [SerializeField]
    PlateBubble m_plateBubble = null;
    public PlateBubble plateBubble => m_plateBubble;
}
